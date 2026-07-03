# play-around-with-azure
At some point Azure came up. In the first years, I thought this would be another temporary hype and that companies would be smart enough not to bind themselves to these kind of environments. But I was wrong about it. Even despite some political tensions between Europe and United States, companies are moving more and more into Azure. So time to try this out from scratch...

## Azure Account
It starts with creating an account. Despite the required credit card registration, there are free options and even a free option that comes with some free credits that should come in hand while try this stuff out. So it started with creating a specific account (email adress), like hotmail or outlook via: [live.com](https://live.com/).
After this, I used that account to create an [Free Azure account](https://azure.microsoft.com/en-us/pricing/purchase-options/azure-account). You have to register things like phone number and (even worse) your credit card. But let's just trust Microsoft for this one and get the thing running.

So after this, I can use it to access my own account via the [Azure Portal](https://portal.azure.com/#home).
<img width="1261" height="1088" alt="image" src="https://github.com/user-attachments/assets/74c109b0-fbd6-4f9f-a680-3330ba9484d2" />

## Azure Subscription and Azure Resource Group
Within that account, it already created a subscription, having defailt name: `Azure subscription 1`.
And then the first next step is to create a resource group within that subscription, which I named `MyFirstResourceGroup`.
I decided to host it in Belgium Central estimating that this is the closest location and therefor the best for me.
No need yet for Tags or other advanced stuff.
<img width="720" height="440" alt="image" src="https://github.com/user-attachments/assets/79dc9733-9414-4b01-afb5-f544fe510e87" />

## Create the simplest Azure Web App
Then it's time to make a very simple static HTML page and host it in Azure.
First I added [a static file](https://github.com/ivoraedts/play-around-with-azure/blob/develop/01-app-service/static-html-page/index.html) in this Github repository to host.
Then it is time to add a Web App as following:

<img width="736" height="1252" alt="image" src="https://github.com/user-attachments/assets/3aedee47-021e-4367-b3d5-8adb2c2b43db" />

followed by:

<img width="664" height="1001" alt="image" src="https://github.com/user-attachments/assets/b6de025f-9744-4716-a2ce-1b559531aa5d" />

then you have to wait for some minute or so.

<img width="814" height="471" alt="image" src="https://github.com/user-attachments/assets/faf43a4b-c34d-4525-aadf-fdac40a027d0" />

and then it's done:

<img width="809" height="674" alt="image" src="https://github.com/user-attachments/assets/39116e37-64b3-4fe3-971b-7dd36038fae1" />


as a result Azure creates a workflow file.
However these files assume the content to be downloaded is in the root.
This is not the case now, so we need to modify the file, so we can indicate the path to download the file(s):
<img width="995" height="1263" alt="image" src="https://github.com/user-attachments/assets/c85b2a43-4398-43a1-89d1-db0d8cc3f744" />

Once this is pushed to the GitHub Repository (in the given branch), the deployment will be done again, which is visible in [actions](https://github.com/ivoraedts/play-around-with-azure/actions).

And finally we can see the first result:
<img width="699" height="212" alt="image" src="https://github.com/user-attachments/assets/e1bb9cce-c89b-49e5-852b-df366add6a6f" />

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







