# Multi Tenant Architecture 🔄(70%) 🟥(30%)

## SaaS Cross-Cutting Concerns 
In a .NET Software-as-a-Service (SaaS) architecture, cross-cutting concerns represent technical functionalities that span your entire system and must execute across various endpoints, layers, or microservices without altering the core business rules. In a multi-tenant SaaS application, these concerns become highly critical because they must almost always be evaluated in the context of a specific tenant.Core 

**SaaS Cross-Cutting Concerns:**
1. **Tenant Resolution:** Identifying the tenant from headers, subdomains, or access tokens on every incoming HTTP request.🔄(70%)
2. **Data Isolation:** Dynamically appending global query filters or swapping out connection strings based on the resolved tenant ID.🔄(70%)
3. **Authentication & Authorization:** Ensuring users are authenticated globally and verified for specific tenant-level permissions or subscription tiers.🔄(70%)
4. **Feature Management & Billing Flags:** Enabling or disabling code execution branches dynamically according to the tenant’s subscription tier.🟥(0%)
5. **Structured Logging & Tracing:** Injecting a TenantId attribute into every log context to isolate logs per customer across microservices.🔄(70%)
6. **Rate Limiting & Throttling:** Restricting request limits at the tenant level to prevent noisy neighbor scenarios.🟥(0%)
7. **Global Exception Handling:** Mapping all unexpected errors to standardized JSON problem details while hiding internal infrastructure quirks.🔄(70%)

## Structural Implementation Patterns in .NET:
To adhere to the Single Responsibility Principle and avoid mixing business logic with infrastructure noise, .NET applications use three distinct design patterns to implement these concerns:

1. **Middleware Pipeline** 
ASP.NET Core Middleware Pipeline: Middleware handles concerns at the outer HTTP level before a request ever reaches your API endpoints. Best used for: Tenant resolution, global rate limiting, global authentication, and top-level exception handling. The Rule of Ordering: The order of your middleware configuration in Program.cs dictates execution. Rate limiting should execute before authentication to conserve server resources, while authentication must run before authorization.🔄(70%) 🟥(30%)

2. **MediatR Pipeline Behaviors (CQRS)** 
If your architecture uses Clean Architecture or Vertical Slices with the MediatR library on GitHub, Pipeline Behaviors operate as intra-application middleware. Best used for: Domain validation (via FluentValidation), transactional database boundaries, application-level logging, and in-memory query caching. Benefit: It allows the application layer to remain independent of the ASP.NET Core HttpContext.🔄(70%) 🟥(30%)

3. **EF Core Global Query Filters & Interceptors** 
When relying on Entity Framework Core, your database access layer can natively manage data separation rules. Best used for: Automated multi-tenant data filtering, soft deletes, and automated database auditing (such as injecting CreatedByTenantId or timestamp fields).
🔄(70%) 🟥(30%)



# Multi-Tenant Implementation 
Monolithic Application & Shared Database 

## Supported Tenant Strategies 
The system supports flexible tenant identification through: 
1. Domain, 
2. Subdomain, 
3. Subdirectory 

## Tenant Resolve (Middleware) 
Cache-aside pattern: (useig session)

1. Resolution Process: The Tenant Resolver middleware identifies the tenant from the request (using routing or headers) and leverages Session and Memory Cache for quick lookups. 
2. Caching: The resolved tenant is cached in the active session. For unresolved requests, the system searches the database, stores the result in Session, and bypasses subsequent DB lookups.

## Global Eception Handle (Middleware)

### **Key Features**
✅ **Global middleware** - Catches all unhandled exceptions without try-catch blocks  
✅ **Intelligent mapping** - 18 exception types → error codes + HTTP status codes  
✅ **Serilog logging** - 3 log streams (application, errors, JSON) with daily rotation  
✅ **Database persistence** - Stores full context with multi-tenant isolation  
✅ **Automatic deduplication** - Repeating exceptions increment counter (within 1 hour)  
✅ **6 database indexes** - Optimized for fast queries and filtering  
✅ **Secure** - Excludes auth headers, cookies, API keys; generic user messages  
✅ **Admin API** - Search, filter, export CSV, view stats, mark resolved, cleanup  
✅ **Multi-tenant** - Automatic tenant scoping via query filters  

