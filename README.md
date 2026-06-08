<img width="901" height="487" alt="GithubSocialImag1" src="https://github.com/user-attachments/assets/96d62ec1-4457-4c2f-914c-10e41e301e66" />

# Story
## Shop Example: 

The website is for small shops or sellers who want to own an online presence to sell products and reach a higher reach for people. The website sells products/services. He/she is the owner of the shop and the website. The shop owner will have his own domain and hosting.  

The shop owner will have a company account to upload products. The shop will have one more account which is for shop admin. The purpose of the admin account is to organize the uploaded products in different templates on the home page and market page. The home page will display products based on selected templates and products. 

Visitors of the products can browse through the products and add to cart and checkout for purchase. Visitors can see the details of the product with multiple photos and information. 

Shop Admin User can configure & display other companies or business advertisements on the shop website. He can be an affiliate of other websites.  

### The website will provide a few more pages:  

1. contact us,  
2. about us,  
3. FAQ 
4. Set a logo for the website.  

In short, this is a very small size CMS for small businesses. 

# Shop Example (Business Concept): 

### 1. The business will give the shop two things. A domain and a hosting plan. 

### 2. As a subscriber, the shop will give us a monthly subscription fee. The fee will cover the hosting of the website. The domain for the shop will have a one-time fee every three years. 

### 3. For new enhancement for the shop’s ow requirements, a one-time fee can be introduced. 

# Shop Example Modues

## User Registration  
  - Anyone can create an account to purchase products.  
  - Registration requires email verification before login is allowed.
    
### **Email Verification Process**  
  - A verification email is sent after registration.  
  - The link is valid for **2 hours**; after that, it expires.  
  - Without verification, users cannot log in.
    
### **Getting a New Verification Link**  
  - Option 1: Try logging in with your registered email and password; a new verification email will be sent.  
  - Option 2: Use the “Forgot Password” link, enter your email, and receive a fresh verification link.
    
### **Security Policy**  
  - No user can log in until their email is confirmed.  
  - Password reset and account recovery options are built in.  

In short: the page outlines a **strict email verification policy** to ensure that only confirmed users can access accounts, with built-in methods to resend verification links if needed. 

## Manage Contents
Here in this shop example, we are considering content (image, link, short note, YouTube link) which are not the shop owner's products to sell. These are to advertise or give message to the visitors about a business or advertisements. These are for giving ads for a third-party company or businesses or for self. The purpose of such contents is to show images or ads with links to go to the actual website link or open a YouTube video.

### Admin User: He/she can add, update and delete a content and see the list of contents for the shop; he is the Admin.

### A Content has few fields:

1. Poster Name
1. Poster Contact Number
1. Post Title
1. Type of Post: (Ad Space, Short Note, YouTube Video)
1. Website: The link of the advertainment (company) or YouTube link
1. Search Tag 
1. Images (any number)

***
### Use of these Contents
When you will configure the Pages of the website, you can select template for the panel (a row in a page) to select from the (Ad Space, Short Note, YouTube Video) contents. This module will setup the contents to use in the pages. Templates are designed for these contents. These templates are only for showing advertisement with or without a link to navigate to the ad website or video.

Remember that there are other templates for Products (shop owners) which includes the add to cart button.


# Solution Design & Architecture (.Net 8.0)

I started my code to build and run for a client (small shop). 

It was previously made for an online marketplace. That was in .NET Framework 4.6 where you must deploy the portal in a windows or cloud based (Microsoft Azure) in Platform Service as a web applica[...]

When I started looking at the code and searching on the internet, I found that Microsoft doesn't have any support over the framework because of security vulnerability.  

I decided to do a migration of my code in .NET 8.0 because it has long term support plan and it is portable both in Windows and Linux servers. 

Also, the technology supports cross platforms including mobile devices and tablets.  

I started to create the architecture of the new solution, keeping in mind the best practices of design and architecture. Based on the research on the internet, I started the migration and tried to[...]

The primary objective was to make the code modular, reusable, separation of the concerns, and readable while doing the code for the solution. 

Another objective was to make sure; it is Linux deployable using containers and keeping the services completely separated from the presentation code (Web Project).  

My plan to separate the services from the presentation is to make the web project light weight and reus the same in different cross platform non-computer devices (Mobile, Tab). 

