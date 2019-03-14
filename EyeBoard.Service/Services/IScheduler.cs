using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Service.Services
{
    public interface IScheduler
    {
        void Start();
        void Stop();
        void DisableTask(string taskId);
        void EnableTask(string taskId);
        void Run(Guid taskId);
    }
}