---

### 📁 **Source Code**
- ExceptionLog.cs
- ExceptionEnums.cs
- ExceptionErrorCodes.cs
- ErrorResponse.cs
- RegisterSerilogConfiguration.cs
- ExceptionLoggingService.cs
- GlobalExceptionHandlingMiddleware.cs
- ExceptionLogsController.cs
- ExceptionDashboardController.cs
- AddExceptionLogsTable.cs

### **Configuration**
- Program.cs (UPDATED with Serilog & middleware)
- ApplicationDbContext.cs (UPDATED with ExceptionLog DbSet)
- appsettings.json

### **Documentation**

- `README_EXCEPTION_HANDLING.md` - **Main reference guide**
- `EXCEPTION_HANDLING_GUIDE.md` - **Detailed 500+ line guide**
- `QUICK_REFERENCE.md` - **Quick lookup for developers**
- IMPLEMENTATION_SUMMARY.md - **What was delivered**
- IMPLEMENTATION_CHECKLIST.md - **Verification checklist (115+ items)**
- DEPLOYMENT_GUIDE.md - **Production deployment steps**


### 📝 **Log Files Created**

Three daily log files (auto-rotating, 30-day retention):
- `application-log-YYYY-MM-DD.txt` - All logs
- `errors-log-YYYY-MM-DD.txt` - Errors only
- `exceptions-log-YYYY-MM-DD.json` - Compact JSON format

---

### 🔐 **Security Built-In**

- ✅ Sensitive headers excluded (Authorization, Cookie, API Keys)
- ✅ Generic user messages (no stack traces exposed)
- ✅ Request body truncated (2000 chars max)
- ✅ Multi-tenant isolation automatic
- ✅ Admin role required for API access

---

### 📚 **Documentation**

1. **README_EXCEPTION_HANDLING.md** - Complete system overview
2. **QUICK_REFERENCE.md** - Quick lookup for developers
3. **DEPLOYMENT_GUIDE.md** - Production deployment
4. **IMPLEMENTATION_CHECKLIST.md** - Verification steps

### ✨ **What Makes It Production-Ready**

✅ **Comprehensive** - 40+ error codes, 18 exception types, 6 API endpoints  
✅ **Performant** - Async logging, database indexes, automatic deduplication  
✅ **Secure** - Sensitive data excluded, generic messages, multi-tenant safe  
✅ **Maintainable** - Clean code, interfaces, DI, extensible design  
✅ **Observable** - Structured logging, dashboards, analytics  
✅ **Documented** - 2000+ lines of guides with examples  

## Authentication 
Default Identity Authentication: Keeps the default ASP.NET Core Identity setup. Uses encrypted, cookie-based default ASP.NET Core Identity. Account Uniqueness: Email remains unique across the global user base. 

## Authorization 
1. **Step 1: Claim Construction** 
- The middleware retrieves the TenantId from ITenantSetter and matches it with the CurrentTenantId stored in the session. The system fetches the user's combined roles (global Identity Roles + Tenant-specific Roles). 
- A unique user claim is created containing the UserId, TenantId, and TenantRole. These newly combined claims are then attached to the authenticated user's principal. 
2. **Step 2: Authorization Handler Execution** 
- The custom authorization handler reads the claim combination from the user's principal. If the claims are valid, it assigns the appropriate tenant-level policy authorization. 

