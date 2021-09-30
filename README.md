# Bolt.Template.WebApi
Template for generating net core library with github actions and tests

## How to use this template
First you need to install this template in your machine.

```
  // this will install the latest version of the template in your machine
  dotnet new -i Bolt.Template.Lib
```

Then you are ready to create new your own project using this template

```
  dotnet new bolt-lib --name <NameOfYourProject>
```

Thats it now you have a solution ready to start developing your awesome library. When you commit the code in gitrepo your code will be build/test and then pushed to nuget with version number you set. You need to define a secret in gihub action named "nugetkey" to push your packages to github.
