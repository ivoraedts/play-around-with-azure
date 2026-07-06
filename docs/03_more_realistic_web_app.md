## Create a more realistic Azure Web App
So this time, I am going to combine a vue.js front-end with a API.net project.
I added some folders to store the projects and did some scaffolding for both.

### ASP.NET WEB.API (.NET10)

Then, next to the frond-end folder, it was time to create a back-end folder/project.
And all that I needed to do in PowerShell (from the earlier mentioned root-folder) was typing the following command:
```
dotnet new webapi -n Web.API
```

### Vue.JS via Vuetify

I wanted to start via [Vuetify](https://vuetifyjs.com/en/) via [some simple Vite command](https://vuetifyjs.com/en/getting-started/installation/#using-vite) working via NPM.
I created another folder for Vuetify and then executed this command:
```
npm create vuetify@latest
```
...and then via some wizard adding as few options as possible.
And directly after that, you can just run it from the `vuetify-project` folder (via the installed Vite tooling) via the following command.
```
npm run dev
```

### Create the .NET Web App in Azure
So again in the Azure Portal, via App Services and click + Create -> Web App, I entered the following:

<img width="748" height="1254" alt="image" src="https://github.com/user-attachments/assets/f9090ce3-4b4d-447d-a291-f94ae871cbe9" />

Followed by:

<img width="654" height="962" alt="image" src="https://github.com/user-attachments/assets/e2a511f5-cfad-47b4-bcca-bec457845639" />

Then I had to have patience for a minute.
And then I could navigate to the resource --> deployment center and than set Github as source, select the repository and branch before clicking Save:

<img width="1050" height="1261" alt="image" src="https://github.com/user-attachments/assets/7ac6595a-bb8a-4803-b4ea-82ccd5e49de4" />

As a result, Azure pushed another workflow into the Github repository.
And for this one, I need to fix the path, just like we did before for the simple HTML page.
In this case, we define that path in an environment variable, so we can use it twice.
Next to that we used the global runner workspace directory to keep the packaging step isolated from accidental local repository path conflicts.

<img width="1066" height="885" alt="image" src="https://github.com/user-attachments/assets/6e151254-53c2-439b-9789-aeeb02175fd3" />

Then after pushing all that to master, the action restarted.
Via the Overview of the related Resource page, you can find the default domain.

<img width="1666" height="328" alt="image" src="https://github.com/user-attachments/assets/26c249b3-0b9d-4e3e-9653-e62a4c59b70b" />

Then you add /weatherforecast (let's say hello-world data of a API.net project) to the URL and as a result you can test the web.api:

<img width="722" height="539" alt="image" src="https://github.com/user-attachments/assets/be9d6c2e-5cd3-4042-9579-5193ef4b57cd" />

### Create the VUE.JS Front-end Web App in Azure

Again, similar to added the Web App for the .NET API, I added a new Web App.
I used the same resource group, named it `vue-frontend` and picked the `Node 24 LTS` stack. (`Linux` and `Belgium Central`)
I similarly linked it via the Deployment Center to Github using the same repository.

<img width="1553" height="1009" alt="image" src="https://github.com/user-attachments/assets/3f7a8c57-3ee5-4aa0-b054-e334f865a276" />

As a result of linking it to the Github, Azure accesses the Github repository and creates another workflow.
Only this time I needed some more modifications to get things to work:

<img width="874" height="822" alt="image" src="https://github.com/user-attachments/assets/25c71934-c2c0-4514-bbb3-41b2e9166efa" />

So next to making sure the relative path is used instead of the root-path, there were some extra steps for pulling npm, granting executable permissions on hte local vite file that is used to build the code and then executing that code.

And then in order to make that Node.js stack to make it run the vue.js web app, we need to run a static file server.
For this we use PM2 as this is already built into the Azure Node image.

<img width="1071" height="575" alt="image" src="https://github.com/user-attachments/assets/6758f953-23a1-4215-b921-b1bb2d7241fc" />

And in the end that one finally runs:

<img width="952" height="732" alt="image" src="https://github.com/user-attachments/assets/22ab2904-3915-4f9c-98b7-b55de19e5c76" />

### Allow the Front-end to access the WEB.API
As the Front-end and the WEB.API are not yet integrated, it's time to allow the front-end to access the WEB.API.
So therefor, the front-end must be granted access in the WEB.API.

So this we arrange by adding a section to the default appsettings.json file:

<img width="316" height="117" alt="image" src="https://github.com/user-attachments/assets/ff03207a-a3f3-4538-bdcd-f1366cf42d11" />

Then we link it in code:

<img width="900" height="715" alt="image" src="https://github.com/user-attachments/assets/3ed5395c-0335-4094-bc7c-d6b7c0b1576d" />

And after pushing and deplying this. The final step will overwriting this setting with the front-end url in the environment in the Azure Portal.
So in the Azure Portal, I needed to select the dotnet-api resource and via Settings + Environment variables add this one:

<img width="1680" height="550" alt="image" src="https://github.com/user-attachments/assets/49a967f0-0c19-47ec-a1b3-858f8514ca05" />

(then click Apply and allow the service to restart)

### Access the WEB.API from the Front-end

So now the API is accessible for the front-end it's time to access it.
I did so by adding the URL to the API in files for `.env.development` for local development and `.env.production`.
This is the one for `.env.development`:
```
VITE_API_URL=http://localhost:5004
```

`.env.production` has that full URL containing `https://dotnet-api-...........belgiumcentral-01.azurewebsites.net/)`
And then asked AI to generate a component that access that weather controller in the API.

When rolling out, we need to be patient as the free tiers in Azure are slow...
After half a minute it looked like:

<img width="393" height="149" alt="image" src="https://github.com/user-attachments/assets/97f4f425-73ee-487a-982f-e4583d19f921" />

so the front-end was running and the dotnet-api was still loading and after a while it looked better:

<img width="486" height="525" alt="image" src="https://github.com/user-attachments/assets/1432401d-1fb1-401f-830f-6d7d0fd051b4" />

### Limit deployments

Each time I pushed something to master, all 3 Azure Web Apps were deploying (as seen on the [Actions page](https://github.com/ivoraedts/play-around-with-azure/actions))
So it was time to overcome this. And the easiest solution is to limit deplyment by specifying the paths.
See example:
```
on:
  push:
    branches:
      - master
    paths:
      - '01-app-service/static-html-page/**' # Only runs if files in this folder change
```
After modyfing this workflow (for the static-html-page), this Web App will only be redeployed if changes to files in the given folder (or subfolder) are commited to the master branch.