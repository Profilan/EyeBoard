
using EyeBoard.Logic.Models;
using System;
using System.Collections.Generic;

namespace EyeBoard.Areas.Admin.Models
{
    public class FileInfoModel
    {
        public string UploadUrl { get; set; }
        public IEnumerable<Medium> Media { get; set; }
        public string AcceptFileTypes { get; set; }
        public int MaxFileSize { get; set; }
        public string UserId { get; set; }
    }
}