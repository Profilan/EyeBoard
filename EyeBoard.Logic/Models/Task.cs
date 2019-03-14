using EyeBoard.Logic.Events;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public virtual TaskType TaskType { get; set; }
        public virtual bool Active { get; set; }

        protected Task() : base()
        {

        }

        public Task(Guid id) : base(id)
        {

        }

        public static Task Create(string inputFile, string outputFile, TaskType taskType)
        {
            Guard.ForNullOrEmpty(inputFile, "inputFile");
            Guard.ForNullOrEmpty(outputFile, "outputFile");
            var task = new Task(Guid.NewGuid());
            task.Active = false;
            task.InputFile = inputFile;
            task.OutputFile = outputFile;
            task.TaskType = taskType;

            return task;
        }

        public void Run()
        {
            Active = true;

            var taskStartedEvent = new TaskStartedEvent(this);
            DomainEvents.Raise(taskStartedEvent);

            switch (TaskType)
            {
                case TaskType.Presentation:
                    FileHandler.ConvertPPTToMP4(InputFile, OutputFile);
                    break;
                case TaskType.Video:
                    FileHandler.ConvertVideoToMP4(InputFile, OutputFile);
                    break;
            }

            var taskFinishedEvent = new TaskFinishedEvent(this);
            DomainEvents.Raise(taskFinishedEvent);
        }
    }

    
}
