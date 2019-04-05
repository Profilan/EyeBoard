
using EyeBoard.Logic.Models;
using System;

namespace EyeBoard.Areas.Admin.Models
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }
        public string InputFile { get; set; }
        public string OutputFile { get; set; }
        public string OriginalFile { get; set; }
    }
}