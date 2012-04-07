using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Wonka.Core.Github.Model;

namespace Wonka.Core.Github
{
    public class GithubAdapter
    {
        private readonly string _user;
        private readonly string _repo;
        
        private const string ApiBaseUri = "https://api.github.com";

        public GithubAdapter(string user, string repo)
        {
            _user = user;
            _repo = repo;
        }

        public IList<Reference> GetAllReferences()
        {
            using (var client = new WebClient())
            {
                var response = client.DownloadString(RefsUri);

                return JsonConvert.DeserializeObject<IList<Reference>>(response);
            }
        }

        protected Uri RefsUri { get { return new Uri(string.Format("{0}/repos/{1}/{2}/git/refs", ApiBaseUri, _user, _repo));}}
    }
}