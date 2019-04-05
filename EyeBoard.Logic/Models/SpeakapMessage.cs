using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Models
{
    public class SpeakapMessage
    {
        public string Id { get; set; }

        public DateTime Created { get; set; }

        public string FullText { get; set; }

        public string Author { get; set; }

        public int Likes { get; set; }

        public IList<string> Images { get; set; }

        public SpeakapMessage()
        {
            Images = new List<string>();
        }
    }
}
