using EyeBoard.Logic.Models;
using Newtonsoft.Json;
using NHibernate;
using Profilan.SharedKernel;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeBoard.Logic.Repositories
{
    public class SpeakapRepository : IRepository<SpeakapMessage, string>
    {
        private readonly RestClient client;
        private readonly RestRequest request = new RestRequest();

        private string authorization = "Bearer 304f756b8a000bbc_74c16a7dedad5ca81f4edc54326a1cb2ee94d0deb1bbc5b78858d8a36300af28";

        public SpeakapRepository()
        {
            var options = new RestClientOptions();
            client = new RestClient(options);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", "Bearer 304f756b8a000bbc_74c16a7dedad5ca81f4edc54326a1cb2ee94d0deb1bbc5b78858d8a36300af28");
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public SpeakapMessage GetById(string id)
        {
            throw new NotImplementedException();
        }

        public SpeakapGroup GetGroupById(string id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var item = session.Get<SpeakapGroup>(id);

                    return item;
                }
            }
        }

        public void Insert(SpeakapMessage entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SpeakapMessage> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SpeakapMessage> ListByAge(DateTime date)
        {
            var items = new List<SpeakapMessage>();

            var groups = ListGroups().Where(x => x.Enabled == true);
            foreach(var group in groups)
            {
                SpeakapEmbeddedMessagesApiModel messages = GetEmbeddedMessages(group.Id);

                var images = new List<string>();
                foreach (var message in messages.Embedded.Messages)
                {
                    /*
                    if (message.Links.Images != null)
                    {
                        foreach (var imageLink in message.Links.Images)
                        {
                            client.BaseUrl = new Uri("https://api.speakap.io" + imageLink.Href);

                            var response = client.Execute(request);

                            SpeakapImageApiModel image = JsonConvert.DeserializeObject<SpeakapImageApiModel>(response.Content);

                            if (image.SpeakapFile != null)
                            {
                                client.BaseUrl = new Uri(image.SpeakapFile.DisplayUrls.TimelinePreviewSmall);
                                response = client.Execute(request);

                                images.Add(Convert.ToBase64String(response.RawBytes));

                            }
                        }
                    }
                    */

                    if (message.Embeds != null)
                    {
                        images.Add(message.Embeds[0].DesktopThumbnailUrl);
                    }
 
                    var fullTextWords = message.Body.Split(new char[0]);
                    var fullText = "";
                    if (fullTextWords.Length > 30)
                    {
                        for (int i = 0; i < 30; i++)
                        {
                            fullText += fullTextWords[i] + " ";
                        }
                        fullText += "...";
                    }
                    else
                    {
                        fullText = message.Body;
                    }

                    SpeakapAuthorApiModel author = GetAuthor(message.Links.Author.Href);

                    items.Add(new SpeakapMessage()
                    {
                        Id = message.Id,
                        FullText = fullText,
                        Author = author.FullName,
                        Likes = message.Likes,
                        Images = images,
                        Created = Convert.ToDateTime(message.Created)
                    });

                }
            }

            return items;
        }

        public IEnumerable<SpeakapMessage> List()
        {
            var items = new List<SpeakapMessage>();

            var dateNow = DateTime.Now.AddMonths(-2);

            client.Options.BaseUrl = new Uri("https://api.speakap.io/networks/2caf8309fb0004cc/messages/?data_since=" + dateNow.ToString("yyyy-MM-dd"));

            var response = client.Get(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                SpeakapMessagesApiModel messages = JsonConvert.DeserializeObject<SpeakapMessagesApiModel>(response.Content);


                foreach (var messageLink in messages.Links.Messages)
                {
                    client.Options.BaseUrl = new Uri("https://api.speakap.io" + messageLink.Href);

                    response = client.Get(request);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        SpeakapMessageApiModel message = JsonConvert.DeserializeObject<SpeakapMessageApiModel>(response.Content);

                        client.Options.BaseUrl = new Uri("https://api.speakap.io" + message.Links.Author.Href);

                        response = client.Get(request);

                        SpeakapAuthorApiModel author = JsonConvert.DeserializeObject<SpeakapAuthorApiModel>(response.Content);

                        var images = new List<string>();
                        if (message.Links.Images != null)
                        {
                            foreach (var imageLink in message.Links.Images)
                            {
                                client.Options.BaseUrl = new Uri("https://api.speakap.io" + imageLink.Href);

                                response = client.Get(request);

                                SpeakapImageApiModel image = JsonConvert.DeserializeObject<SpeakapImageApiModel>(response.Content);

                                client.Options.BaseUrl = new Uri(image.SpeakapFile.DisplayUrls.TimelinePreviewSmall);
                                response = client.Get(request);

                                images.Add(Convert.ToBase64String(response.RawBytes));


                            }
                        }

                        var fullTextWords = message.Body.Split(new char[0]);
                        var fullText = "";
                        if (fullTextWords.Length > 30)
                        {
                            for (int i = 0; i < 30; i++)
                            {
                                fullText += fullTextWords[i] + " ";
                            }
                            fullText += "...";
                        }
                        else
                        {
                            fullText = message.Body;
                        }


                        items.Add(new SpeakapMessage()
                        {
                            Id = message.Id,
                            FullText = fullText,
                            Author = author.FullName,
                            Likes = message.Likes,
                            Images = images,
                            Created = Convert.ToDateTime(message.Created)
                        });
                    }
                }
            }

            return items;
        }

        public void Update(SpeakapMessage entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateGroup(SpeakapGroup entity)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(entity);
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<SpeakapGroup> ListGroups()
        {
            var options = new RestClientOptions("https://api.speakap.io/networks/2caf8309fb0004cc/groups/?limit=100");
            var client = new RestClient(options);
            var request = new RestRequest();

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", authorization);

            var response = client.Get(request);

            var groups = new List<SpeakapGroup>();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                SpeakapGroupsApiModel model = JsonConvert.DeserializeObject<SpeakapGroupsApiModel>(response.Content);

                using (ISession session = SessionFactory.GetNewSession("db1"))
                {
                    foreach (var groupLink in model.Links.Groups)
                    {
                        client.Options.BaseUrl = new Uri("https://api.speakap.io" + groupLink.Href);

                        response = client.Execute(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            SpeakapGroupApiModel group = JsonConvert.DeserializeObject<SpeakapGroupApiModel>(response.Content);

                            using (ITransaction transaction = session.BeginTransaction())
                            {
                                SpeakapGroup item = session.Get<SpeakapGroup>(group.Id);

                                if (item == null)
                                {
                                    item = new SpeakapGroup()
                                    {
                                        Id = group.Id,
                                        Name = group.Name,
                                        Description = group.Description != null ? group.Description : "",
                                        Enabled = false
                                    };

                                    session.SaveOrUpdate(item);
                                    transaction.Commit();
                                }

                                groups.Add(item);
                            }
                        }
                    }
                }
            }

            return groups;
        }

        public SpeakapMessagesApiModel GetMessages(string group)
        {
            var dateNow = DateTime.Now.AddMonths(-2);

            var client = new RestClient("https://api.speakap.io/networks/2caf8309fb0004cc/timeline/?group=" + group);
            var request = new RestRequest();

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", authorization);

            var response = client.Get(request);

            SpeakapMessagesApiModel messages = JsonConvert.DeserializeObject<SpeakapMessagesApiModel>(response.Content);

            return messages;

        }

        public SpeakapEmbeddedMessagesApiModel GetEmbeddedMessages(string group)
        {
            var dateNow = DateTime.Now.AddMonths(-2);

            var options = new RestClientOptions("https://api.speakap.io/networks/2caf8309fb0004cc/timeline/?embed=messages.images&group=" + group);
            var client = new RestClient(options);
            var request = new RestRequest();

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", authorization);

            var response = client.Get(request);

            SpeakapEmbeddedMessagesApiModel messages = JsonConvert.DeserializeObject<SpeakapEmbeddedMessagesApiModel>(response.Content);

            return messages;

        }

        public SpeakapAuthorApiModel GetAuthor(string href)
        {
            var options = new RestClientOptions("https://api.speakap.io" + href);
            var client = new RestClient(options);
            var request = new RestRequest();

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", authorization);

            var response = client.Get(request);

            SpeakapAuthorApiModel author = JsonConvert.DeserializeObject<SpeakapAuthorApiModel>(response.Content);

            return author;

        }
    }
}
