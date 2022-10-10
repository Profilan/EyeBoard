using EyeBoard.Logic.MessageBrokers.Models;
using EyeBoard.Logic.MessageBrokers.Publishers;
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
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace EyeBoard.Areas.Admin.Controllers.Api
{
    public class UploadController : ApiController
    {
        private readonly MediaRepository _mediaRepository = new MediaRepository();
        // private readonly TaskRepository _taskRepository = new TaskRepository();
        private readonly PublisherBase _publisher = MessageBrokerPublisherFactory.Create(MessageBrokerType.RabbitMq);

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


            // TODO: Change this to Messagebus
            var task = Logic.Models.Task.Create(path, physicalPath, originalFileName, TaskType.Video);
            var taskMessageJson = JsonConvert.SerializeObject(task);
            var messageBytes = Encoding.UTF8.GetBytes(taskMessageJson);
            var brokerMessage = new Message(messageBytes, Guid.NewGuid().ToString("N"), "application/json");
            await _publisher.Publish(brokerMessage);

            // _taskRepository.Insert(task);

            return Request.CreateResponse(HttpStatusCode.OK, new { path = virtualPath, name = originalFileName, id = task.Id }, JsonMediaTypeFormatter.DefaultMediaType);

            // return Request.CreateResponse(HttpStatusCode.BadRequest);
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

            var task = Logic.Models.Task.Create(path, physicalPath, originalFileName, TaskType.Video);
            var taskMessageJson = JsonConvert.SerializeObject(task);
            var messageBytes = Encoding.UTF8.GetBytes(taskMessageJson);
            var brokerMessage = new Message(messageBytes, Guid.NewGuid().ToString("N"), "application/json");
            await _publisher.Publish(brokerMessage);
            // _taskRepository.Insert(task);

            return Request.CreateResponse(HttpStatusCode.OK, new { path = virtualPath, name = originalFileName, id = task.Id }, JsonMediaTypeFormatter.DefaultMediaType);

            // return Request.CreateResponse(HttpStatusCode.BadRequest);
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
