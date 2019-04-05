using EyeBoard.Logic.Models;
using EyeBoard.Logic.Repositories;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using System;
using System.Configuration;
using System.ServiceProcess;

[assembly: OwinStartup(typeof(EyeBoard.Service.Startup))]
namespace EyeBoard.Service
{
    public partial class EyeBoardService : ServiceBase
    {
        private readonly TaskRepository _taskRepository = new TaskRepository();

        public IHubProxy Hub { get; private set; }
        public string Url { get; private set; }

        public EyeBoardService()
        {
            InitializeComponent();

            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("EyeBoard Scheduler"))
            {
                System.Diagnostics.EventLog.CreateEventSource("EyeBoard Scheduler", "EyeBoard Management");
            }
            eventLog1.Source = "EyeBoard Scheduler";
            eventLog1.Log = "EyeBoard Management";

            Url = ConfigurationManager.AppSettings["HubConnectionUrl"];
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("EyeBoard Scheduler started", System.Diagnostics.EventLogEntryType.Information, 0);

            try
            {
                WebApp.Start(Url);

                var connection = new HubConnection(Url);
                Hub = connection.CreateHubProxy("taskSchedulerHub");
                Hub.On<string>("runTask", taskId => RunTask(taskId));
                connection.Start().Wait();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void AddTask(string inputFile, string outputFile, string originalFile, TaskType taskType)
        {
            Task task = Task.Create(inputFile, outputFile, originalFile, taskType);
            task.Run();
        }

        public void RunTask(string id)
        {
            try
            {
                var task = _taskRepository.GetById(new Guid(id));

                task.Run();
            }
            catch (Exception e)
            {
                eventLog1.WriteEntry("EyeBoard Task error: " + e.StackTrace, System.Diagnostics.EventLogEntryType.Error, 1001);
            }
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
