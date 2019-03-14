using EyeBoard.Logic.Models;
using EyeBoard.Logic.Repositories;
using Newtonsoft.Json;
using Profilan.SharedKernel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace EyeBoard.Areas.Admin.Controllers.Api
{
    public class UploadController : ApiController
    {
        private readonly MediaRepository _mediaRepository;

        public UploadController()
        {
            _mediaRepository = new MediaRepository();
        }

        [Route("api/upload/video")]
        [HttpPost]
        public async Task<HttpResponseMessage> UploadVideos()
        {
            var root = HttpContext.Current.Server.MapPath("~/temp/uploads");
            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            string userId = result.FormData["userId"];

            var videosFolder = System.Configuration.ConfigurationManager.AppSettings["VideosFolder"];

            var originalFileName = GetDeserializedFileName(result.FileData.First());

            var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);
            string path = result.FileData.First().LocalFileName;

            var physicalDir = HttpContext.Current.Server.MapPath(videosFolder) + @"/" + userId;
            if (!Directory.Exists(physicalDir))
            {
                Directory.CreateDirectory(physicalDir);
            }
            var fileExt = Path.GetExtension(originalFileName);
            var fileName = Path.GetFileNameWithoutExtension(originalFileName) + ".mp4";
            string physicalPath = physicalDir + @"/" + fileName;
            string virtualPath = videosFolder + @"/" + userId + @"/" + fileName;

                if (FileHandler.ConvertVideoToMP4(path, physicalPath))
                {

                    var medium = Movie.Create(Path.GetFileNameWithoutExtension(originalFileName), DateTime.Now, DateTime.MaxValue, 0, virtualPath);
                    medium.CreatedBy = userId;
                    medium.ModifiedBy = userId;
                    _mediaRepository.Insert(medium);

                    File.Delete(path);

                    return Request.CreateResponse(HttpStatusCode.OK, new { path = virtualPath, name = originalFileName, id = medium.Id, title = medium.Title }, JsonMediaTypeFormatter.DefaultMediaType);
                }
 
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("api/upload/presentation")]
        [HttpPost]
        public async Task<HttpResponseMessage> UploadPresentations()
        {
            var root = HttpContext.Current.Server.MapPath("~/temp/uploads");
            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            string userId = result.FormData["userId"];

            var presentationsFolder = System.Configuration.ConfigurationManager.AppSettings["PresentationsFolder"];

            var originalFileName = GetDeserializedFileName(result.FileData.First());

            var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);
            string path = result.FileData.First().LocalFileName;

            var physicalDir = HttpContext.Current.Server.MapPath(presentationsFolder) + @"/" + userId;
            if (!Directory.Exists(physicalDir))
            {
                Directory.CreateDirectory(physicalDir);
            }
            var fileName = Path.GetFileNameWithoutExtension(originalFileName) + ".mp4";
            string physicalPath = physicalDir + @"/" + fileName;
            string virtualPath = presentationsFolder + @"/" + userId + @"/" + fileName;
            if (FileHandler.ConvertPPTToMP4(path, physicalPath))
            {
                
                var medium = Presentation.Create(Path.GetFileNameWithoutExtension(originalFileName), DateTime.Now, DateTime.MaxValue, 0, virtualPath);
                medium.CreatedBy = userId;
                medium.ModifiedBy = userId;
                _mediaRepository.Insert(medium);

                File.Delete(path);

                return Request.CreateResponse(HttpStatusCode.OK, new { path = virtualPath, name = originalFileName, id = medium.Id, title = medium.Title }, JsonMediaTypeFormatter.DefaultMediaType);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        private string GetDeserializedFileName(MultipartFileData multipartFileData)
        {
            var fileName = GetFileName(multipartFileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        private string GetFileName(MultipartFileData multipartFileData)
        {
            return multipartFileData.Headers.ContentDisposition.FileName;
        }
    }
}
