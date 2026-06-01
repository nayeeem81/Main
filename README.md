# Project Main (Branch: master)
# Solution Design Plan & Best Practices (.Net 8.0)

I started my code to build and run for a client (small shop). 

It was previously made for an online marketplace. That was in .NET Framework 4.6 where you must deploy the portal in a windows or cloud based (Microsoft Azure) in Platform Service as a web application. Deployment infrastructure/platform service is a windows server based. 

When I started looking at the code and searching on the internet, I found that Microsoft doesn’t have any support over the framework because of security vulnerability.  

I decided to do a migration of my code in .NET 8.0 because it has long term support plan and it is portable both in Windows and Linux servers. 

Also, the technology supports cross platforms including mobile devices and tablets.  

I started to create the architecture of the new solution, keeping in mind the best practices of design and architecture. Based on the research on the internet, I started the migration and tried to keep and reuse some parts of the past work.  

The primary objective was to make the code modular, reusable, separation of the concerns, and readable while doing the code for the solution. 

Another objective was to make sure; it is Linux deployable using containers and keeping the services completely separated from the presentation code (Web Project).  

My plan to separate the services from the presentation is to make the web project light weight and reus the same in different cross platform non-computer devices (Mobile, Tab). 

Microsoft already has their own technology for app development (Xamarin) which uses the API (Web API) project hosted on any server. My plan was to keep the code common for everyone (web, mobile, & tablet). 

In that consideration, my data infrastructure (Model, Repository) is self-registered. This project has zero dependency over any Data Transfer Object or View Model.  

Again, the service never communicates with the presentation layer with the Entity Models. They talk with Data Infrastructure in entity and business models.  

While communicating with the Presentation layer (web project), they use business objects which have no connection or tracking with Data Infrastructure. This technique provides the application database more secure because in any mistake, code doesn’t have any chances to alter or change.  

Since the Data Infrastructure is self-registered, we don’t need to keep any references for Data Infrastructure. We can even remove the Connection strings as well. We are using Code First Migration using Entity Framework Core. The migrator console app can take care of migrations and update the database. The repository works with the entities which are in the Data Infrastructure. The Data Infrastructure has two projects (Main. Infrastructure and Main. Model). We can hopefully keep them in a container on the server (Linux). 

They communicate with the Service layer (Project) happens using (Data Model & Entity Model) with the Data Infrastructure. Service sends Entity Model (for saving or update). The returned queries from the Data Infrastructure are re-created with new objects which have no connection with the database and entity model. It means the communication initiates and closes inside the servers where the processing is not exposed to the client's browsers. 

Rowsers are not displayed with the model which the service projects send. They see and communicate with the controller and end points with the View Model. This is in the web project (presentation). 

The web project communicates with the service project with Business Model objects. This is how I tried to keep the web project separate and make the service project reusable for other cross-platform projects using Web API. 

This part is done. New knowledge is accumulating and I am refactoring the code continuously. Like the registration, middleware is self-contained and reusable using dependency extensions. But the parameters are provided from the appsettigs.json from the web project. 

More information on the best practices, please check the Help Documents folder in the Main Code Repository.

# Web App Project:

Identity (Signin, Signout, Email veirficaton, Acoount lock, Roles based authorizaton):
For auntication, we are using te .Net 8.0 Identity with default configuratin. The tables are IdentityUser and IdentityRole. Authorization is Role based. Middleware configuration and registratin is done in the Infrastructure project. Settings are kept in the Appsetts.json file in te Web Project.

Currently the roles are: Admin, Company & User

# Main.Infrastructure Project:
Nuget PMC:
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.0
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.0
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore -Version 8.0.0

# Main.Migrator Project:
When we create the console project (Auto):
Install-Package Microsoft.VisualStudio.Azure.Containers.Tools.Targets -Version 1.23.0

Nuget PMC:
Install-Package Microsoft.Extensions.Hosting -Version 8.0.0
Install-Package Microsoft.Extensions.Configuration.Json -Version 8.0.0
Install-Package Microsoft.EntityFrameworkCore.Design -Version 8.0.0

# The Best Practices used in the Main Project: 

1. I used View Components to make the code modular and readble in the layout page.
2. In program.cs, most configuration code is moved to Data Infrastructure and Service project.
3. Zero use of EF core packages and references in the web project.


