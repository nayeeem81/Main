# 🏬🛍️Shopping Mall Web App (Multi-Tenant SaaS)

**We are developing a multi-tenant stores web application. A store is called tenant ⇄ A tenant is a store. (vice versa)** 

### **🛍️store is a 🛍️shop inside 🏬shopping mall**

The owner of the shop/store can be a legitimate business (with a trade license) or individual (not a professional seller, a freelance seller using the store for his or her own product selling purpose).  We treat the tenant as a shop. Anyone registering in the multitenant web application is the admin of a shop. The registration is for the shop with an email address from the multi-tenant portal:

### www.tenantors.com

The registration is for the shop with an email address from the portal (example: www.tenantors.com). An email can be used for multiple tenants. Just like a single sign in across all tenants. Read followng for understanding the detail, about the multi-tenant web application. We explain first, the application related keywords and objects. We must undertand them to get a complete view about the concept & story of the multi-tenant SaaS.

## Tenant🛍️ & User🔑 

### Further Details To Understand the story better next...

### 1. 🛍️Tenants:
Tenants are a shop. Tenant is created with an email address. The person who owns the email address is the first admin user of the shop, may be the owner. He can create it on behalf of the owner.

### 2. 🔑Users:
Initial tenant creation, this multi-tenant web application; assigns the user (email) as an admin of the shop. Then, he/she can add more users by invitation (email) link. A user can own multiple Tenant with the same email address. He/she can work under a Tenant (from an invite) keeping his/her own tenants. A user can work for multiple tenants with one email address.

## 🛍️Tenant Types (According to Hosting Plan)

### Tenant name is: **kaiassociates**
           
**kaiassociates** can host in 3 differnt ways to use our **Multi Tenant Web App Store Features**

### Domain: www.kaiassociates.com
After creating a tenant, the user can add a domain for the tenant. The tenant needs to add the web application Ip address to his domain provider as his shop website host. The tenant will use their own domain URL and use the multi-tenant web application provided with shop-related features.

### Subdomain: www.kaiassociates.tenantors.com
**Unique username is the name of the subdomain**
After creating a tenant, the user can add a sub domain for the tenant under the web application domain. The tenant needs no Ip address or domain provider. Rather, the tenant needs to provide a unique name (if nobody is using the same name) as their sub domain. The tenants use their own subdomain-based URL and use the multi-tenant web application provided with shop-related features.
            
### Subdirectory: www.tenantors.com/kaiassociates/
**Unique username is the name of the subdirectory**
After creating a tenant, the user by default gets a directory for the tenant under the web application domain. The sub directory will be created by the unique username during the creation of the Tenant. The tenants use their subdirectory-based URL and use the multi-tenant web application provided with shop-related features.

## Login approach
Tenant is registered by an user email. Verify email link sent upon registration form submission. User gets admin role and a subdirectory for the tenant (default). He/She must must verify the registered email to activate the tenant account. 

Tenant user can login and access the store work space only using verified email.

**After email verification, user will login in the website, from where the user was registered. (example:tenantors.com)**

After successful Login, the tenant admin can add a domain for his own store. Once the domain is configured, the user can login from there own domain.

**🆔🪪www.kaiassociates.com**

After successful Login, the tenant admin can configure the subdomain in the website. They will get a sub domain for his store. the user will continue login from the same website or with the subdomain website.

**📲🪪www.kaiassociates.tenantors.com**

After successful Login, the tenant admin can configure the domain or subdomain in the website. They will get a domain/sub domain for his store. Otherwise the user from default subdirectory workspace continue login from the same website.

**🏢🪪www.tenantors.com/kaiassociates**

### Summary:

### **🛍️ Tenant View:**
In the above three cases: it is the way how the tenant (store) users will login, use their own work space and how customers will find their online store. The tenants may boost their online presence using media, social media or use web url in a name card.

### **🛠️Technical View:**
It will confirm the isolation of the store identity and their  independent work space for each tenant inside the multi tenant web application.

## 🛍️Tenant Work Space

### **Tenant Shop Website (URL):**

**Tenant has it's own work space. Users can upload products, add ads (images, links) and arrange the pages by their choice.**


**Think the workspace as your shop in a shopping mall:**


**You plan in similar concept: the way the owner and managers plan, design interior and arrange products of a shop to display!**

