using EyeBoard.Logic.MessageBrokers.Models;
using EyeBoard.Logic.MessageBrokers.Subscribers;
using EyeBoard.Logic.Repositories;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using Profilan.SharedKernel.Enums;
using System;
using System.Configuration;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Narrowcast.Service
{
    public partial class NarrowcastService : ServiceBase
    {
        private readonly TaskRepository _taskRepository = new TaskRepository();
        private readonly SubscriberBase _subscriber;
        private ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        private System.Timers.Timer _timer;

        private IHubProxy _hubProxy;

        public NarrowcastService()
        {
            InitializeComponent();

            eventLog1 = new System.Diagnostics.EventLog();

            if (!System.Diagnostics.EventLog.SourceExists("EyeBoard Scheduler"))
            {
                System.Diagnostics.EventLog.CreateEventSource("EyeBoard Scheduler", "EyeBoard Management");
            }
            eventLog1.Source = "EyeBoard Scheduler";
            eventLog1.Log = "EyeBoard Management";

            _subscriber = MessageBrokerSubscriberFactory.Create(MessageBrokerType.RabbitMq);
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

            eventLog1.WriteEntry("EyeBoard Scheduler started", System.Diagnostics.EventLogEntryType.Information, 0);


            _timer = new System.Timers.Timer();
            _timer.Interval = 10000;
            _timer.Elapsed += new ElapsedEventHandler(WorkerThreadFunc);
            _timer.Start();

            //WorkerThreadFunc();
        }

        private void WorkerThreadFunc(object sender, ElapsedEventArgs e)
        {
            _timer.Enabled = false;
            
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

                }
                catch (Exception ex)
                {
                    eventLog1.WriteEntry("EyeBoard Task error: " + ex.StackTrace, System.Diagnostics.EventLogEntryType.Error, 1001);
                }
        }

        public Task<bool> RunTask(string id)
        {
            var result = false;
            try
            {
                var task = _taskRepository.GetById(new Guid(id));

                eventLog1.WriteEntry("Running Task for " + task.OutputFile, System.Diagnostics.EventLogEntryType.Information, 1003);

                result = task.Run(eventLog1);
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry("EyeBoard Task error: " + ex.StackTrace, System.Diagnostics.EventLogEntryType.Error, 1002);
            }

            return Task.FromResult(result);
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("EyeBoard Scheduler stopped", System.Diagnostics.EventLogEntryType.Information, 0);
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
    }
}
