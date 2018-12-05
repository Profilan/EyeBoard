using Profilan.SharedKernel;
using System;

namespace EyeBoard.Logic.Models
{
    public class Article : Entity<Guid>, ISystemInfo
    {
        public virtual int State { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual int CreatedBy { get; set; }
        public virtual DateTime Modified { get; set; }
        public virtual int ModifiedBy { get; set; }

        public virtual string Title { get; set; }
        public virtual string FullText { get; set; }
        public virtual DateTime PublishUp { get; set; }
        public virtual DateTime PublishDown { get; set; }
        public virtual int Ordering { get; set; }

        public Article()
        {

        }

        public Article(Guid id) : base(id)
        {
            PublishUp = DateTime.Now;
            PublishDown = DateTime.MaxValue;
            Ordering = 0;
        }
    }
}