### **🏢 Store Features (Tenant):**

1. **🏢Tenant Profile: (domain setup / buy domain, invite users) 🔄(50%)**
2. **🔒 Security & Isolation: Manage user account and change password 🔄(100%)**                    
3. **📦Product Manager: Manage Products (add, edit, delete, view) with admin dashboard 🔄(100%)**                  
4. **🖼️Advertisement Manager: (create ads with images, texts, links) 🔄(100%)** 
5. **🎛️Page Manager: (small CMS to organize the products and ads with different templates) 🔄(100%)**
6. **🛒Shopping Cart and Order Processing: (Pending Development) 🟥(0%)**
7. **📝Payment Manager: (Pending Development) 🟥(0%)**

## 👑Conecpt of the Story

You have a business with a valid trade license and do business professionally. You can register to open a shop. To open a shop, you need an email address to register. You are the owner of the shop. You are the admin on the shop website.

You have staff in your business. After registering at the shop, you can add staff with an invite link. You need to provide the email address of the staff to work for your online shop. You are the administrator of the shop. You can invite staff to join. You can add an invitation to join as an admin or a manager.

The staff can accept or reject your invitation to join. If the staff accepts your invitation, they must have an account on the multi-tenant web application. Your invitation will create his account in the application. An email will be sent from the Web Application to verify his/her email address. Once he/she verifies the link in his/her email inbox, he/she can login with the email and password in your shop.  Based on the type of invitation, the staff can perform specific tasks of the shop.

## ☁️🐧Hosting Environment (Linux VPS)

### 🐧Linux Environment In ☁️Cloud VPS

### Internet🌐➡️   Nginx Reverce Proxy🔒🛡️➡️   Router to Web App🖥️

He/she will go to the link of the tenant URL; browser sends the request to the server (multi-tenant shopping host). Multi-tenant web application hosted on a Linux VPS: Nginx reverse proxy receives the request and acts as router for Multi-Tenant Web Application. This layer of security is a Sheild for the shopping host.

### Internet🌐➡️   Nginx Reverce Proxy🔒🛡️🔀   Load Balance🖥️🖥️🖥️

Nginx convert https (encrypted) requests to http (decrypted) requests, Routing for (domain, sub domain) tenants to the host, Scale during high traffic times as load balancer, by shop visitors to multiple instances of VPS or different ports of the same VPS, limit the request per tenant to stop crashing the server by any abusive user or DDOS attack. The response from the shopping host is again encrypted and returned to the browser by Nginx.

## Concerns for Tenant (Isolation): 

Multi-Tenant applications often suffer from data leaks, which is an error because of the problem of isolation from one tenant to another. This error was not addressed by the application until they realized that the data was leaking because of an isolation problem. 

**The architecture of the multi-tenant model inside the application uses shared resources.**  

The shared resources are: 
1. The application who is serving all the tenants
2. Sometimes the database as well. 

So, it is very necessary to who are the users requesting access to the database for information and changing the database records. Here isolation comes into the play.  How we are allowing access to the user over the data of a tenant. It is not about the user if the user is authenticated in the system properly, rather the solution is with answer, if the user has the right to request and get access to the requested resource. 

This isolation is a big concern for multi-tenant applications because of the shared database and application which is used by all tenants. How do we isolate them and their users from the shared application and database infrastructure is the point where the data leak happens, and it is because of the architecture lacking with few of the concerns that didn’t address inside the application architecture design. 

# Multi-Tenant Request Pipeline

<img width="641" height="536" alt="Request Plipeline" src="https://github.com/user-attachments/assets/4a4be819-efff-4c4b-8aa9-306febf8bd0d" />


## Global Safety Net

<img width="635" height="218" alt="SafetyNetExceptionResponcePipeline" src="https://github.com/user-attachments/assets/40b630d1-6363-48cf-b382-868fb149c70b" />


# Middleware Order

**[ Middleware Order in Program.cs ]**

**1. TenantResolutionMiddleware ──► Resolves Tenant A via Subdomain/URL**
**2. UseRouting()**               
**3. UseAuthentication()        ──► Parses user claims (User claims they belong to Tenant B)**
**4. TenantSecurityMiddleware   ──► MATCH CHECK: Does URL Tenant match User Tenant? (FAIL -> 403)**
**5. UseAuthorization()** 

