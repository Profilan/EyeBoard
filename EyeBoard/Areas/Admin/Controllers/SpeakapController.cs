using EyeBoard.Areas.Admin.Models;
using EyeBoard.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EyeBoard.Areas.Admin.Controllers
{
    [Authorize(Roles = "GRolNarrowcastBeheerder, GRolNarrowcastRedacteur")]
    public class SpeakapController : Controller
    {
        private readonly SpeakapRepository speakapRepository = new SpeakapRepository();

        public ActionResult Index()
        {
            var items = speakapRepository.ListGroups();

            var groups = new List<SpeakapGroupViewModel>();
            foreach (var group in items)
            {
                groups.Add(new SpeakapGroupViewModel()
                {
                    Id = group.Id,
                    Name = group.Name,
                    Description = group.Description,
                    Enabled = group.Enabled
                });
            }

            return View(groups);
        }

        public ActionResult Edit(string id)
        {
            var item = speakapRepository.GetGroupById(id);

            var group = new SpeakapGroupViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Enabled = item.Enabled
            };

            return View(group);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection data)
        {
            try
            {
                var group = speakapRepository.GetGroupById(data["Id"]);

                var enabled = true;
                if (data["Enabled"] == "false")
                {
                    enabled = false;
                }
                group.Enabled = enabled;

                speakapRepository.UpdateGroup(group);

                Request.Flash("success", "Groep is opgeslagen");

                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                Request.Flash("error", "Ernstige fout: " + e.Message);

                return RedirectToAction("Index");
            }
        }
    }
}