Microsoft already has their own technology for app development (Xamarin) which uses the API (Web API) project hosted on any server. My plan was to keep the code common for everyone (web, mobile, &[...]

In that consideration, my data infrastructure (Model, Repository) is self-registered. This project has zero dependency over any Data Transfer Object or View Model.  

Again, the service never communicates with the presentation layer with the Entity Models. They talk with Data Infrastructure in entity and business models.  

While communicating with the Presentation layer (web project), they use business objects which have no connection or tracking with Data Infrastructure. This technique provides the application data[...]

Since the Data Infrastructure is self-registered, we don't need to keep any references for Data Infrastructure. We can even remove the Connection strings as well. We are using Code First Migrati[...]

They communicate with the Service layer (Project) happens using (Data Model & Entity Model) with the Data Infrastructure. Service sends Entity Model (for saving or update). The returned queries fr[...]

Rowsers are not displayed with the model which the service projects send. They see and communicate with the controller and end points with the View Model. This is in the web project (presentation)[...]

The web project communicates with the service project with Business Model objects. This is how I tried to keep the web project separate and make the service project reusable for other cross-platfo[...]

This part is done. New knowledge is accumulating and I am refactoring the code continuously. Like the registration, middleware is self-contained and reusable using dependency extensions. But the p[...]

More information on the best practices, please check the Help Documents folder in the Main Code Repository.

# Security Feature: Identity 

These: Signin, Signout, Email veirficaton, Acoount lock, Roles based authorizaton are the pages where these security features are applied.

For auntication, we are using te .Net 8.0 Identity with default configuratin. The tables are IdentityUser and IdentityRole. Authorization is Role based. Middleware configuration and registration i[...]

Currently the roles are: Admin, Company & User

### Broken Access Control & Enumeration (OWASP A01:2021):
### The Threat: Attackers input various email addresses into a forgot password form to see which ones return a "User not found" error. This maps out registered user bases for targeted phishing or[...]
### Mitigation: 
1. Anti-Enumeration Logic. 
2. The controller uses an identity-blind diversion step. 

We will check if user exists and if the email is verified or not. If not exists and verified, the enumerating code from hacker or threat will be redirected but we do not reveal if the user exists [...]

This means that we will check the link with the existence and verified requirement of the link. The threat don't know if the user or email already in exists. They are running code against the logi[...]

We will rather provide a confirmation that check your inbox for the link to set up your password. This is how we are mitigating the Anti-Enumeration Logic. From the security stand; Whether the ema[...]
 
### Cryptographic Failures & Session Hijacking (OWASP A02:2021)

### The Threat: Predictable reset tokens (like simple base64 hashes or sequential numbers) can be guessed by automated scripts, allowing malicious password overrides.

### Mitigation: 
1. Cryptographic Token Lifecycles: 
The workflow calls ASP.NET Core's internal GeneratePasswordResetTokenAsync(user). This function is from the Identity User Manager. This generates a time-bound, cryptographically random string sign[...]

2. Security Stamp Invalidation: 
Once ResetPasswordAsync (Identity owned method) completes successfully, ASP.NET Core automatically refreshes the user's Security Stamp in the database. This instantly invalidates any active browse[...]

### Injection and Cross-Site Request Forgery (OWASP A03:2021 / A05:2021)
### The Threat: Attackers spoof forms using unauthorized cross-domain scripts or target database flaws via inputs.
### Mitigation: 
1. Token Integrity Checks: The [ValidateAntiForgeryToken] attribute added to both POST endpoints (action) in controller. They work side-by-side with @Html.AntiForgeryToken() implicitly built into [...]
2. Entity Framework Core acts as the data layer (ApplicationDbContext which is Identity configured). By utilizing parameterized LINQ parameters under the hood FindByEmailAsync(email) before sign; [...]

### Identification and Authentication Failures (OWASP A07:2021)
### The Threat: Weak reset pathways easily bypass initial account defenses, nullifying complex user passwords. 
### Mitigation: 
1. State Enforcement Policies: 
The system explicitly requires email verification before allowing a password reset flow (IsEmailConfirmedAsync). The application binds the target email address context directly inside the cryptogr[...]

# Main.Infrastructure Project (Data Infrastructure):
### Nuget PMC:
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.0
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.0
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore -Version 8.0.0

### Main.Migrator Project:
When we create the console project (Auto):
Install-Package Microsoft.VisualStudio.Azure.Containers.Tools.Targets -Version 1.23.0

### Nuget PMC:
Install-Package Microsoft.Extensions.Hosting -Version 8.0.0
Install-Package Microsoft.Extensions.Configuration.Json -Version 8.0.0
Install-Package Microsoft.EntityFrameworkCore.Design -Version 8.0.0

# The Best Practices used in the Main Project: 

1. I used View Components to make the code modular and readble in the layout page.
2. In program.cs, most configuration code is moved to Data Infrastructure and Service project.
3. Zero use of EF core packages and references in the web project.

# Github Action (Release.yml) & Docker file (Continuous Integration)

## Workflow Test - Automated Build & Release

✅ This repository is now configured with automated CI/CD workflows:
- **dotnet.yml**: Tests on every push/PR to master
- **release.yml**: Builds Docker image, creates releases, and deploys to VPS (when available)

Test merge completed successfully!