# Pipeline Starting Point: 

## TenantResolverHandlingMiddleware
**TenantResolverHandlingMiddleware:** In ASP.NET Core 8.0, a Multi-Tenant Request Pipeline isolates data and configuration per customer by resolving tenant context at the very beginning of an HTTP request. This architecture relies on this middleware to execute early in the pipeline to parse the incoming request, look up the tenant, and inject the context into a scoped service for downstream components.

**Middleware Name: TenantResolverHandlingMiddleware**

### Function: Tenant Identification, Faster Response and Tenant Data Isolation

**1. Tenant Identification:**
We first get the host and path from the HttpRequest object and extract the domain/subdomain or subdirectory. We check the extracted domain/subdomain or subdirectory in the database to identify, if they are in the list of our tenants. If found, we consider the request is valid and tenant is resolved. When the resolver gets a request from a logged user, it get the resolved tenant id.

**2. Faster Response:**
This middleware keep resolved TenantId in memory cache (consider active memory in the server end from where the request entered) for a specific time period (life time for 30 minutes). The tenant id is kept in memory; to not hit the database for resolved tenants again. We set the cache for 30 minutes. We query the database again and reset the cache active for next 30 minutes. This middleware does this for performance: faster response time for the request (reduce latency).

**3. Tenant Data Isolation**
ITenantSetter is a scoped (service) registered in the program.cs. Scoped means: for the entire request life time the service is active from the middleware to the end layer (ef core dbcontext). We set the resolved tenant id, in this service. We can access this scoped service using Dependency Injection (DI) from any (controllers, services, DbContext) components. 

The primary objective is to create a **Global Query Filter (isolated database partition for each tenant)** from the shared database (for all tenants) using the resolved tenant id. Any data related to the tenant is isolated from other tenants because of this isolation. This data isolation using the scoped ITenantSetter is an architectural design for multi tenant applicaton using .Net 8.0. Each request for a tenant can work and view only from the pertitioned isolated database for the tenant. 

**Read the meaning of data isolation next:** 

### Multi Tenant SaaS: 
**In this application, Database is shared. But it acts like a single database for each tenant.**

### By Global Query Filter
**Data Isolation: It means that, the tenant queries are done the partitioned records (just like a database which is his own database). Other tenants data are not present when any records are fetched from the database by a tenant. Because, globally it is filtered by GlobalQuery Filter based on the tenant id (resolved).**

## Web App Project Folder Structure (For Routeing)
**To Understand the Need of Route for Multi Tenant Architecture**

<img width="503" height="349" alt="FolderStructureMonolithicWebProject" src="https://github.com/user-attachments/assets/0f1a9a08-cb26-440e-9ea0-f8354c971c35" />

**We are using monolithich architecture, the published output will a single application instance and a single shared wwwroot.The MVC application folder structure separate Area controllers from standard controllers in this design pattern.**




**I am writting about the folder structure, to let know that the route is default. We are not doing any Tenant specific routeing.**


### Tenants have:
**1. Domain**
**2. Sub Domain**
**3. Sub Directory**


**For (1 and 2), it is** always a default routeing. If we provide tenant specific route; it implies that we have special reasons and tenant specific directories. We are creating the architecture which serves multiple tenants with same application (the multi-tenant SaaS).


**For static resources** which we can keep tenants specific: one style sheet [siteTennat1.css, siteTennat2.css, siteTennat3.css ... ] for each tenant for their own. This is a flexible and advaced method for styleing the pages for tenants. But it requres special skills in css code writeing to use the feature. 


In our application, We are not usng static resources for each tenant. We are providing two features to let them structure the pages by providing different Templates (to structure the page) and few themes (colors). 


## Tenant Concern: 


### What we are doing is:

**1. We will provide Vlobal Variables (colors) to create themes for tenants.** 
**2. Templates (1 col, 2 col, 3 col, 4 col), Banner (1 Image), Ad (Image with Link), Image Carousal.** 
**3. All templates will get the same color theme, set by the tenant.**
**4. Tenant can organize the rows as their wish (up, down, drag and drop) with the templates provided.**


## Technical Concern: 


### What we are doing is:


