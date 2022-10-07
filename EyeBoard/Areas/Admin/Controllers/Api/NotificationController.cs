using EyeBoard.Logic.Repositories;
using EyeBoard.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

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
                string title = item.Title;
                // Check for placeholders
                var paramx = new Regex(@"{([^}]+)}", RegexOptions.Compiled);
                var paramMatches = paramx.Matches(item.Title);
                foreach (var paramMatch in paramMatches)
                {
                    // Check for SQL placeholders
                    var sqlx = new Regex(@"[[a-zA-Z_+0-9]+]::[[a-zA-Z_0-9]+]", RegexOptions.Compiled);
                    var sqlMatches = sqlx.Matches(paramMatch.ToString());
                    foreach (var sqlMatch in sqlMatches)
                    {
                        // Execute query
                        string result = ExecuteSQL(sqlMatch.ToString());
                        title = title.Replace(paramMatch.ToString(), result);
                    }
                }

                notifications.Add(new NotificationViewModel()
                {
                    Id = item.Id,
                    Title = title,
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

        private string ExecuteSQL(string s)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["db2"].ConnectionString;


            

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                string[] separators = new string[] { "::" };
                string[] parameters = s.Split(separators, StringSplitOptions.None);
                
                var query = "SELECT " + parameters[1] + " FROM " + parameters[0];

                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    decimal result = Convert.ToDecimal(command.ExecuteScalar());

                    int amount = (int)Math.Round(result, 0);

                    return amount.ToString("N0", CultureInfo.CreateSpecificCulture("nl-NL"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return "Error in SQL";
        }
    }
}
