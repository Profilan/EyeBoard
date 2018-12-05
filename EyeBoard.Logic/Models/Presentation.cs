using Profilan.SharedKernel;
using System;

namespace EyeBoard.Logic.Models
{
    public class Presentation : Medium
    {
        protected Presentation() : base()
        {

        }

        public Presentation(Guid id) : base(id)
        {

        }

        public static Presentation Create(string title,
            DateTime? publishUp,
            DateTime? publishDown,
            int? ordering,
            string url)
        {
            Guard.ForNullOrEmpty(title, "title");
            var presentation = new Presentation(Guid.NewGuid());
            presentation.Title = title;
            presentation.PublishUp = publishUp;
            presentation.PublishDown = publishDown;
            presentation.Ordering = ordering;
            presentation.Url = url;
            return presentation;
        }
    }
}
