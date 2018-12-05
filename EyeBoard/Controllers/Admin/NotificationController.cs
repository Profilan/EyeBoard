using EyeBoard.Logic.Models;
using EyeBoard.Logic.Repositories;
using EyeBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EyeBoard.Controllers.Admin
{
    [Authorize(Roles = "superuser, administrator")]
    public class NotificationController :  BaseController
    {
        private readonly NotificationRepository _notificationRepository = new NotificationRepository();
        private readonly ScreenGroupRepository _screenGroupRepository = new ScreenGroupRepository();

        // GET: Notification
        public ActionResult Index()
        {
            var items = _notificationRepository.List();
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

            return View(notifications);
        }


        public ActionResult Create()
        {
            var model = new NotificationViewModel()
            {
                Groups = _screenGroupRepository.List(),
                PublishUp = DateTime.Now,
                PublishDown = DateTime.MaxValue
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var notification = Notification.Create(collection["Title"]);
                notification.CreatedBy = GetCurrentUser().Id;
                notification.ModifiedBy = notification.CreatedBy;
                _notificationRepository.Insert(notification);

                var selectedGroups = collection["Groups[]"];
                var groups = _screenGroupRepository.List();
                if (!String.IsNullOrEmpty(selectedGroups))
                {
                    foreach (var group in groups)
                    {
                        // Is the group checked
                        if (IsChecked(group, selectedGroups))
                        {
                            if (!notification.Groups.Contains(group))
                            {
                                group.AddNewNotification(notification);
                                _screenGroupRepository.Update(group);
                            }
                        }
                        else
                        {
                            if (notification.Groups.Contains(group))
                            {
                                group.DeleteNotification(notification);
                                _screenGroupRepository.Update(group);
                            }
                        }

                    }
                }
                else
                {
                    foreach (var group in groups)
                    {
                        if (notification.Groups.Contains(group))
                        {
                            group.DeleteNotification(notification);
                            _screenGroupRepository.Update(group);
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(Guid id)
        {
            try
            {
                var notification = _notificationRepository.GetById(id);

                var model = new NotificationViewModel()
                {
                    Id = notification.Id,
                    Title = notification.Title,
                    PublishUp = notification.PublishUp,
                    PublishDown = notification.PublishDown,
                    Groups = _screenGroupRepository.List(),
                    SelectedGroups = notification.Groups
                };

                return View(model);
            }
            catch (Exception e)
            {

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                var notification = _notificationRepository.GetById(new Guid(collection["Id"]));

                var selectedGroups = collection["Groups[]"];
                var groups = _screenGroupRepository.List();
                if (!String.IsNullOrEmpty(selectedGroups))
                {
                    foreach (var group in groups)
                    {
                        // Is the group checked
                        if (IsChecked(group, selectedGroups))
                        {
                            if (!notification.Groups.Contains(group))
                            {
                                group.AddNewNotification(notification);
                                _screenGroupRepository.Update(group);
                            }
                        }
                        else
                        {
                            if (notification.Groups.Contains(group))
                            {
                                group.DeleteNotification(notification);
                                _screenGroupRepository.Update(group);
                            }
                        }

                    }
                }
                else
                {
                    foreach (var group in groups)
                    {
                        if (notification.Groups.Contains(group))
                        {
                            group.DeleteNotification(notification);
                            _screenGroupRepository.Update(group);
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                var model = new NotificationViewModel()
                {
                    Id = new Guid(collection["Id"]),
                    Title = collection["Title"]
                };
                return View(model);
            }
        }

        private bool IsChecked(ScreenGroup group, string groups)
        {
            foreach (var groupId in groups.Split(','))
            {
                if (group.Id == new Guid(groupId))
                {
                    return true;
                }
            }

            return false;
        }

    }
}