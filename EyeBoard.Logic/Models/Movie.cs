using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Models
{
    public class Movie : Medium
    {
        protected Movie() : base()
        {

        }

        public Movie(Guid id) : base(id)
        {

        }

        public static Movie Create(string title,
            DateTime? publishUp,
            DateTime? publishDown,
            int? ordering,
            string url)
        {
            Guard.ForNullOrEmpty(title, "title");
            var movie = new Movie(Guid.NewGuid());
            movie.Title = title;
            movie.PublishUp = publishUp;
            movie.PublishDown = publishDown;
            movie.Ordering = ordering;
            movie.Url = url;
            return movie;
        }
    }
}