**1. For the domain ad sub domain based tenants:** always get the default route bacause of the technology (asp.net core mvc). It is the defaut routeing middleware behavior in .Net 8.0 multi tennat SaaS.


**2. For the Sub Direcoty tenants:** we need to use extra measure to either rewire the the base path to perfrom the default behavior. Or implement the per tennat directory which we explained earlier. Technically, the tenant who use sub directory (www.tenantors.com/ttkhai, here ttkhai is the tenant), we are rewriteing the base path to tenantors.com/ inside the route pipeline, so the route donot consider the ttkhai as an end point. It will search for the end point but actualy, this is the name of the tenant. We are doing url rewrite touse the default route. As a reasult the  applcation is serving same dynamic and static and resources forsame one instance. From same same site.css forall tenants. We make the colors dynamic using the global variable to create themes. 


# Multi-Tenant Application Security: For Each Tenant  

He/she will go to the link of the tenant URL; browser sends the request to the server (multi-tenant shopping host). Multi-tenant web application hosted on a Linux VPS: Nginx reverse proxy receives the request and acts as router for Multi-Tenant Web Application. This layer of security is a Sheild for the shopping host.  


Nginx convert https (encrypted) requests to http (decrypted) requests, Routing for (domain, sub domain) tenants to the host, Scale during high traffic times as load balancer, by shop visitors to multiple instances of VPS or different ports of the same VPS, limit the request per tenant to stop crashing the server by any abusive user or DDOS attack. The response from the shopping host is again encrypted and returned to the browser by Nginx. 


# Multi-Tenant Application Security: 


## Resolving Tenant (Middleware) 🔄(100%)


It is the starting point to isolate and secure the Tenant and its data.  

1. Tenant host is resolved against the database and if found, the middleware will serve the tenant; otherwise, the application will route to the portal.
2. The found tenant’s id is the first-class security variable to serve the tenant. This is kept in the session for future requests to stop hitting the database again.
3. The Resolved Tenant Id is primarily used to partition the shared database at the very beginning when the request life cycle starts using DI.
4. The authenticated user token validation we do in this same middleware. At the very entry point, we reject if the token does not match the regenerated token for an authenticated user.
5. We also match the Resolved Tenant Id from memory with the Tenant Id coming from the request in HttpContext to stop contaminated tenant **[Stop Data Leake]**  

**We provide safety here for the authenticated user for the tenant as the token uses the userid, tenant id and role of the tenant with a long secret key. We stopped at the entry point.** 

## **Another Layer of security**🔄(100%) 
 is for the tenant using same cookie but it is tenant specific & browser tab specific because the name of the cookie is suffixed by Ienant Id. This token confirms the unsafe action cannot take place even if the user gets pass with the authentication Jwt token (by validating) inside the resolve middleware (tenant resolver) and go dow stream. This antiforgery token (with teannt suffix) is checked just before executing the unsafe actions (which change data: add, update, delete) using action filter. It is overwriting the mvc core Iforgery attribute with validation and options with the tenant specific names for cookies. Options are set with IConfiguration for the cookie naming. **[Stop Data Leake]** 

We do this to confirm that the same user (email) with multiple tenants' access cannot get any leaked data which he/she has no access for a tenant when he is accessing tenants from the same browser with different tabs. **[Users who access multiple Tenants]** 

## After successful login: 🔄(100%) 

1. We use Jwt for token generation and validation. The authenticated user is validated in the middleware which resolves the tenant host.
2. We didn’t leave the session memory entirely. For faster detection of the tenant host, we must stop hitting the database each time for performance. Currently we are checking the memory cache; if found, we immediately return the Tenant Id. For scale horizontally, we need a shared distributed Radis cache for all instances.
3. For the secret Key, which we use for encryption of the token store, we need a separate data store for the safety of the Key. Currently, we are using one key for all tenants. It is quite long (64 bytes), hard to decrypt by unwanted. 

## How we do? 🔄(100%)
In response object, we create an authorization header; for a token (short lived, long living) refreshing token with a description created by (UserId, TenantId, User Role) by a secret key. The key is longer in size for security. 

Also, when the user logged in for the first time, we created an authorization access token for the authenticated user. Variables to create tokens: UserId, TenantId, User Role for the Tenant, with a Secret Key.  

