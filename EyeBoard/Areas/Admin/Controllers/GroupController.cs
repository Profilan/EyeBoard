using EyeBoard.Logic.Models;
using EyeBoard.Logic.Repositories;
using EyeBoard.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Profilan.SharedKernel;

namespace EyeBoard.Areas.Admin.Controllers
{
    [Authorize(Roles = "GRolNarrowcastBeheerder")]
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

            var user = GetCurrentUser().User.ToString();

            var groups = new List<GroupViewModel>();
            foreach (var item in items)
            {
                groups.Add(new GroupViewModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    CreatedBy = GetCurrentUser().User.ToString(),
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
                UserId = GetCurrentUser().User.ToString()
            };

            return View(groupViewModel);
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var group = ScreenGroup.Create(collection["Title"]);
                
                if (collection["Theme"] != "0")
                {
                    group.Theme = Enum.Parse(typeof(Theme), collection["Theme"]).ToString();
                }
                else
                {
                    group.Theme = "Blue";
                }
                group.CreatedBy = GetCurrentUser().User.ToString();
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
                var videos = _mediaRepository.ListByUser(GetCurrentUser().User.ToString()).Where(p => p.GetType() == typeof(Movie));
                var presentations = _mediaRepository.ListByUser(GetCurrentUser().User.ToString()).Where(p => p.GetType() == typeof(Presentation));

                var model = new GroupViewModel()
                {
                    Id = group.Id,
                    Title = group.Title,
                    Videos = videos,
                    SelectedVideos = group.Media.Where(v => v.GetType() == typeof(Movie)),
                    Presentations = presentations,
                    SelectedPresentations = group.Media.Where(p => p.GetType() == typeof(Presentation)),
                    Theme = group.Theme == null ? Theme.Blue : (Theme)Enum.Parse(typeof(Theme), group.Theme)
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

                var selectedPresentations = collection["Presentations[]"];
                var presentations = _mediaRepository.List().Where(p => p.GetType() == typeof(Presentation));
                if (!String.IsNullOrEmpty(selectedPresentations))
                {
                    foreach (var presentation in presentations)
                    {
                        // Is the presentation checked
                        if (IsChecked(presentation, selectedPresentations))
                        {
                            if (!group.Media.Contains(presentation))
                            {
                                presentation.AddNewGroup(group);
                                _mediaRepository.Update(presentation);
                            }
                        }
                        else
                        {
                            if (group.Media.Contains(presentation))
                            {
                                presentation.DeleteGroup(group);
                                _mediaRepository.Update(presentation);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var presentation in presentations)
                    {
                        if (group.Media.Contains(presentation))
                        {
                            presentation.DeleteGroup(group);
                            _mediaRepository.Update(presentation);
                        }
                    }
                }

                var selectedVideos = collection["Videos[]"];
                var videos = _mediaRepository.List().Where(p => p.GetType() == typeof(Movie));
                if (!String.IsNullOrEmpty(selectedVideos))
                {
                    foreach (var video in videos)
                    {
                        // Is the presentation checked
                        if (IsChecked(video, selectedVideos))
                        {
                            if (!group.Media.Contains(video))
                            {
                                video.AddNewGroup(group);
                                _mediaRepository.Update(video);
                            }
                        }
                        else
                        {
                            if (group.Media.Contains(video))
                            {
                                video.DeleteGroup(group);
                                _mediaRepository.Update(video);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var video in videos)
                    {
                        if (group.Media.Contains(video))
                        {
                            video.DeleteGroup(group);
                            _mediaRepository.Update(video);
                        }
                    }
                }

                group.Title = collection["Title"];
                if (collection["Theme"] != "0")
                {
                    group.Theme = Enum.Parse(typeof(Theme), collection["Theme"]).ToString();
                }
                else
                {
                    group.Theme = "Blue";
                }
                group.ModifiedBy = GetCurrentUser().User.ToString();

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

        private bool IsChecked(Medium medium, string media)
        {
            foreach (var mediumId in media.Split(','))
            {
                if (medium.Id == new Guid(mediumId))
                {
                    return true;
                }
            }

            return false;
        }

    }
}