## Tenant Isolation 
1. Policy based Tenant Role (authorization) 
2. Global Roles: Operates on the default Identity Roles (GlobalAdmin, User). 
3. TenantRole: Policy-based, driven by tenant-specific roles. 
4. Tenant Scoping: Once a user is authenticated, the system generates custom claims that isolate the user to the specific tenant they are accessing. Aithenticated user uses the default Identity Auth Cookie.
5. Data Partitioning: Data is partitioned at the row level via EF Core's Global Query Filters. A global filter (based on TenantId) is set via an ITenantSetter interface. Using Dependency Injection (DI), the database is partitioned and isolated for each tenant. 
6. Keeping the code structure clean and memorable. Authorization code is mostly in one place, manageable and maintainable for more roles and policies.
7. Subdirectory Handling: Routes are rewritten for subdirectory-based tenants, allowing the browser and server to manage authentication of cookies natively. (Removes code complexity) 
8. Request Security: Unsafe requests are protected by the Identity default Antiforgery mechanisms. Fetch and AJAX requests require an explicit header attachment.

## Solution Design & Architecture (.NET 8.0) 
### Background: 
I started my code to build and run for a client (small shop). It was previously made for an online marketplace. That was in .NET Framework 4.6 where you must deploy the portal in a windows or cloud based (Microsoft Azure) in Platform Service as a web application. Deployment infrastructure/platform service is a windows server based. When I started looking at the code and searching on the internet, I found that Microsoft doesn’t have any support over the framework because of security vulnerability. I decided to do a migration of my code in .NET 8.0 because it has long term support plan and it is portable both in Windows and Linux servers. Also, the technology supports cross platforms including mobile devices and tablets.  

### The Best Practices by Research 
Note: Before planning for the multitenant aplicatin saas, I didn't consider or research the scaling part. Still it is applicable with curret design. I started to create the architecture of the new solution, keeping in mind the best practices of design and architecture. Based on the research on the internet, I started the migration and tried to keep and reuse some parts of the past work.  
1. The primary objective was to make the code modular, reusable, separation of the concerns, and readable while doing the code for the solution.
2. Another objective was to make sure it is Linux deployable and keeping the services completely separated from the presentation code (Web Project). Now, code is separated and using services but the API project is not there yet.
3. My plan to separate the services from the presentation is to make the web project light weight and reuse the same in different cross platform non-computer devices (Mobile, Tab).
4. Microsoft already has their own technology for app development (Xamarin) which uses the API (Web API) project hosted on any server. My plan was to keep the code common for everyone (web, mobile, & tablet). 

## Solution Design 

1. In that consideration, my data infrastructure (Model, Repository) is self-registered. This project has zero dependency over any Data Transfer Object or View Model.
2. Again, the service never communicates with the presentation layer with the Entity Models. They talk with Data Infrastructure in entity and business models.
3. While communicating with the Presentation layer (web project), they use business objects which have no connection or tracking with Data Infrastructure. This technique provides the application database more secure because in any mistake, code doesn’t have any chances to alter or change.
4. Since the Data Infrastructure is self-registered, we don’t need to keep any references for Data Infrastructure. We can even remove the Connection strings as well. We are using Code First Migration using Entity Framework Core. The migrator console app can take care of migrations and update the database.  (But currently, we are keeping the connetin string in web app project)
5. The repository works with the entities only in the Data Infrastructure. The Data Infrastructure has two projects (Main. Infrastructure and Main. Model).
6. They communicate with the Service layer (Project) happens using (Data Model & Entity Model) with the Data Infrastructure. Service sends Entity Model (for saving or update). The returned queries from the Data Infrastructure are re-created with new objects which have no connection with the database and entity model. It means communication initiates and closes inside the servers.
7. Browsers are not displayed with the model which the service project sends. They see and communicate with the controller and end points with the View Model. This is in the web project (presentation).
8. The service registrations, middleware is self-contained and reusable using dependency extensions. The parameters (connection string) are provided from the appsettigs.json from the web project.  

