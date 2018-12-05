using EyeBoard.Logic.Repositories;
using EyeBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EyeBoard.Controllers.Admin.Api
{
    public class NotificationController : ApiController
    {
        private readonly NotificationRepository _notificationRepository = new NotificationRepository();
        private readonly ScreenGroupRepository _groupRepository = new ScreenGroupRepository();

        [HttpGet]
        [Route("api/notification/{groupId}")]
        public IHttpActionResult GetList(Guid groupId)
        {
            var group = _groupRepository.GetById(groupId);
            var dateNow = DateTime.Now;
            var items = _notificationRepository.ListByGroup(group).Where(x => x.PublishUp <= dateNow && x.PublishDown >= dateNow);
            var notifications = new List<NotificationViewModel>();
            foreach (var item in items)
            {
                notifications.Add(new NotificationViewModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    PublishUp = item.PublishUp,
                    PublishDown = item.PublishDown
                });
            }

            return Ok(notifications);
        }
    }
}
