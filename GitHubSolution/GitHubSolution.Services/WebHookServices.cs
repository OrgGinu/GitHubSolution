using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Security.Cryptography;
using GitHubSolution.Services.Contracts;

namespace GitHubSolution.Services
{
    public class WebHookServices : IWebHookServices
    {
        private const string _sha1Prefix = "sha1=";
        private readonly string _secret;
        private readonly IConfiguration _config;
        public WebHookServices(IConfiguration config)
        {
            _config = config;
            _secret = _config["GitHubSetting:WebHookSecret"];
        }
        public bool IsValidGitHubWebHookRequest(string payload, string signatureWithPrefix)
        {
            try
            {
                if (signatureWithPrefix.StartsWith(_sha1Prefix, StringComparison.OrdinalIgnoreCase))
                {
                    var signature = signatureWithPrefix.Substring(_sha1Prefix.Length);
                    var secret = Encoding.ASCII.GetBytes(_secret); //get secret from app settings
                    var payloadBytes = Encoding.ASCII.GetBytes(payload);

                    using (var hmSha1 = new HMACSHA1(secret))
                    {
                        var hash = hmSha1.ComputeHash(payloadBytes);
                        var hashString = ToHexString(hash);

                        if (hashString.Equals(signature))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }

        private object ToHexString(byte[] hash)
        {
            var builder = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
            {
                builder.AppendFormat("{0:x2}", b);
            }

            return builder.ToString();
        }
    }
}