**We embed the token description as user claim (in server after logging in). claims are usually encrypted. In the browser, it is safe for tenant and user-related variables are not exposed.**  

**We also add claim inside the HttpContext User object: UserId, TenantId, Tenant Role.** 

## Authorize Role Middleware: 🔄(100%)

1. For authorization in the server, we recreate the token (using the same variables with the secret key) and validate it in the server with the incoming request (Tenant Resolve Middleware). That is just validation.
2. But the role will be checked in the Role assignment middleware. Match formatted tenant role (UserId:TenantId:Role) from the User object to claim to extract the Role and validate against the allowed roles.  

In the middleware of Authorization Handler: the success assigns access policy for the user for a tenant.  Any one of the above unsuccessful validations will not allow the logged user to access the resources. Then, we allow middleware to give return success for accessing the allowed resources. The policy is configured in the service registration and uses the MVC default to authorize policy attributes. 

**[Above all are impkemented]**

## Stop Resource Access by Unwanted / by Mistake: 🔄(100%) 

**Anti-forgery second layer of security with resolved Tenant Id with suffix for the Cookie Name**

We have another attribute which is checked in parallel to assess a resource handled by default MVC Core in the middleware for stopping anti-forgery. It uses tokens on both sides to validate (custom validation). We made a few changes in the default option for this to work for multi-tenancy. Based on the nameof cookie we will validate. Cookie header is same. For Java Script and Ajax, it is default MVC request. Validatin is custom. Validation attribute is custom for action. In view the tag helper is default. We removed a (path based isolation cookies) for each tenant; rether keep the cookie name unique for each tenant. 

## We Handle Tenant Security Following Above Standards🔄(100%)
**for such multi tenant applications **

# Multi Tenant Architecture 🔄(00%) 🟥(20%)

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
4. Data Partitioning: Data is partitioned at the row level via EF Core's Global Query Filters. A global filter (based on TenantId) is set via an ITenantSetter interface. Using Dependency Injection (DI), the database is partitioned and isolated for each tenant. 
5. Keeping the code structure clean and memorable. Authorization code is mostly in one place, manageable and maintainable for more roles and policies and uses Jwt Token.
6. Request Security: Unsafe requests are protected by the Jwt Custom cookei options and validation.

## Solution Design & Architecture (.NET 8.0) 

### Background: 
I started my code to build and run for a client (small shop). It was previously made for an online marketplace. That was in .NET Framework 4.6 where you must deploy the portal in a windows or cloud based (Microsoft Azure) in Platform Service as a web application. Deployment infrastructure/platform service is a windows server based. 

When I started looking at the code and searching on the internet, I found that Microsoft doesn’t have any support over the framework because of security vulnerability. 

### I decided to do a migration of my code in .NET 8.0 because it has long term support plan and it is portable both in Windows and Linux servers. Also, the technology supports cross platforms including mobile devices and tablets.

## How to scale this application:

Even though you have separated the code into different projects (Web, Service, Domain, Infrastructure), they are compiled together and run as a single process on your Linux VPS.

Here is exactly how to scale your monolithic architecture, how introducing a separate API project changes the strategy, and how routing works across multiple instances.

Following are the strategies for scaling your current monolithic architecture and the potential benefits of introducing a separate API project:

### Strategy 1: Scaling Your Current monolithic Architecture

Because your entire application runs as a single process, you cannot scale just the "Shared Service project" on its own. You must scale the entire monolith together.

**Vertical Scaling First:**

Upgrade your Linux VPS to a higher tier with more CPU cores and RAM. This is the fastest, zero-code way to handle more traffic.

**Database Connection Pooling:**

Since you use a shared database, multiple application threads will compete for database connections. You must optimize your connection pool size in your appsettings.json connection string so the database doesn't choke.

**In-Memory Caching:**

Use .AddMemoryCache() in .NET 8.0 to store frequently requested, non-tenant-specific data directly in the VPS RAM to avoid hitting the shared database.

**Strategy 2: Scaling by Introducing an API Project**

If you split your architecture so that the Web Project (frontend/UI) communicates with a separate API Project (backend logic, services, and database access), you gain better scaling flexibility.

**Independent Scaling:**

You can deploy the Web project and the API project onto two completely separate Linux VPS instances.

**Targeted Resource Allocation:**

