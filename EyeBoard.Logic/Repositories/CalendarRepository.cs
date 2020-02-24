using EyeBoard.Logic.Models;
using Newtonsoft.Json;
using Profilan.SharedKernel;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Repositories
{
    public class CalendarRepository : IRepository<OutlookEvent, string>
    {
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public OutlookEvent GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(OutlookEvent entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OutlookEvent> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OutlookEvent> List()
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.Local;
            DateTime now = DateTime.Now;

            var url = "https://ex-eek-zwd-04.zwd.deeekhoorn.com/api/v2.0/me/events?startDateTime=" + now.ToString("yyyy-MM-ddT00:00") + "&endDateTime=" + now.AddMonths(2).ToString("yyyy-MM-ddT00:00");
            var client = new RestClient(url);
            client.Authenticator = new NtlmAuthenticator(@"EEKZWD\Narrowcasting", "4qhFgbrvxs");
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            CalendarApiModel model = JsonConvert.DeserializeObject<CalendarApiModel>(response.Content);

            List<OutlookEvent> events = new List<OutlookEvent>();
            foreach (EventApiModel outlookEvent in model.Events)
            {
                var start = outlookEvent.Start.DateTime;
                var end = outlookEvent.End.DateTime;
                if (!outlookEvent.IsAllDay)
                {
                    start = start.Add(timeZoneInfo.BaseUtcOffset);
                    end = end.Add(timeZoneInfo.BaseUtcOffset);
                }
                events.Add(new OutlookEvent()
                {
                    Id = outlookEvent.Id,
                    Subject = outlookEvent.Subject,
                    IsAllDay = outlookEvent.IsAllDay,
                    IsCancelled = outlookEvent.IsCancelled,
                    Start = start,
                    End = end
                });
            }

            return events;
        }

        public void Update(OutlookEvent entity)
        {
            throw new NotImplementedException();
        }
    }
}
