# GitHubAutomate

GitHubAutomate is a simple web service that listens for GitHub organization events to know when a repository has been created. When the repository is created, this service automates the protection of the default branch of the repository. It will also create an issue with @mention that outlines the protections that were added.

Technologies & versions
*  ASP .NET Core Web API
*  .Net Core version 3.1

3rd party libraries used
*  Octokit.net - https://github.com/octokit/octokit.net

# Usage
Deploy the service to any desired platform like Azure, AWS or GCP and change the following configuration values in the AppSettings to reflect your GitHub environment details

* RepoName: "YOUR REPO NAME"
* Token: "PERSONAL ACCESS TOKEN"
* Owner: "REPO OWNER"
* WebHookSecret: "WEBHOOK SECRET"

Set up a WebHook in the desired GitHub organization. 
https://docs.github.com/en/developers/webhooks-and-events/creating-webhooks
* Note that the Payload URL should match your API endpoint
* Select the individual events radio button and check repositories so that the webhook triggers on repository events
* Set the content type to be application/json
* Save the Webhook

Test the App by creating a new repository in the organization and see that the branch protection is applied to the default branch as well as an issue is created under the repo.

How Webhook authentication works
https://docs.github.com/en/developers/webhooks-and-events/securing-your-webhooks
