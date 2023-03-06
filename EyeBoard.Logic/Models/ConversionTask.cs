﻿using EyeBoard.Logic.Events;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Models
{
    public enum TaskType
    {
        Video,
        Presentation
    }

    public class ConversionTask : Entity<Guid>
    {
        public virtual string InputFile { get; set; }
        public virtual string OutputFile { get; set; }
        public virtual string OriginalFile { get; set; }
        public virtual TaskType TaskType { get; set; }
        public virtual bool Active { get; set; }
        public virtual DateTime Created { get; set; }

        protected ConversionTask() : base()
        {
            
        }

        public ConversionTask(Guid id) : base(id)
        {
            
        }

        public static ConversionTask Create(string inputFile, string outputFile, string originalFile, TaskType taskType)
        {
            Guard.ForNullOrEmpty(inputFile, "inputFile");
            Guard.ForNullOrEmpty(outputFile, "outputFile");
            Guard.ForNullOrEmpty(originalFile, "originalFile");
            var task = new ConversionTask(Guid.NewGuid())
            {
                Active = false,
                InputFile = inputFile,
                OutputFile = outputFile,
                OriginalFile = originalFile,
                TaskType = taskType,
                Created = DateTime.Now,
            };

            return task;
        }

        public virtual bool Run()
        {
            Active = true;

            var taskStartedEvent = new TaskStartedEvent(this);
            DomainEvents.Raise(taskStartedEvent);

            var result = false;

            switch (TaskType)
            {
                case TaskType.Presentation:
                    result = FileHandler.ConvertPPTToMP4(InputFile, OutputFile);
                    break;
                case TaskType.Video:
                    result = FileHandler.ConvertVideoToMP4(InputFile, OutputFile);
                    break;
            }

            var taskFinishedEvent = new TaskFinishedEvent(this);
            DomainEvents.Raise(taskFinishedEvent);

            return result;
        }
    }

    
}