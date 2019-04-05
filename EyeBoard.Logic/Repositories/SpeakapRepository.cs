using EyeBoard.Logic.Models;
using Newtonsoft.Json;
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
        private readonly RestClient client = new RestClient();
        private readonly RestRequest request = new RestRequest(Method.GET);

        public SpeakapRepository()
        {
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", "Bearer 2dcc4f63a00008c4_c217339e61e08a7512ab261fe3a11f6019a65a1b0e0affee4967bf2002c96ca8");
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public SpeakapMessage GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(SpeakapMessage entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SpeakapMessage> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SpeakapMessage> List()
        {
            var items = new List<SpeakapMessage>();

            client.BaseUrl = new Uri("https://api.speakap.io/networks/2caf8309fb0004cc/messages/");
           
            IRestResponse response = client.Execute(request);
            
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                SpeakapMessagesApiModel messages = JsonConvert.DeserializeObject<SpeakapMessagesApiModel>(response.Content);

                
                foreach (var messageLink in messages.Links.Messages)
                {
                    client.BaseUrl = new Uri("https://api.speakap.io" + messageLink.Href);

                    response = client.Execute(request);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        SpeakapMessageApiModel message = JsonConvert.DeserializeObject<SpeakapMessageApiModel>(response.Content);

                        client.BaseUrl = new Uri("https://api.speakap.io" + message.Links.Author.Href);

                        response = client.Execute(request);

                        SpeakapAuthorApiModel author = JsonConvert.DeserializeObject<SpeakapAuthorApiModel>(response.Content);

                        var images = new List<string>();
                        if (message.Links.Images != null)
                        {
                            foreach (var imageLink in message.Links.Images)
                            {
                                client.BaseUrl = new Uri("https://api.speakap.io" + imageLink.Href);

                                response = client.Execute(request);

                                SpeakapImageApiModel image = JsonConvert.DeserializeObject<SpeakapImageApiModel>(response.Content);

                                client.BaseUrl = new Uri(image.SpeakapFile.DisplayUrls.TimelinePreviewSmall);
                                response = client.Execute(request);

                                images.Add(Convert.ToBase64String(response.RawBytes));


                            }
                        }

                        var fullTextWords = message.Body.Split(new char[0]);
                        var fullText = "";
                        if (fullTextWords.Length > 15)
                        {
                            for (int i = 0; i < 15; i++)
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
    }
}
