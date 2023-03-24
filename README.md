# HvZCase Backend
This repository handles the backend for a game called "Human vs Zombies", which is a more involved version of Tag, in which zombies try to catch humans and humans do their best to survive. Players are able to create squads together and chat together. 

## How to run
To run this locally, you need to have [SSMS](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16) installed, you need to have a [Pusher](https://pusher.com/) account and Channel created and you need to have a keycloak service running locally or online. There needs to be two Realm Roles set to access everything, "user" and "admin. Set the connection string to be your local SSMS connection string, and add the Pusher secrets "PUSHER_APP_ID", "PUSHER_APP_KEY", "PUSHER_APP_SECRET" to your local secrets manager. In visual studio 2022, this would be done using the commands 
cd ./HvZAPI/
dotnet user-secrets "PUSHER_APP_ID" <appId> --project "HvZAPI.csproj"
dotnet user-secrets "PUSHER_APP_KEY" <appKey> --project "HvZAPI.csproj"
dotnet user-secrets "PUSHER_APP_SECRET" <appSecret> --project "HvZAPI.csproj"

You will also need to set the secrets for your keycloak URIs with 
dotnet user-secrets "ISSUER_URI" <issuerUri> --project "HvZAPI.csproj"
dotnet user-secrets "KEY_URI" <keyUri> --project "HvZAPI.csproj"

Once this is done, you can now enter "add-migration Initial" and then "update-database" into the PM console to create the initial database with some seeded data. 

## How to deploy on Azure. 
To deploy on Azure, you will need to create a SQLDatabase on Azure and allow access from Web Apps, for sake of ease also add your own local client as allowed. Once this has been created, you can get the connection string to your database, and replace the local one in the appsettings.json file with said link. Run the migration commands above again, making sure that your migration folder is currently empty, and then remove your connection string from the appsettings.json file so that it can not be seen in any source control. Make sure to keep the "SERVER_CONNECTION" option in the appsettings.json file, but you can just have it point to an empty string (or to your local Db for testing purposes).

Once this is done, you will need to create a [Docker](https://hub.docker.com/) image for your app. Once you've have the docker image, push it to the hub and then you can create a Web App on Azure, picking to build from docker hub image. Link the one you have created, and let it create the Web App. Once you've created the Web App, you'll need to set up the secrets in a similar way as we did locally, but instead of running the commands locally, you'll enter them into your Web App configuration. Enter the connection string under the "Connection Strings" and the other secrets under "Application Settings". 

Once this is done, you should have your API up and running. You can check if it works by going to your "<deployedLink>/api/v1/game" to see if it returns anything. 

## Made with 
The backend was made using ASP.NET, SSMS, Docker and Azure. Documentation is handled with Swagger. 

## Made by
Mattias Smedman

Danielle Hamrin

Keman Nguyen