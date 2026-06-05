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
For auntication, we are using te .Net 8.0 Identity with default configuratin. The tables are IdentityUser and IdentityRole. Authorization is Role based. Middleware configuration and registratin is done in the Infrastructure project. Settings are kept in the Appsettings.json file in te Web Project.

Currently the roles are: Admin, Company & User




# Security Feature:

# Broken Access Control & Enumeration (OWASP A01:2021):
The Threat: Attackers input various email addresses into a forgot password form to see which ones return a "User not found" error. This maps out registered user bases for targeted phishing or brute-force attacks.
Mitigation: 
1. Anti-Enumeration Logic. 
2. The controller uses an identity-blind diversion step. 

We will check if user exists and if the email is verified or not. If not exists and verified, the enumerating code from hacker or threat will be redirected but we do not reveal if the user exists or is verified. We wil not let the threat know about the login information which they are seeking with the link they collected from different sources. In our configuration, we configured that users must be verify email after sign up. Before that they cannot access the web application. We also configured that users cannot try more than consecutive 5 times to login in the application. 5 times try, will lock the account. The admin must be inform to unlock the account. 

This means that we will check the link with the existence and verified requirement of the link. The threat don't know if the user or email already in exists. They are running code against the login attempts with emails they want to check. So, they want to about the email. In the redirection process, we will not expose the email or any message to the threat or genuine user who forget the password. 

We will rather provide a confirmation that check your inbox for the link to set up your password. This is how we are mitigating the Anti-Enumeration Logic. From the security stand; Whether the email address exists in the system or not, the user sees the generic confirmation page (Meaning: "If matching records exist, an email was sent"). No structural information is leaked to an external scanner. This is the controller called an identity-blind diversion step. This code is in the presentation layer in Auth controller.

 
# Cryptographic Failures & Session Hijacking (OWASP A02:2021)

The Threat: Predictable reset tokens (like simple base64 hashes or sequential numbers) can be guessed by automated scripts, allowing malicious password overrides.

Mitigation: 
1. Cryptographic Token Lifecycles: 
The workflow calls ASP.NET Core's internal GeneratePasswordResetTokenAsync(user). This function is from the Identity User Manager. This generates a time-bound, cryptographically random string signed with the application's unique deployment key. The link exposes a strict token lifespan. We set the lifespan in the configuration. From the application configuration any link that we send to the user in email to reset password are for 2 hours to use. After that time, the link will not be affective anymore. If an attacker intercepts an old email link and the token expires, it is preventing the replay attacks. 

2. Security Stamp Invalidation: 
Once ResetPasswordAsync (Identity owned method) completes successfully, ASP.NET Core automatically refreshes the user's Security Stamp in the database. This instantly invalidates any active browser sessions, cookies, or old tokens globally. Because after resetting the time stamp, the link is unusable even the attacker is using the link within two hours.


# Injection and Cross-Site Request Forgery (OWASP A03:2021 / A05:2021)
The Threat: Attackers spoof forms using unauthorized cross-domain scripts or target database flaws via inputs.
Mitigation: 
1. Token Integrity Checks: The [ValidateAntiForgeryToken] attribute added to both POST endpoints (action) in controller. They work side-by-side with @Html.AntiForgeryToken() implicitly built into Razor <form> elements. This blocks Cross-Site Request Forgery (CSRF). This is used when we access our data to save, update or delete the database.
2. Entity Framework Core acts as the data layer (ApplicationDbContext which is Identity configured). By utilizing parameterized LINQ parameters under the hood FindByEmailAsync(email) before sign; with the separate sign in method; SQL Injection risks are neutralized entirely. This is used during sign in process.


# Identification and Authentication Failures (OWASP A07:2021)
The Threat: Weak reset pathways easily bypass initial account defenses, nullifying complex user passwords. 
Mitigation: 
1. State Enforcement Policies: 
The system explicitly requires email verification before allowing a password reset flow (IsEmailConfirmedAsync). The application binds the target email address context directly inside the cryptographic validation routine. This ensures that a token generated for userA@example.com cannot be strategically resubmitted to reset the password for userB@example.com.

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

# Github Action (Release.yml) & Docker file (Continuous Integration)


