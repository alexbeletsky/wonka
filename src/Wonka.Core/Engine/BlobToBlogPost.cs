using System;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Wonka.Core.Engine.Model;
using Wonka.Core.Github.Model;

namespace Wonka.Core.Engine
{
    internal class BlobToBlogPost
    {
        public BlogPost Process(Blob blob)
        {
            string content = blob.Content;

            if (blob.Encoding == "base64")
            {
                content = Encoding.UTF8.GetString(Convert.FromBase64String(content));
            }

            return ExtractTitleAndBodyFromContent(content);
        }

        private BlogPost ExtractTitleAndBodyFromContent(string content)
        {
            var document = new HtmlDocument();
            document.LoadHtml(content);

            string title = document.DocumentNode.DescendantNodes().Single(n => n.Name == "title").InnerText;
            string body = document.DocumentNode.DescendantNodes().Single(n => n.Name == "body").InnerHtml;

            return new BlogPost {Title = title, Body = body};
        }
    }
}