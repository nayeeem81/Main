# Multi-Tenant Request Pipeline

  [ Incoming HTTP Request ] 
          │
          ▼
┌────────────────────────────────────┐
│   1. Exception Handling & HSTS     │ <--- Global safety net (Explain Later)
└────────────────────────────────────┘
          │
          ▼
┌────────────────────────────────────┐
│   2.Tenant Resolution Middleware   │ <---   Custom Middeware:TenantResolverHandlingMiddleware
└────────────────────────────────────┘        Extracts Tenant via Header/Subdomain/Subdirectory
          │
          ▼
┌─────────────────────────────────┐
│ 3. Routing Middleware           │ <--- Matches URLs to endpoints
└─────────────────────────────────┘
          │
          ▼
┌─────────────────────────────────┐
│ 4.Tenant Security Middleware    │ <--- TenantSecurityMiddleware: Does URL Tenant match User 
└─────────────────────────────────┘      Tenant?
          │
          ▼
┌─────────────────────────────────┐
│ 5. Authentication / Authorize   │ <--- Parses user claims (User claims they belong to Tenant B)
└─────────────────────────────────┘      Validates identities (Tenant-aware)
          │
          ▼
┌─────────────────────────────────┐
│ 6. MVC Controller & EF Core     │ <--- App logic & isolated DB query execution
└─────────────────────────────────┘


## Global Safety Net

**[ Incoming Request ] ──► 1. Global Safety Net (Captures errors on the way back out)
                            │
                            ▼
                         2. Tenant Resolution Middleware
                            │
                            ▼
                         3. Controllers / DB Layer (If an exception throws here...)
                            │
        [ Exception Bubbles Up ] ──────► Catches it, logs it, and returns a friendly HTML view.**

 # Middleware Order

 [ Middleware Order in Program.cs ]

 1. TenantResolutionMiddleware ──► Resolves Tenant A via Subdomain/URL
 2. UseRouting()           
 3. UseAuthentication()        ──► Parses user claims (User claims they belong to Tenant B)
 4. TenantSecurityMiddleware   ──► MATCH CHECK: Does URL Tenant match User Tenant? (FAIL -> 403)    
 5. UseAuthorization()

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

**We are using monolithich architecture, the published output will a single application instance and a single shared wwwroot.The MVC application folder structure separate Area controllers from standard controllers in this design pattern.**

Main.WebAppCore/
│
├── Areas/
│   └── CompanyContent/                  <-- The Area name
│       ├── Controllers/
│       │   └── ManageProductController.cs
│       └── Views/
│           └── ManageProduct/
│               └── Index.cshtml
│
├── Controllers/                         <-- Standard non-area controllers
│   └── HomeController.cs
│
├── wwwroot/                             <-- Common assets used by everyone
│   ├── css/
│   │   └── site.css
│   └── js/
│       └── site.js
------------------------------

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
