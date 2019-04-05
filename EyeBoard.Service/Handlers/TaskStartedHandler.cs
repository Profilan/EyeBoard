using APITaskManagement.Service.Hubs;
using EyeBoard.Logic.Events;
using EyeBoard.Logic.Repositories;
using Microsoft.AspNet.SignalR;
using Profilan.SharedKernel;
using System.Diagnostics;

namespace AEyeBoard.Service.Handlers
{
    public class TaskStartedHandler : IHandle<TaskStartedEvent>
    {
        private readonly TaskRepository _taskRepository = new TaskRepository();

        public void Handle(TaskStartedEvent args)
        {
            args.Task.Active = true;
            _taskRepository.Update(args.Task);

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<TaskSchedulerHub>();
            hubContext.Clients.All.startTask(args.Task.Id.ToString(), args.Task.OutputFile, args.Task.Active);

            EventLog eventLog = new EventLog();
            eventLog.Source = "EyeBoard Scheduler";
            eventLog.Log = "EyeBoard Management";

            eventLog.WriteEntry("Task started: " + args.Task.OutputFile, System.Diagnostics.EventLogEntryType.Information, 1006);
        }
    }
}
