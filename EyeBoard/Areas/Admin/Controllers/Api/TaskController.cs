using EyeBoard.Areas.Admin.Models;
using EyeBoard.Logic.Models;
using EyeBoard.Logic.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EyeBoard.Areas.Admin.Controllers.Api
{
    public class TaskController : ApiController
    {
        private readonly TaskRepository _taskRepository = new TaskRepository();

        [HttpGet]
        [Route("api/task")]
        public IHttpActionResult GetTasks(TaskType taskType)
        {
            var items = _taskRepository.List().Where(x => x.Active == true && x.TaskType == taskType);

            var tasks = new List<TaskViewModel>();
            foreach (var task in items)
            {
                tasks.Add(new TaskViewModel()
                {
                    Id = task.Id,
                    InputFile = task.InputFile,
                    OutputFile = task.OutputFile,
                    OriginalFile = task.OriginalFile,
                });
            }

            return Ok(tasks);
        }
    }
}
