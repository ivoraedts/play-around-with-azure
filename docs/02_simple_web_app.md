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