### Future Work: 
1. The web project communicates with the service project with Business Model objects. This is how I tried to keep the web project separate and make the service project reusable for other cross-platform projects using Web API.
2. Because of the saperatin and breaking the code modular, we can convert the solution into micro service based deployment and scale the heavy traffic api services.

## Security Features
(Default Identity Flow)

1. **Broken Access Control & Enumeration (OWASP A01:2021):**  Attackers input various email addresses into a forgotten password form to see which ones return a "User not found" error. This maps out registered user bases for targeted phishing. Mitigation: (Anti-Enumeration Logic, the controller uses an identity-blind diversion step) We will check if the user exists and if the email is verified or not. If not exists and verified, the enumerating code from hacker or threat will be redirected, but we do not reveal if the user exists. This means that we will check the link with the existence and verified requirement of the link. The threat doesn't know if the user or email already exists. They are running code against the login. We will rather provide a confirmation that check your inbox for the link to set up your password. This is how we are mitigating the Anti-Enumeration Logic. 
2. **Cryptographic Failures & Session Hijacking (OWASP A02:2021)** Predictable reset tokens (like simple base64 hashes or sequential numbers) can be guessed by automated scripts, allowing malicious password overrides. Cryptographic Token Lifecycles & Security Stamp Invalidation The workflow calls ASP.NET Core's internal GeneratePasswordResetTokenAsync(user). This function is from the Identity User Manager. This generates a time-bound, cryptographically random string during sending an email link. Once ResetPasswordAsync (Identity owned method) completes successfully, ASP.NET Core automatically refreshes the user's Security Stamp in the database. This instantly invalidates the token timestamp. The mail link is no longer usable.
3. **Injection and Cross-Site Request Forgery (OWASP A03:2021 / A05:2021)** Attackers spoof forms using unauthorized cross-domain scripts or target database flaws via inputs. The [ValidateAntiForgeryToken] attribute added to both POST endpoints (action) in controller. They work side-by-side with @Html.AntiForgeryToken() implicitly built into the views. Entity Framework Core acts as the data layer (ApplicationDbContext which is Identity configured). By utilizing parameterized LINQ parameters under the hood FindByEmailAsync(email) before signing in a user reduced the risk of password injection.
4. **Identification and Authentication Failures (OWASP A07:2021)** Weak reset pathways easily bypass initial account defenses, nullifying complex user passwords. The system explicitly requires email verification before allowing a password to reset flow (IsEmailConfirmedAsync) and login. 

## Previous Shop Example (Identity Default) 
It is extended to use tenants (IdentityUser is now: 
1. ApplicationUser inherited from IdentityUser).
2. Authentication didn't change. 
3. Authorization is updated for multi-tenant environment. 

## The Best Practices (Web Project): 
1. I used View Components to make the code modular and readable on the layout page.
2. In program.cs, most configuration code is moved to Data Infrastructure and Service project.
3. Zero use of EF core packages and references in the web project. 

**Note: These are no more valid. We are now migrating to Multi Tenant architecture.**
1. Security Feature (Web Project) using Identity
2. These: Sign in, Signout, Email verification, Account lock, Roles based authorization are the pages where these security features are applied. For authentication, we are using te .Net 8.0 Identity with the default configuration. The tables are IdentityUser and IdentityRole. Authorization is Role based. Currently the roles are: (Admin, Company & User)
3. It has changed for multi-tenant: Global Admin and User. Now, we have tenant specific roles too. 

Story of Shop Example: 
The website is for small shops or sellers who want to own an online presence to sell products and reach a higher reach for people. The website sells products/services. He/she is the owner of the shop and the website. The shop owner will have his own domain and hosting. The shop owner will have a company account to upload products. The shop will have one more account which is for shop admin. The purpose of the admin account is to organize the uploaded products in different templates on the home page and market page. The home page will display products based on selected templates and products. Visitors of the products can browse through the products and add to cart and checkout for purchase. Visitors can see the details of the product with multiple photos and information. Shop Admin User can configure & display other companies or business advertisements on the shop website. He can be an affiliate of other websites. The website will provide a few more pages: contact us,  about us, FAQ  and  Set a logo for the website. In short, this is a very small-size CMS for small businesses.  