If your traffic involves heavy data processing, your API VPS will need high CPU/RAM. If your traffic is just users loading static web pages, your Web VPS can remain small and cheap.

**API Worker Pools:**

If the API becomes the bottleneck, you can duplicate only the API project across 3 or 4 separate Linux servers, while keeping only 1 server for the Web frontend.

### How one Instance Becomes Multiple Instances:

To turn one instance into multiple instances on a Linux VPS infrastructure, you use a process called Horizontal Scaling (Scaling Out).

You provision 2 or more separate Linux VPS instances (e.g., VPS 1, VPS 2, VPS 3).

**Deploy the Code:**

You deploy the exact same compiled .NET 8.0 published folder to all of those VPS instances.

**Keep them Stateless:**

For this to work, the application must not save files (like uploaded user images) or user sessions locally on the VPS disk. Sessions should be stored in a shared distributed cache (like Redis), and files should go to an object storage service (like AWS S3 or MinIO).

### How Traffic Routes to These Instances:

To split traffic among your multiple VPS instances, you must place a Reverse Proxy / Load Balancer in front of them. On Linux, Nginx or HAProxy are the industry standards for this.

**Step 1: [ Incoming User Traffic ]**
When a user goes to your domain name (e.g., www.example.com). The DNS resolves this to the public IP of your Nginx server:

**Step 2: [ Nginx Load Balancer ]**
(Public IP: 192.168.1.1) :
Seneds the traffic to your multiple VPS instances based on the load balancing algorithm you choose:

**Step 3:**
1. [ VPS Instance 1 ] :(Port: 5000)
2. [ VPS Instance 2 ] :(Port: 5000)
3. [ VPS Instance 3 ] :(Port: 5000)

The Entry Point:

Your domain name points directly to the single IP address of the Nginx server.

**The Routing Rules:**
You configure Nginx with the private IP addresses of your .NET 8.0 VPS instances.

**Load Balancing Algorithms:**

Nginx passes the traffic using specific rules:
**1. Round Robin:**
Sends request 1 to VPS 1, request 2 to VPS 2, request 3 to VPS 3, and repeats.

**2. Least Connections:**
Sends the next user request to whichever VPS is currently handling the least amount of active traffic.

**3. Ip Hash (Sticky Sessions):**
Ensures a specific tenant or user always hits the exact same VPS instance, which is helpful if you cannot use a shared distributed cache yet.


### The Best Practices by Research 

**Note:** 
Before planning for the multitenant application saas, I didn't consider or research the scaling part. Still it is applicable with curret design. (Vertical ad Horizontal Scale)

### Current Archiecture is:
**1. Monolithich** (one deployable unit, can be scaled horizontally and vertically). Above has details about scaleing for this application.
**2. Clean Architecture** (makes the code organnized and maintainable)

I started to create the architecture of the new solution, keeping in mind the best practices of design and architecture. 

**This objective created the Clean Architecture:**
1. The primary objective was to make the code modular, reusable, separation of the concerns, and readable while doing the code for the solution.
2. Another objective was to make sure it is Linux deployable and keeping the services completely separated from the presentation code (Web Project). Now, code is separated and using services but the API project is not there yet.
3. My plan to separate the services from the presentation is to make the web project light weight and reuse the same in different cross platform non-computer devices (Mobile, Tab).
4. Microsoft already has their own technology for app development (Xamarin) which uses the API (Web API) project hosted on any server. My plan was to keep the code common for everyone (web, mobile, & tablet). 

## Solution Design (Monolithic and Clean Structure)

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

# Links Google AI used to generate Contents based on my search ad questions:

## For Jwt Token for Multi Tenant Impementation

