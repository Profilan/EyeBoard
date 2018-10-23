using Profilan.SharedKernel;
using System;

namespace EyeBoard.Logic.Models
{
    public class Article : Entity
    {
        public virtual string Title { get; set; }
        public virtual string FullText { get; set; }
        public virtual int State { get; set; }
        public virtual DateTime PublishUp { get; set; }
        public virtual DateTime PublishDown { get; set; }
        public virtual int Ordering { get; set; }

        public virtual DateTime Created { get; set; }
        public virtual int CreatedBy { get; set; }
        public virtual DateTime Modified { get; set; }
        public virtual int ModifiedBy { get; set; }

        public Article()
        {
            State = 0;
            PublishUp = DateTime.Now;
            PublishDown = DateTime.MaxValue;
            Ordering = 0;
        }
    }
}