## Software Scope:
1. **Module: Account Management (Multi Tenant Model)** Anyone can create an account to purchase products, registration requires email verification before login is allowed, email Verification process: a verification email is sent after registration, the link is valid for 2 hours; after that, it expires, Without verification, users cannot log in; getting a new verification link, try logging in with your registered email and password; a new verification email will be sent, use the “Forgot Password” link, enter your email, and receive a fresh verification link, Security Policy: No user can log in until their email is confirmed, Password reset and account recovery options are built in. In short: the page outlines a strict email verification policy to ensure that only confirmed users can access accounts, with built-in methods to resend verification links if needed. **For multi tenant integration, some new fetures are being added**
2. **Module: Manage Advertisement Posts** Here in this shop example, we are considering content (image, link, short note, YouTube link) which are not the shop owner's products to sell. These are to advertise or give messages to the visitors about a business or advertisement. These are for giving ads for a third-party company or businesses or for self. The purpose of such contents is to show images or ads with links to go to the actual website link or open a YouTube video. Tenant Admin User: He/she can add, update and delete content and see the list of contents for the shop; he is the Admin. A Content has few fields: Poster Name, Poster Contact Number, Post Title, Type of Post: (Ad Space, Short Note, YouTube Video), Website: The link of the advertainment (company) or YouTube link, Search Tag, Images (any number). **Use of these Contents:** When you configure the pages of the website, you can select template for the panel (a row in a page) to select from the (Ad Space, Short Note, YouTube Video) contents. This module will setup the contents to use in the pages. Templates are designed for these contents. These templates are only for showing advertisement with or without a link to navigate to the ad website or video. Remember that there are other templates for Products (shop owners/tenant) which includes the add to cart button.
3. **Module: Manage Products** Here in this shop example, we are considering Product (Name, Price, Description, Images) which are the shop owner's/tenant's items to sell. These are to be sold to online users. Users can add the products to the shopping cart and order them from the tenant store. Company User/Tenant Content Mananger: he/she can add, update and delete a product and see the list of products for the shop. The purpose of this module is: add a product, update an existing product, delete a product, see the list of all products. Each Products can have as many images as he/she (shop owners/tenant) wants. Until now, no validation has been provided to restrict users from entering a limited number of product images. A product has few fields: product name, description, category, subcategory, price, discount, sale commission (if the shop/tenant wants to sell by any third-party shop/tenant), search tag, images (any number)
4. **Module: Page Settings &  Configuration** When Shop Admin/Tenant Admin will configure the pages of the website, he/she can select template for the panel (a row in a page) to select from the products. This module will setup the products to display on the pages. Templates are designed for these products. These templates only show products with links to view details of the product or to add to cart. Remember that there are other templates for ad posts (shop admin/tenant admin) created in mosule 2, which can also be added in a row of a page.

## NuGet Packages 
1. Main. Infrastructure (Class Library): 
- Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.0
- Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.0
- Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore -Version 8.0.0
2. Main.Migrator Project: (Console Application: Not in use)
- Install-Package Microsoft.VisualStudio.Azure.Containers.Tools.Targets -Version 1.23.0
- Install-Package Microsoft.Extensions.Hosting -Version 8.0.0
- Install-Package Microsoft.Extensions.Configuration.Json -Version 8.0.0
- Install-Package Microsoft.EntityFrameworkCore.Design -Version 8.0.0 

## GitHub Action (Continuous Integration CI) 
✅ 1. This repository is now configured with automated CI workflows (dotnet.yml: Tests on every push/PR to master) 

<img width="901" height="487" alt="GithubSocialImag1" src="https://github.com/user-attachments/assets/96d62ec1-4457-4c2f-914c-10e41e301e66" />

 
