using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Wonka.Core.Github.Model;

namespace Wonka.Core.Github
{
    public class GithubAdapter : IGithubAdapter
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
                var url = string.Format("{0}/repos/{1}/{2}/git/refs", ApiBaseUri, _user, _repo);
                var response = client.DownloadString(url);

                return JsonConvert.DeserializeObject<IList<Reference>>(response);
            }
        }

        public Trees GetTrees(string sha)
        {
            using (var client = new WebClient())
            {
                var url = string.Format("{0}/repos/{1}/{2}/git/trees/{3}?recursive=1", ApiBaseUri, _user, _repo, sha);
                var response = client.DownloadString(url);

                return JsonConvert.DeserializeObject<Trees>(response);
            }
        }

        public Blob GetBlob(string sha)
        {
            using (var client = new WebClient())
            {
                var url = string.Format("{0}/repos/{1}/{2}/git/blobs/{3}", ApiBaseUri, _user, _repo, sha);
                var response = client.DownloadString(url);

                return JsonConvert.DeserializeObject<Blob>(response);
            }
        }
    }
}