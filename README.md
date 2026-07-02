# play-around-with-azure
At some point Azure came up. In the first years, I thought this would be another temporary hype and that companies would be smart enough not to bind themselves to these kind of environments. But I was wrong about it. Even despite some political tensions between Europe and United States, companies are moving more and more into Azure. So time to try this out from scratch...

## Azure Account
It starts with creating an account. Despite the required credit card registration, there are free options and even a free option that comes with some free credits that should come in hand while try this stuff out. So it started with creating a specific account (email adress), like hotmail or outlook via: [live.com](live.com).
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










