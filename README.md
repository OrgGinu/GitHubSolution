# GitHubAutomate

GitHubAutomate is a simple web service that listens for organization events to know when a repository has been created. When the repository is created, this service automates the protection of the default branch. It will also create an issue with @mention that outlines the protections that were added.

# Usage
Change the following configuration values in the AppSettings to reflect your GitHub environment details

* RepoName: "YOUR REPO NAME"
* Token: "PERSONAL ACCESS TOKEN"
* Owner: "REPO OWNER"
* WebHookSecret: "WEBHOOK SECRET"

Set up a WebHook in the desired GitHub organization. 
https://docs.github.com/en/developers/webhooks-and-events/creating-webhooks
* Note that the Payload URL should match your PI endpoint
* Select the individual events radio button and check repositories so that the webhook triggers on repository events
* Set the content type to be application/json
* Save the Webhook

Test the App by creating a new repository in the organization and see that the branch protection is applied to the default brnach as well as an issue is created under the repo.
