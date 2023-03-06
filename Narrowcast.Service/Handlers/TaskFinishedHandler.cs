using EyeBoard.Logic.Events;
using EyeBoard.Logic.Models;
using EyeBoard.Logic.Repositories;
using Microsoft.AspNet.SignalR;
using Narrowcast.Service.Hubs;
using Profilan.SharedKernel;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace Narrowcast.Service.Handlers
{
    public class TaskFinishedHandler : IHandle<TaskFinishedEvent>
    {
        private readonly TaskRepository _taskRepository = new TaskRepository();
        private readonly MediaRepository _mediaRepository = new MediaRepository();

        public void Handle(TaskFinishedEvent args)
        {
           

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<TaskSchedulerHub>();
            
            var originalfilename = Path.GetFileName(args.Task.OriginalFile);
            var filename = Path.GetFileNameWithoutExtension(originalfilename) + ".mp4";

            var directory = Path.GetDirectoryName(args.Task.OutputFile);
            var folders = directory.Split('\\');
            var folder = "";
            switch (args.Task.TaskType)
            {
                case TaskType.Presentation:
                    folder = ConfigurationManager.AppSettings["PresentationsFolder"];
                    break;
                case TaskType.Video:
                    folder = ConfigurationManager.AppSettings["VideosFolder"];
                    break;
            }
            string userId = folders[folders.Length - 1];
            string virtualPath = folder + @"/" + userId + @"/" + filename;

            Medium medium = null;
            switch (args.Task.TaskType)
            {
                case TaskType.Video:
                    medium = Movie.Create(Path.GetFileNameWithoutExtension(originalfilename), DateTime.Now, DateTime.MaxValue, 0, virtualPath);
                    medium.CreatedBy = userId;
                    medium.ModifiedBy = userId;
                    _mediaRepository.Insert(medium);
                    break;
                case TaskType.Presentation:
                    medium = Presentation.Create(Path.GetFileNameWithoutExtension(originalfilename), DateTime.Now, DateTime.MaxValue, 0, virtualPath);
                    medium.CreatedBy = userId;
                    medium.ModifiedBy = userId;
                    _mediaRepository.Insert(medium);
                    break;
            }


            // Cleanup stuff
            _taskRepository.Delete(args.Task.Id);
            File.Delete(args.Task.InputFile);

            hubContext.Clients.All.finishTask(medium.Id.ToString(), virtualPath, originalfilename, medium.Title);

            EventLog eventLog = new EventLog()
            {
                Source = "EyeBoard Scheduler",
                Log = "EyeBoard Management"
            };

            eventLog.WriteEntry("Task finished: " + args.Task.OutputFile, System.Diagnostics.EventLogEntryType.Information, 1004);
        }
    }
}
