using EyeBoard.Areas.Admin.Models;
using EyeBoard.Logic.Models;
using EyeBoard.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EyeBoard.Areas.Admin.Controllers
{
    [Authorize(Roles = "GRolNarrowcastBeheerder, GRolNarrowcastRedacteur")]
    public class VideoController : BaseController
    {
        private readonly MediaRepository _mediaRepository = new MediaRepository();
        private readonly ScreenGroupRepository _screenGroupRepository = new ScreenGroupRepository();

        public ActionResult Index()
        {
            var videos = _mediaRepository.ListByUser(GetCurrentUser().User.ToString());

            var viewModel = new FileInfoModel()
            {
                UploadUrl = "/api/upload/video",
                MaxFileSize = 512,
                AcceptFileTypes = @"/(\.|\/)(mp4|mkv|avi)$/i",
                Media = videos.Where(x => x.GetType().Name == "Movie"),
                UserId = GetCurrentUser().User.ToString()
            };

            return View(viewModel);
        }


        public ActionResult Edit(Guid id)
        {
            try
            {
                var item = _mediaRepository.GetById(id);

                var viewModel = new MediumViewModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Url = item.Url,
                    Groups = _screenGroupRepository.List(),
                    SelectedGroups = item.Groups
                };

                return View(viewModel);
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
                var presentation = _mediaRepository.GetById(new Guid(collection["Id"]));

                var selectedGroups = collection["Groups[]"];
                var groups = _screenGroupRepository.List();
                if (!String.IsNullOrEmpty(selectedGroups))
                {
                    foreach (var group in groups)
                    {
                        // Is the group checked
                        if (IsChecked(group, selectedGroups))
                        {
                            if (!presentation.Groups.Contains(group))
                            {
                                group.AddNewPresentation(presentation);
                                _screenGroupRepository.Update(group);
                            }
                        }
                        else
                        {
                            if (presentation.Groups.Contains(group))
                            {
                                group.DeletePresentation(presentation);
                                _screenGroupRepository.Update(group);
                            }
                        }

                    }
                }
                else
                {
                    foreach (var group in groups)
                    {
                        if (presentation.Groups.Contains(group))
                        {
                            group.DeletePresentation(presentation);
                            _screenGroupRepository.Update(group);
                        }
                    }
                }

                Request.Flash("success", Resources.Resources.Presentation + " " + Resources.Resources.Updated);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Request.Flash("error", Resources.Resources.SevereError + ": " + e.Message);

                return RedirectToAction("Index");
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