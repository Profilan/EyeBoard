using Profilan.SharedKernel;
using System;

namespace EyeBoard.Logic.Models
{
    public class Category : Entity<Guid>, ISystemInfo
    {
        public virtual int State { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime Modified { get; set; }
        public virtual string ModifiedBy { get; set; }

        public virtual string Title { get; set; }
        public virtual string Model { get; set; }

        public Category() : base()
        {

        }

        public Category(Guid id) : base(id)
        {

        }
    }
}
