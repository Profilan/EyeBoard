using EyeBoard.Logic.Events;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EyeBoard.Logic.Models
{
    public enum TaskType
    {
        Video,
        Presentation
    }

    public class Task : Entity<Guid>
    {
        public virtual string InputFile { get; set; }
        public virtual string OutputFile { get; set; }
        public virtual string OriginalFile { get; set; }
        public virtual TaskType TaskType { get; set; }
        public virtual bool Active { get; set; }

        protected Task() : base()
        {

        }

        public Task(Guid id) : base(id)
        {

        }

        public static Task Create(string inputFile, string outputFile, string originalFile, TaskType taskType)
        {
            Guard.ForNullOrEmpty(inputFile, "inputFile");
            Guard.ForNullOrEmpty(outputFile, "outputFile");
            Guard.ForNullOrEmpty(originalFile, "originalFile");
            var task = new Task(Guid.NewGuid())
            {
                Active = false,
                InputFile = inputFile,
                OutputFile = outputFile,
                OriginalFile = originalFile,
                TaskType = taskType,
            };

            return task;
        }

        public virtual bool Run(EventLog eventLog)
        {
            Active = true;

            var taskStartedEvent = new TaskStartedEvent(this);
            DomainEvents.Raise(taskStartedEvent);

            bool result = false;
            switch (TaskType)
            {
                case TaskType.Presentation:
                    result = FileHandler.ConvertPPTToMP4(InputFile, OutputFile, eventLog);
                    break;
                case TaskType.Video:
                    result = FileHandler.ConvertVideoToMP4(InputFile, OutputFile, eventLog);
                    break;
            }

            var taskFinishedEvent = new TaskFinishedEvent(this);
            DomainEvents.Raise(taskFinishedEvent);

            return result;
        }
    }

    
}
