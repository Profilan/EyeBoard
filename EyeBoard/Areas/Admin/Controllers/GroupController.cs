using EyeBoard.Logic.Models;
using EyeBoard.Logic.Repositories;
using EyeBoard.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EyeBoard.Areas.Admin.Controllers
{
    [Authorize]
    public class GroupController : BaseController
    {
        private readonly ScreenGroupRepository _screenGroupRepository;
        private readonly MediaRepository _mediaRepository;

        public GroupController() : base()
        {
            _screenGroupRepository = new ScreenGroupRepository();
            _mediaRepository = new MediaRepository();
        }

        // GET: Group
        public ActionResult Index()
        {
            var items = _screenGroupRepository.List();

            var groups = new List<GroupViewModel>();
            foreach (var item in items)
            {
                groups.Add(new GroupViewModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    CreatedBy = GetCurrentUser().DisplayName,
                    Modified = item.Modified,
                });

            }

            return View(groups);
        }

        public ActionResult Create()
        {
            var groupViewModel = new GroupViewModel()
            {
                UploadUrl = "/api/upload/presentation",
                MaxFileSize = 512,
                AcceptFileTypes = @"/(\.|\/)(ppt?x)$/i",
                Media = new List<Medium>(),
                UserId = GetCurrentUser().Id
            };

            return View(groupViewModel);
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var group = ScreenGroup.Create(collection["Title"]);
                group.CreatedBy = GetCurrentUser().Id;
                group.ModifiedBy = group.CreatedBy;
                _screenGroupRepository.Insert(group);

                /*
                var presentationUrls = collection["Presentations[]"].Split(',');
                var media = new List<Presentation>();
                foreach (var url in presentationUrls)
                {
                    var info = new FileInfo(url);
                    var medium = Presentation.Create(info.Name, DateTime.Now, DateTime.MaxValue, 0, url);
                    _mediaRepository.Insert(medium);
                    group.Media.Add(medium);
                }
                _screenGroupRepository.Update(group);
                */

                Request.Flash("success", Resources.Resources.Group + " " + Resources.Resources.Saved);

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
                var group = _screenGroupRepository.GetById(id);

                var model = new GroupViewModel()
                {
                    Id = group.Id,
                    Title = group.Title
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
                var group = _screenGroupRepository.GetById(new Guid(collection["Id"]));

                group.Title = collection["Title"];
                group.ModifiedBy = GetCurrentUser().Id;

                _screenGroupRepository.Update(group);

                Request.Flash("success", Resources.Resources.Group + " " + Resources.Resources.Updated);

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