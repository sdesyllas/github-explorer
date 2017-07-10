# github-explorer 
![alt tag](http://spyrosteamcity.uksouth.cloudapp.azure.com/app/rest/builds/buildType:(id:GithubExplorer_Build)/statusIcon)

ASP.NET MVC application for user profile search and repositories listing

This project is using :

* C#
* ASP.net
* MVC 5
* Bower
* SimpleInjector
* NUnit
* Moq
* FluentAssertions
* nBuilder
*gulp

To use:
restore nuget packages, install bower packages, install npm packages for gulp and run gulpfile to minify css/js.

For the .Net Core version of the project please check this [fork](https://github.com/theshoreditch/github-explorer)

# Task
Create an ASP.Net MVC website with a page containing a text box to enter a name in and a submit button to search GitHub for the name.

Have the back end call the GitHub users API (e.g. https://api.github.com/users/robconery) and get the users name, location and avatar url from the returned json. Use the repos_url value to get a list of all the repos for the user.

On the results page, show the username, location, avatar and the 5 repos with the highest stargazer_count.

Use this opportunity to show us what you know, even if you wouldn’t ordinarily use the concepts in such a trivial example.

Upload your work to GitHub and send us the url of the repository. If you don’t have a GitHub account you can register for a free one at https://github.com/join. Please make sure that your repository is a public one.
