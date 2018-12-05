using EyeBoard.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EyeBoard.Logic.Repositories
{
    public class FeedRepository
    {
        public IEnumerable<Feed> ListByUrl(string url)
        {
            XmlDocument rssXmlDoc = new XmlDocument();

            // Load the RSS file from the RSS URL
            rssXmlDoc.Load(url);

            // Parse the Items in the RSS file
            XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");

            StringBuilder rssContent = new StringBuilder();

            // Iterate through the items in the RSS file
            var items = new List<Feed>();
            foreach (XmlNode rssNode in rssNodes)
            {
                XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                string title = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("description");
                string description = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("enclosure");
                string imageUrl = rssSubNode != null ? rssSubNode.Attributes["url"].Value : "";

                items.Add(new Feed()
                {
                    Title = title,
                    Description = description,
                    ImageUrl = imageUrl
                });
            }

            return items;
        }
    }
}
