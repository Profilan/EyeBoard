using EyeBoard.Logic.Models;
using EyeBoard.Logic.Repositories;
using EyeBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EyeBoard.Controllers.Admin
{
    [Authorize(Roles = "superuser")]
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
            return View(new ScreenViewModel()
            {
                Groups = _screenGroupRepository.List().OrderBy(x => x.Title)
            });
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var group = _screenGroupRepository.GetById(new Guid(collection["GroupId"]));
                var screen = Screen.Create(collection["Title"], collection["Location"], group);
                screen.CreatedBy = GetCurrentUser().Id;
                screen.ModifiedBy = screen.CreatedBy;
                screen.HostName = collection["HostName"];

                _screenRepository.Insert(screen);

                return RedirectToAction("Index");
            }
            catch
            {
                var model = new ScreenViewModel()
                {
                    Id = new Guid(collection["Id"]),
                    Title = collection["Title"],
                    Location = collection["Location"],
                    HostName = collection["HostName"],
                    Groups = _screenGroupRepository.List().OrderBy(x => x.Title)
                };
                return View(model);
            }
        }

        public ActionResult Edit(Guid id)
        {
            try
            {
                var screen = _screenRepository.GetById(id);

                var model = new ScreenViewModel()
                {
                    Id = screen.Id,
                    Title = screen.Title,
                    Location = screen.Location,
                    HostName = screen.HostName,
                    GroupId = screen.Group.Id,
                    Groups = _screenGroupRepository.List().OrderBy(x => x.Title)
                };

                return View(model);
            }
            catch
            {

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

                screen.Title = collection["Title"];
                screen.Location = collection["Location"];
                screen.HostName = collection["HostName"];
                screen.ModifiedBy = GetCurrentUser().Id;
                if (screen.Group.Id != group.Id)
                    screen.Group = group;

                _screenRepository.Update(screen);

                return RedirectToAction("Index");
            }
            catch
            {
                var group = _screenGroupRepository.GetById(new Guid(collection["GroupId"]));

                var model = new ScreenViewModel()
                {
                    Id = new Guid(collection["Id"]),
                    Title = collection["Title"],
                    Location = collection["Location"],
                    HostName = collection["HostName"],
                    GroupId = group.Id,
                    Groups = _screenGroupRepository.List().OrderBy(x => x.Title)
                };
                return View(model);
            }
        }
    }
}