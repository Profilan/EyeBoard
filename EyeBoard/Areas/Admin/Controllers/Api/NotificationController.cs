using EyeBoard.Logic.Repositories;
using EyeBoard.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EyeBoard.Areas.Admin.Controllers.Api
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

        [Route("api/Notification/Delete/{id}")]
        [HttpPost]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                var notification = _notificationRepository.GetById(id);

                var groups = _groupRepository.List();

                foreach (var group in groups)
                {
                    if (notification.Groups.Contains(group))
                    {
                        group.DeleteNotification(notification);
                        _groupRepository.Update(group);
                    }
                }

                _notificationRepository.Delete(id);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Content(HttpStatusCode.NoContent, "Notification is succesfully removed.");
        }
    }
}