[1] [https://andrewlock.net](https://andrewlock.net/implementing-custom-token-providers-for-passwordless-authentication-in-asp-net-core-identity/)
[2] [https://stackoverflow.com](https://stackoverflow.com/questions/42593188/how-to-customize-reset-password-token-in-asp-net-core-identity)
[3] [https://www.mindbowser.com](https://www.mindbowser.com/spring-security-multi-provider-authentication/)
[4] [https://csharp-video-tutorials.blogspot.com](https://csharp-video-tutorials.blogspot.com/2019/10/aspnet-core-custom-token-provider.html)
[5] [https://learn.microsoft.com](https://learn.microsoft.com/en-us/previous-versions/aspnet/dn613280%28v=vs.108%29)
[6] [https://aspnet-docs-example.readthedocs.io](https://aspnet-docs-example.readthedocs.io/en/latest/autoapi/Microsoft/AspNet/Identity/DataProtectorTokenProvider/)
[7] [https://www.stevejgordon.co.uk](https://www.stevejgordon.co.uk/asp-net-core-identity-token-providers)
[8] [https://dotnettutorials.net](https://dotnettutorials.net/lesson/how-to-store-tokens-in-asp-net-core-identity/)
[9] [https://docs.tetrate.io](https://docs.tetrate.io/service-bridge/operations/multiple-iam-keys)
[10] [https://dotnettutorials.net](https://dotnettutorials.net/lesson/how-to-store-tokens-in-asp-net-core-identity/)
[11] [https://stackoverflow.com](https://stackoverflow.com/questions/59394909/asp-net-core-identity-make-tokens-email-confirmation-password-reset-etc-va)
[12] [https://stackoverflow.com](https://stackoverflow.com/questions/77986146/asp-net-core-custom-token-provider-use-across-different-projects)
[13] [https://learn.microsoft.com](https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/configuration/overview?view=aspnetcore-10.0)
[14] [https://stackoverflow.com](https://stackoverflow.com/questions/74110441/net-core-identity-server-custom-token-provider-generating-long-token-string)

## Configure Session Options

[1] [https://antondevtips.com](https://antondevtips.com/blog/master-configuration-in-asp-net-core-with-the-options-pattern)
[2] [https://www.freedesktop.org](https://www.freedesktop.org/software/fontconfig/fontconfig-user.html)
[3] [https://commons.apache.org](https://commons.apache.org/proper/commons-configuration/userguide/howto_builders.html)
[4] [https://medium.com](https://medium.com/asp-dotnet/why-anti-forgery-tokens-use-in-net-core-75ab11f6c2da)
[5] [https://medium.com](https://medium.com/@robert0321/asp-net-security-best-practices-protecting-your-web-applications-a9ea012d7434)
[6] [https://www.youtube.com](https://www.youtube.com/watch?v=G910vWOdhQ8)

## Default ValidateAntiForgeryToken

[1] [https://www.syncfusion.com](https://www.syncfusion.com/blogs/post/10-practices-secure-asp-net-core-mvc-app)
[2] [https://learn.microsoft.com](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/controller-methods-views?view=aspnetcore-10.0)
[3] [https://www.itb.ec.europa.eu](https://www.itb.ec.europa.eu/docs/guides/latest/installingValidatorProduction/index.html)
[4] [https://steeltoe.io](https://steeltoe.io/docs/v3/management/cloud-foundry.html)
[5] [https://www.linkedin.com](https://www.linkedin.com/posts/amrsaafan_one-of-the-common-issues-that-appear-in-any-activity-7385279767153037312-18ab)
[6] [https://docs.umbraco.com](https://docs.umbraco.com/umbraco-forms/13.latest/developer/configuration)
[7] [https://www.red-gate.com](https://www.red-gate.com/simple-talk/development/web/how-to-secure-legacy-asp-net-mvc-against-csrf-attacks/)
[8] [https://www.c-sharpcorner.com](https://www.c-sharpcorner.com/article/learn-about-action-filters-in-asp-net-mvc/)
[9] [https://andrewlock.net](https://andrewlock.net/automatically-validating-anti-forgery-tokens-in-asp-net-core-with-the-autovalidateantiforgerytokenattribute/)
[10] [https://fiware-orion.readthedocs.io](https://fiware-orion.readthedocs.io/en/master/orion-api.html)

## Global Excepton Handling

[1] [https://medium.com](https://medium.com/@Moltech/middleware-in-net-enhancing-request-handling-with-custom-logic-7bf4f7a648a6)
[2] [https://medium.com](https://medium.com/@mina.abdo/middleware-and-request-pipeline-in-asp-net-core-73044052c88a)
[3] [https://www.linkedin.com](https://www.linkedin.com/pulse/kiss-try-catch-goodbye-middleware-cleaner-more-manageable-islam-5dmhc)
[4] [https://medium.com](https://medium.com/we-are-developers/middleware-in-asp-net-core-c94d82dc3ef6)
[5] [https://medium.com](https://medium.com/@AntonAntonov88/handling-errors-with-iexceptionhandler-in-asp-net-core-8-0-48c71654cc2e)
[6] [https://blog.elmah.io](https://blog.elmah.io/error-logging-middleware-in-aspnetcore/)
[7] [https://learn.microsoft.com](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-10.0)
[8] [https://www.infoworld.com](https://www.infoworld.com/article/2257411/how-to-use-serilog-in-aspnet-core.html)
[9] [https://antondevtips.com](https://antondevtips.com/blog/logging-best-practices-in-asp-net-core)
[10] [https://last9.io](https://last9.io/blog/serilog-configuration/)
[11] [https://www.c-sharpcorner.com](https://www.c-sharpcorner.com/UploadFile/a3d5d0/structured-logging-with-serilog-and-seq-part-2/)

## View and Analyze Your Serilog/Log files

[1] [https://dzone.com](https://dzone.com/articles/multi-tenancy-implementation-using-spring-boot-and)
[2] [https://oneuptime.com](https://oneuptime.com/blog/post/2026-01-27-nodejs-multi-tenancy/view)
[3] [https://oneuptime.com](https://oneuptime.com/blog/post/2026-01-26-multi-tenant-apps-dotnet/view)
[4] [https://dev.to](https://dev.to/heterl0/building-multi-tenant-applications-with-nextjs-a-custom-subdomain-approach-5105)
[5] [https://medium.com](https://medium.com/@odenigbo67/subdomain-driven-schema-isolated-multi-tenancy-using-nestjs-872c0f279b44)
[6] [https://dev.to](https://dev.to/leandroveiga/building-a-multi-tenant-minimal-api-architecture-with-net-8-40he)
[7] [https://milantoncic.medium.com](https://milantoncic.medium.com/structured-logging-in-asp-net-core-using-serilog-421de2679867)
[8] [https://www.euphoricthought.com](https://www.euphoricthought.com/enhancing-log-management-with-serilog-and-graylog/)
[9] [https://blog.angelwebdesigns.com.au](https://blog.angelwebdesigns.com.au/structured-logging-with-serilog-in-asp-net-core/)

## Keep Log Seperately in Places (layers)

[1] [https://www.youtube.com](https://www.youtube.com/watch?v=T-EiJQOKNpk)
[2] [https://www.linkedin.com](https://www.linkedin.com/pulse/structured-logging-logs-management-case-study-using-aspnet-sofer-%E7%A7%8B%E7%A6%8F)
[3] [https://www.reddit.com](https://www.reddit.com/r/selfhosted/comments/14ykic7/simple_app_for_monitoring_server_log/)
[4] [https://www.alexhyett.com](https://www.alexhyett.com/app-logs-dotnet-seq-5-serilog/)
[5] [https://medium.com](https://medium.com/@nithidol/spring-boot-3-logging-for-monitoring-1a42e157bcd9)
[6] [https://dev.to](https://dev.to/iamcymentho/mastering-distributed-tracing-with-serilog-and-seq-in-net-fp)
[7] [https://benfoster.io](https://benfoster.io/blog/serilog-best-practices/)
[8] [https://yisusvii.medium.com](https://yisusvii.medium.com/structured-logging-in-net-8-isolated-a-comprehensive-guide-3da16ce62e4b)
[9] [https://satyampushkar.medium.com](https://satyampushkar.medium.com/serilog-logging-to-console-seq-elasticsearch-file-using-dotnet6-d9536b534209)
[10] [https://dev.to](https://dev.to/iamcymentho/mastering-distributed-tracing-with-serilog-and-seq-in-net-fp)
[11] [https://logmanager.com](https://logmanager.com/blog/log-management/log-file-analysis-tools/)
[12] [https://www.papertrail.com](https://www.papertrail.com/solution/tips/logging-in-docker-how-to-get-it-right/)
[13] [https://medium.com](https://medium.com/django-unleashed/get-visibility-into-your-docker-container-logs-with-grafana-loki-of-a-django-application-9584bddfe540)
[14] [https://oneuptime.com](https://oneuptime.com/blog/post/2026-01-30-centralized-logging-architecture/view)

 

