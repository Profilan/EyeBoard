using EyeBoard.Logic.Models;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Owin.Hosting;
using System;
using System.Configuration;
using System.ServiceProcess;

namespace EyeBoard.Service
{
    public partial class EyeBoardService : ServiceBase
    {
        public IHubProxy Hub { get; private set; }
        public string Url { get; private set; }

        public EyeBoardService()
        {
            InitializeComponent();

            Url = ConfigurationManager.AppSettings["HubConnectionUrl"];
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                WebApp.Start(Url);

                var connection = new HubConnection(Url);
                Hub = connection.CreateHubProxy("taskSchedulerHub");
                Hub.On<Task>("addTask", task => AddTask(task.InputFile, task.OutputFile, task.TaskType));
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void AddTask(string inputFile, string outputFile, TaskType taskType)
        {
            Task task = Task.Create(inputFile, outputFile, taskType);
            task.Run();
        }

        protected override void OnStop()
        {
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
    }
}
