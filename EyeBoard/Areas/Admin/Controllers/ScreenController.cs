using EyeBoard.Logic.Models;
using EyeBoard.Logic.Repositories;
using EyeBoard.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EyeBoard.Areas.Admin.Controllers
{
    [Authorize(Roles = "GRolNarrowcastBeheerder")]
    public class ScreenController : BaseController
    {
        private readonly ScreenRepository _screenRepository;
        private readonly ScreenGroupRepository _screenGroupRepository;

        public ScreenController() : base()
        {
            _screenRepository = new ScreenRepository();
            _screenGroupRepository = new ScreenGroupRepository();
        }

        public ActionResult Index()
        {
            var items = _screenRepository.List();

            var screens = new List<ScreenViewModel>();
            foreach (var item in items)
            {
                screens.Add(new ScreenViewModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Location = item.Location,
                    CreatedBy = "Administrator",
                    Modified = item.Modified
                });
            }

            return View(screens);
        }

        public ActionResult Create()
        {
            var groupList = _screenGroupRepository.List().OrderBy(x => x.Title);
            var groups = new List<SelectListItem>();
            foreach (var group in groupList)
            {
                groups.Add(new SelectListItem()
                {
                    Value = group.Id.ToString(),
                    Text = group.Title
                });
            }


            return View(new ScreenViewModel()
            {
                Groups = groups
            });
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var group = _screenGroupRepository.GetById(new Guid(collection["GroupId"]));
                var screen = Screen.Create(collection["Title"], collection["Location"], group);
                screen.CreatedBy = GetCurrentUser().User.ToString();
                screen.ModifiedBy = screen.CreatedBy;
                screen.HostName = collection["HostName"];
                screen.RefreshTime = new RefreshTime(Convert.ToInt32(collection["RefreshHours"]), Convert.ToInt32(collection["RefreshMinutes"]), Convert.ToInt32(collection["RefreshSeconds"]));

                _screenRepository.Insert(screen);

                Request.Flash("success", Resources.Resources.Screen + " " + Resources.Resources.Saved);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Request.Flash("error", Resources.Resources.SevereError + ": " + e.Message);

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(Guid id)
        {
            try
            {
                var screen = _screenRepository.GetById(id);

                var groupList = _screenGroupRepository.List().OrderBy(x => x.Title);
                var groups = new List<SelectListItem>();
                foreach (var group in groupList)
                {
                    groups.Add(new SelectListItem()
                    {
                        Value = group.Id.ToString(),
                        Text = group.Title
                    });
                }

                var model = new ScreenViewModel()
                {
                    Id = screen.Id,
                    Title = screen.Title,
                    Location = screen.Location,
                    HostName = screen.HostName,
                    GroupId = screen.Group != null ? screen.Group.Id.ToString() : "",
                    Groups = groups,
                    RefreshHours = screen.RefreshTime.Hours,
                    RefreshMinutes = screen.RefreshTime.Minutes,
                    RefreshSeconds = screen.RefreshTime.Seconds
                };

                return View(model);
            }
            catch (Exception e)
            {
                Request.Flash("error", Resources.Resources.SevereError + ": " + e.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                var screen = _screenRepository.GetById(new Guid(collection["Id"]));

                var group = _screenGroupRepository.GetById(new Guid(collection["GroupId"]));

                group.Screens.Remove(screen);

                screen.Title = collection["Title"];
                screen.Location = collection["Location"];
                screen.HostName = collection["HostName"];
                screen.ModifiedBy = GetCurrentUser().User.ToString();
                screen.RefreshTime = new RefreshTime(Convert.ToInt32(collection["RefreshHours"]), Convert.ToInt32(collection["RefreshMinutes"]), Convert.ToInt32(collection["RefreshSeconds"]));
                
                screen.Group = group;

                _screenRepository.Update(screen);

                Request.Flash("success", Resources.Resources.Screen + " " + Resources.Resources.Updated);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Request.Flash("error", Resources.Resources.SevereError + ": " + e.Message);

                return RedirectToAction("Index");
            }
        }
    }
}