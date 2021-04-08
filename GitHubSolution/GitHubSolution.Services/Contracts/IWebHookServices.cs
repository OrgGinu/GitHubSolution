namespace GitHubSolution.Services.Contracts
{
    public interface IWebHookServices
    {
        /// <summary>
        /// Check for a valid signature
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="signatureWithPrefix"></param>
        /// <returns></returns>
        bool IsValidGitHubWebHookRequest(string payload, string signatureWithPrefix);
    }
}
