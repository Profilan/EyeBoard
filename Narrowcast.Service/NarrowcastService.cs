using EyeBoard.Logic.MessageBrokers.Models;
using EyeBoard.Logic.MessageBrokers.Subscribers;
using EyeBoard.Logic.Repositories;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(Narrowcast.Service.Startup))]
namespace Narrowcast.Service
{
    public partial class NarrowcastService : ServiceBase
    {
        private readonly TaskRepository _taskRepository = new TaskRepository();
        private readonly SubscriberBase _subscriber;
        private ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        private Thread _thread;

        private IHubProxy _hubProxy;

        public NarrowcastService()
        {
            InitializeComponent();

            _subscriber = MessageBrokerSubscriberFactory.Create(Profilan.SharedKernel.Enums.MessageBrokerType.RabbitMq);
        }

        protected override async void OnStart(string[] args)
        {

            // Start Video Queue Listener (RabbitMQ)
            /*
            _thread = new Thread(WorkerThreadFunc);
            _thread.Name = "Video Queue Listener";
            _thread.IsBackground = true;
            _thread.Start();
            */
            WorkerThreadFunc();

            while (true)
            {

            }
        }
        private void WorkerThreadFunc()
        {
            //Task.Delay(60000);
            var hubConnectionUrl = ConfigurationManager.AppSettings["HubConnectionUrl"];

            WebApp.Start(hubConnectionUrl);

            // Start SignalR Proxy

            using (var hubConnection = new HubConnection(hubConnectionUrl))
            {
                _hubProxy = hubConnection.CreateHubProxy("taskSchedulerHub");

                hubConnection.Start().Wait();
            }

            while (true)
            {
                try
                {
                    _subscriber.Subscribe(async (subs, messageReceivedEventArgs) =>
                    {
                        var body = messageReceivedEventArgs.ReceivedMessage.Body;
                        var taskMessageJson = Encoding.UTF8.GetString(body);
                        TaskMessage taskMessage = JsonConvert.DeserializeObject<TaskMessage>(taskMessageJson);

                        if (taskMessage != null)
                        {
                            var isOk = await RunTask(taskMessage.TaskId.ToString());

                            if (isOk)
                            {
                                await subs.Acknowledge(messageReceivedEventArgs.AcknowledgeToken);
                            }
                        }
                    });

                    Task.Delay(25000);
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                }
            }
        }
        public Task<bool> RunTask(string id)
        {
            var result = false;
            try
            {
                var task = _taskRepository.GetById(new Guid(id));

                result = task.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Task.FromResult(result);
        }

        protected override void OnStop()
        {
            _shutdownEvent.Set();
            if (!_thread.Join(3000))
            { // give the thread 3 seconds to stop
                _thread.Abort();
            }
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
    }
}
