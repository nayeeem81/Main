# [YouTube: Multi-Tenant SAAS Stores (Asp.Net Core Mvc .Net 8.0)](https://www.youtube.com/playlist?list=PLIK1B9HNGKX8)
 
## The Project Progress:
[GitHub - nayeeem81/Main: Multi-Tenant Store Shared Database [Asp. Net Core MVC (.NET 8.0)] · GitHub](https://www.linkedin.com/pulse/one-paper-which-fostered-progress-naim-ul-islam-prodhan-vrtuc/)

**May 2026:**
Solution Design (Monolithic and Clean Structure): I had a solution developed with .NET framework 4.6. I opened the solution and did some research on the security features of the previous solution (asp.net mvc web application). The .NET Framework 4.6 has serious security-related vulnerability and Microsoft stopped the support for the framework. It is obsolete. After that, the next Microsoft Standard (which is for cross platform) and has support to deploy in Linux environments has options for me to migrate the solution. I found that earlier .NET Core (.NET Standard) passed for quiet long years. Now, Microsoft finally put the full focus to keep them all in the one name (.NET) with long term support with cross platform support and deployment flexibility. It has already been introduced in the Identity Membership (which is more reliable and a lot of the code ad tables (entities); the flow of Sign in, sign up and related security concerts are taken care of by Microsoft Identity. 

I just started with the .NET Standard 2.0 (ASP.NET Core Web App) and started migration. At some point, my plan was not to start with .NET 8.0. GitHub Copilot forced me to move to .NET 8.0. Previously, I used TFS in very professional workplaces. This work was personal and I planned to create the solution as a template and keep it in the GitHub public repository. I was working for me to migrate the existing solution to deliver to my friend for their store. It was voluntary work. I was sick and recovered from the mild stroke. I wanted to volunteer for the work open and useable for all.  

**June 2026:**
I was almost migrated to the .NET Standard 2.0; I asked the GitHub Copilot to modernize the entire solution. I did that just after I got the latest update from Visual Studio Community Edition. Suddenly, the solution stopped building, and all of the NuGet Packages had support after Modernize, but one package (most probably image related); didn't get support. Removing that; it has other built errors. It was not building with .NET 10.0. After troubleshooting the build errors; I reached to .NET 8.0 the solution had has zero build errors and warnings. 

In the meantime, I integrated the Identity membership in two dB contexts classes (identity & business entities) but kept the same connection string. So, I had one database. I was required to write an entire new code for the flow (sign in, sign up) and the product (save, update, delete) codes. I reused some of the past codes but not the entire files. It was a new design (with less properties) and only the necessities were there. 

**July, Aug, Sep 2026:** 
I had a plan that I will use the code for multiple stores. A link in google search moved to know more about the Multi-Tenant, Tenancy, and examples. 

I have already created the monolithic and clean architecture; I had plan to make the code following the best practices (solution architecture): separation of concerns, keep the data infrastructure separate from service, data transfer objects just go to controllers (web project / Api project). Views will have their View Model (only needed properties) and will have the mapping. I didn't use any auto mappers or similar mappings packages (since the scope was small and it is robust & reliable). After following the best practices, I completed the store with Identity Membership. 

The GitHub Copilot Modernizations; stopped the tested solution (new but with the required modules already done. Some bug fixing was pending. CMS logic was in my brain (planned). I will do the same but with less steps to configure the pages. I researched the best practices (how to add a panel, drag/drop, up/down). I tried to use them as suggested in the links from Google AI. 

At his point; I started to move the architecture into the multi-tenant architecture. I started with hope and never even thought that it had a lot of challenges and architecture must meet some of the context specific isolations. I got a research paper and the paper stated all of SaaS related architectural issues which must be met for isolation, few for Tenant specific roles, an email should be able to use for different tenants, there are audits for such multi-tenant web applications, how to run the .NET application behind the reverse proxy and do the SSL termination, code for the Nginx config (cookie pass through, etc.), start static files sending before the remaining processing is completed (stop race conditions)  in the middleware pipeline. I needed to decide on the tenant resolution: for (domain/sub domain/sub directory) all or one or two and researched to get an idea to work on which. Over time, I decided to keep each of the options open. At some point, I decided to keep only domains.  

When the anti-forgery (global: submit, fetch, jQuery ajax), cross tenant bleeding (cookies with tenant suffixed, using Jwt - tenant specific, rotating refresh token (with state, long lived) and access token, middleware for JWT based authentication, middleware to check the tenant context, the middleware for (tenant role and policy based authorization), query filters to keep tenant database isolated, keep the logo (separate for each), theme based on global variables tenant suffixed (site.css). Using relative path for accessing the dynamic URLS, it took quite a few months to meet the goals which the paper states mandatory architectural design goals.  

Still working on the theme issues (changing) and with only domain-based tenant resolution tested locally. 

Thanks

Naim   -   OG      ( Short Key )
08 Sep 2026




This is the paper which gives me the most insights and advice on completing the multi-tenant architecture: 
___
Multi-Tenant SaaS Architectures: Design Principles and Security Considerations Ritesh Kumar Independent Researcher Pennsylvania, USA,  
ritesh2901@gmail.com  
06052841.pdf 

# Fostering Education: 
I decided to contribute to the community; I have the experience. The experience is a journey with the communities I worked with in software and technology. I started in 2000. I learned from them; they are my work associates and affairs. I have learned from the academics; the journey of engineering started, the teachers. The actual area of technology and engineering is important for me to share and deliver in an impactful manner; it is resilient and sustainable. 

To make it sustain; we do foster; that is education fostering.  Because knowledge and experience come with a price at that time. You can never buy the time (experience 26 years). Fostering the education & experience, transfer and adoption are very much required to deliver impactful results. Here is the plan I just prepared after researching and aligning with a plan of fostering education. I found that education fostering is very important. 

## 1. Code Fostering (GitHub Repository):  
With the knowledge of technology-based work experiences; I can prepare a container (consider a GitHub repository). It is a part of the fostering education (knowledge and experience based, delivery of code). In the fostering process, code is a part (fostering education); I can deliver some engineers to read, learn, enrich and adopt. To make the code fostering more impactful; to make the code; into a software product which can be usable in a production environment, not just a prototype.  

## 2. Hardware Fostering:  
To make the code fostering working and impactful, stacks technologies, domain, hardware, datacenter vendors host them; are all part of the ecosystem. 

## 3. Service Fostering:  
Maintainers, the operation of the data related to the application, smooth maintenance for the users. 

## 4. Fostering the Business Knowledge: 
Business Executives can use their business knowledge to bring them (stores) to a place, engage them, educate them, and get the most value from the code, hardware and the production ecosystem. 

## 5. Users Fostering (Small Businesses/Stores): 
These are the people who make the actual use of the working product or the software in the cheapest possible way that education fostering is impactful.  


# Multi-Tenant ASP.NET Core .NET 8.0 (Multi-Tenant SAAS)

## 🥃 Run Code in your Laptop (28 Aug 2026)

### 1. One Hour Video for Kids to a Cook (kitchen) or a Computer User from any Discipline follow the steps; You can run the Application in your Laptop.
### 2. It means, it is easy. Do not get scared to setup the code and tools in the laptop.
### 3. Download the zip file for copy and paste in the required places shown in the video. [Commands.zip](https://github.com/user-attachments/files/31552456/Commands.zip)

## VIDEO 1 [Step 1: VS 2026 Download](https://youtu.be/JX4WnexPvPE) 

## VIDEO 2 [Step 2.0 Download SQL Server Express](https://youtu.be/mvb8XGVaHDQ?si=d-hAprSIAfwubz_R)

## VIDEO 3 [Step 2.1 SQL Server (For Exploration)](https://youtu.be/n7fg_V2-5FU?si=JBN0xZxoC2l0_qSJ)

## VIDEO 4 [Step 3 Clone Git Hub Repository](https://youtu.be/2zRAFTmoNdM?si=Y8Bx5RrJkWDVn_th)

## VIDEO 5 [Step 4 Create Database (Add Migration & Update Database)](https://youtu.be/a2VEGjYfk8Q?si=4geGYzeJl0Y6n_kd)

## VIDEO 6 [Step 5 Copy and Paste Host File (Know the database)](https://youtu.be/N8gvuxiHJ8w?si=jHxS0VjMQShiIfQZ)

## VIDEO 7 [Step 6 - Download Nginx, Add Settings, Create Certificate](https://youtu.be/to3lt2XTuqI?si=FZwUY7Oo_TKi_zBl)

## VIDEO 8 [Step 7 Run The Application and Nginx, Test in Browser](https://youtu.be/Q2j-4RyEliY?si=TCIoO9aOWubE4d_D)


# Latest Videos 🆕🎬

1. [Video: Bottom Header - 1. Dynamic Product Category Menu, 2. Fixed Scroll-Menu (Home Page/All Pages)](https://1drv.ms/v/c/93d70fb51193cb6b/IQDIvxEUH5tcR5AofCnZmpZfAYgosaxW98BVEvhy8FOVbx0?e=1Zex41) **NEW!**
2. [Video: Home Page: Setup (Panels: Add - Drag & Drop, Edit - Up & Down](https://1drv.ms/v/c/93d70fb51193cb6b/IQB4198G_WMSRpVZBD2l6J5DARgBU77fxnTsPz2KCSk37eE?e=ZBSUSb) 
3. [Video: Product UX, Page Setup (Panels, Drag & Drop](https://1drv.ms/v/c/93d70fb51193cb6b/IQDP8Vj1-bABR6M5FOHG1L3eAXVYFZk3p0-CCl2cSB2nAvc?e=d6g8c6) 
4. [Video - Tenant Product Module Full](https://1drv.ms/v/c/93d70fb51193cb6b/IQCiwVnAVKW4SpTsNHp_KRpnAUll_aWmsJVLTm5ICW5szCI?e=7ebxyv) 
5. [Video Multi Tenant with Theme and Logo](https://1drv.ms/v/c/93d70fb51193cb6b/IQCbPoQogKj0Sp4JcSrTqlTdAaTPrdYOCt7i2xo-eiRa1Nk?e=zW63vs) 
6. [Latest video](https://1drv.ms/v/c/93d70fb51193cb6b/IQAdpVbw9SSUTIkiv8jbxnrzAWtZcojWwLbMsDhPbqOcFBc?e=bbNRme)
7. [From VS and Nginx](https://1drv.ms/v/c/93d70fb51193cb6b/IQA-f-V2ZcA3RKPQmD1k3q_QAaFFDqIFFMMyGS7K9kcHEQ8?e=Z6BhbS)
8. [Theme Seed & Database SQL Script](https://1drv.ms/u/c/93d70fb51193cb6b/IQAUlLSDaKNySocpGhJc6rwRAZbMUTp5WKFcbGST3aEP4Ak?e=Kl0cxT)
9. [Video - Debugging of Tenant Product & Photos](https://1drv.ms/v/c/93d70fb51193cb6b/IQDVNYSAQMf8Rb45NWgIdiaHAVuqanStaSjo0pe7yMQjdio?e=GJ37yG)
10. [Video - Refined Theme](https://1drv.ms/v/c/93d70fb51193cb6b/IQBmlArEE1UzTYF15imEdp-7AV6PBRUxTHaiPr0GXF_nPm4?e=0O69CQ)
11. [Videos - ](https://1drv.ms/v/c/93d70fb51193cb6b/IQBfJQMns98vRo8xccLLZUWFAWyI4iURrX5QWuM9g4Iia-Q?e=CXsNkt) 
12.  [VIDEO 4th Sep 2026: Home Page, Configuration](https://1drv.ms/v/c/93d70fb51193cb6b/IQCijybVm3feT5cbg_rdlmLSAXTbDHlti52qHOttcTj-bTI?e=KXbDRM)
13.  6th Sep 2026: VIDEO [Home Page, Menus, Menu Type, SubType, Product Details](https://1drv.ms/v/c/93d70fb51193cb6b/IQBNefqwFvPLR4EQH-iAaH8dAXEvuoWP7pDzVvotBTowS9Q?e=D8LL2u) *NEW VIDEO!*


# Multi-Tenant ASP.NET Core Middleware Pipeline Architecture

This document describes the execution sequence and structural design of the HTTP request lifecycle pipeline. The middleware chain is meticulously ordered to eliminate race conditions, preserve multi-tenant isolation, handle static assets efficiently, and enforce cross-origin security rules.

[Multi-Tenant ASP.NET Core Middleware Pipeline Architecture - Latest](https://github.com/nayeeem81/Main/wiki/.NET-8.0-Pipeline-(Progrm.cs)))

   1. HTTP Request
   2. Error & TLS Enforcement        ──► StatusCodePages, HttpsRedirection
   3. Edge Security (CORS)           ──► Appends headers before any terminal handlers
   4. Static Asset Optimization      ──► WebOptimizer, StaticFiles (Bypasses Tenancy/Auth)
   5. Endpoint Identification        ──► UseRouting (Enables Endpoint Metadata Inspection)
   6. Tenancy Context Layer          ──► TenantResolver (Stores TenantId in HttpContext.Items)
   7. Isolated Session Layer         ──► TenantSessionCookie, UseSession (Tenant-Scoped)
   8. Context Localization           ──► CustomLocalization (Tenant-Configured Culture)
   9. Refresh Token                  ──► Recreate access token when expired or Browser reopens 
   10. Security Auth Matrix          ──► Authentication, TokenValidation, Authorization
   11. Anti-Exploit & Optimization   ──► Antiforgery, OutputCache (Context-Aware)
   12. Endpoint Controllers          ──► MapControllers, MapControllerRoute


# Core Structural Rules & Dependencies

### 1. Global CORS Placement
* **Rule:** Must execute *before* `UseStaticFiles` and `UseRouting`.
* **Reasoning:** Cross-origin font/asset requests (`.woff2`, `.json`, `.svg`) require immediate `Access-Control-Allow-Origin` header injection before the static asset handler terminates the request. Furthermore, HTTP `OPTIONS` preflight checks must be caught and short-circuited instantly before wasting processing overhead on routing or tenancy resolution engines.

### 2. Routing Before Tenancy
* **Rule:** `UseRouting()` happens strictly *before* `TenantResolverHandlingMiddleware`.
* **Reasoning:** All tenants point to the same structural endpoint locations. By identifying the route blueprint early, the `TenantResolverHandlingMiddleware` can query `HttpContext.GetEndpoint()` or `HttpContext.GetRouteData()` to extract tenant slug variables safely from the request trajectory.

### 3. Tenancy Data Lifespan
* **Rule:** The `TenantId` or resolved tenant wrapper must be injected into `HttpContext.Items`. I used this concept using a scooped tenant setting context. I used scopped ITennantSetter (DI) for a single source of truth. 
* **Reasoning:** `HttpContext.Items` is a thread-safe, scoped dictionary structured for the lifetime of a single HTTP request, preventing cross-tenant data-leakage during concurrent, parallel async request flows.

### 4. Tenant-Scoped Cookie Sessions
* **Rule:** `TenantSessionCookieMiddleware` runs *after* Tenancy resolution but *before* `UseSession()`.
* **Reasoning:** The custom cookie middleware extracts the validated `TenantId` from `HttpContext.Items` and updates the `SessionOptions.Cookie.Name` (e.g. `.AspNetCore.Session.TenantA`) dynamically. This isolates user states into tenant-distinct partitions before the `UseSession()` engine builds out the state pool.

### 5. Authentication, Antiforgery, and Caching Topography
* **Rule:** `UseAuthentication()` runs *before* `UseAntiforgery()` and `UseOutputCache()`.
* **Reasoning:** Antiforgery verification tokens rely on the security identity of the authenticated caller; evaluating it early treats verified sessions as anonymous, breaking form submissions. Similarly, `UseOutputCache()` must evaluate downstream of authentication/authorization to prevent caching private or role-restricted data across multi-tenant barriers.

# Host File & Nginx Server Config

[Host File Latest]([https://github.com/nayeeem81/Main/wiki/Host-File-(C:%5CWindows%5CSystem32%5Cdrivers%5Cetc)-Laptop](https://github.com/nayeeem81/Main/wiki/Host-File))

<img width="921" height="459" alt="hostfilescreenshot" src="https://github.com/user-attachments/assets/08bcb32e-01a4-4c5e-86c4-f7cc0346ece7" />


[Latest Nginx Config]([https://github.com/nayeeem81/Main/wiki/Host-File-(C:%5CWindows%5CSystem32%5Cdrivers%5Cetc](https://github.com/nayeeem81/Main/wiki/Nginx-Config)

<img width="558" height="253" alt="certificate" src="https://github.com/user-attachments/assets/f7e6c960-78c6-4e3b-a771-aa001b58300d" />

# Kestrel server

- The Kestrel server is the lightweight (🔄100% tested), cross-platform (windows 🔄100% tested), high-performance (🔄100% tested) web server built directly into .NET.  
- When the multi-tenant .NET 8.0 web application runs, Kestrel acts as the inner server that directly hosts the app code and handles raw network connections (like HTTP and WebSockets 🔄100% tested). 
- From the Visual Studio, in the development mode and in the context of testing for multi-tenant architecture development, this is the role of Kestrel server. Here is exactly how Kestrel fits into the multi-tenant setup and why it matters to this current architecture: 

**1. Kestrel's Role in this Architecture** 
From the Visual studio IDE, when we click the run button (debug menu) for the code in my development environment, Kestrel is the process start running on the machine/laptop (listening on a port like localhost:52899). The kestrel process (🔄100% tested: web server built directly into .NET) is running now and listening. In the power shell, we write commands and run the nginx process (reverse proxy) which listens to (http and https). Then we type to test the multi-tenant custom domains. 

# Flow  

In the browser, type and visit https://finearts.test, the request first goes to the local Nginx proxy. Nginx terminates the SSL encryption and proxies (forwards) the raw traffic to Kestrel via HTTP on my web app localhost port (5000). (🔄100% tested). In the solution of the application, we have launchSettings.json, which is configured to listen to 5000. 

**"applicationUrl": "http://127.0.0.1:5000"** 

# The Handshake

When your browser opens that WebSocket connection in the browser inspect (web tool), I tested and see, Kestrel is the server that accepts the 101 Switching Protocols handshake and holds the TCP pipe open to process your background real-time data or development Hot Reload. (🔄100% tested) 

## Why does the middleware order pipeline and configure headers Is very important? 

Because Kestrel sits behind Nginx, it does not naturally know what domain the user typed into their browser—it only sees localhost:52899. 

This is exactly why that line at the very top of your request pipeline is so critical: 

**_ = app.UseForwardedHeaders(forwardedHeadersOptions);** 
 

This line forces Kestrel to read the proxy headers passed by Nginx (X-Forwarded-Host). It forces Kestrel to override its own local port data and rewrite the internal request context host to finearts.test or lifestyles.test. Without this, TenantResolverHandlingMiddleware would only ever see localhost from Kestrel, and your cache lookups would fail. (🔄100% tested) 

**Kestrel Optimization for Multi-Tenancy** 

- Kestrel handles thousands (tested but not thousands, performed well 🔄100% tested) of simultaneous connections (🔄100% tested) with almost zero overhead, making it highly efficient for multi-tenant applications.  
- It is designed to be paired with a reverse proxy (like Nginx) in production. (🔄100% tested) 
- Nginx handles public-facing responsibilities (SSL certificate management (🔄100% tested), DDoS protection (🔄100% tested), static file caching. 
- Kestrel handles core application logic (running middlewares, managing Session (tenant isolated session) and processing database queries (tenant query isolation), Jwt Authentication (tenant isolated) global anti forgery validation. 

# Nginx (Localhost with Custom Domains)
Nginx is a reverse proxy for a web server, which can listen to both http and https protocols and terminate the https secure certificate and forward to a Kristel server running a .NET Web Application.  

Ngixn is listening to standard port 80 for http and 443 for https.  

In this Nginx Config script, of the nginx, the test is done in the local windows environment. The browser sends the request to the host file in the laptop host file. If the request (domain) to the host file is not present, it forwards browser request to the local internet service provider's DNS server.  

**Host File: hosts (open with note pad)** 
Location: C:\Windows\System32\drivers\etc
Multi-tenant Local Development Domains (Host File)

```host
127.0.0.1 finearts.test
127.0.0.1 lifestyles.test
127.0.0.1 localhost
127.0.0.1 app.internal
```


The host in the local machine is configured with two domains. It is then forwarded to the nginx web server. In the Nginx server config, it knows the browser hosts (in Nginx config), and it strips the certificate, then forwards to .NET 8.0 web application pipeline.  

.NET Krestel Server (launch settings: is listening to the port: 5000 and with http://127.0.0.1:5000) .Net middleware pipeline. The communication between Nginx and .NET Krestel server is using http protocol. 

**Exe File Location:** D:\nginx-1.31.3\

**File Name: nginx.conf** 

**Location: D:\nginx-1.31.3\conf** 

You can keep the folder in any drive. For making the path short, in the root of any drive is good to go. The following are the contents of the Nginx configuration file. 


# PowerShell Commands (Nginx) 

```
cd D:\nginx-1.31.3\conf 

Start-Process .\nginx.exe\ 

Start-Process -FilePath "D:\nginx-1.31.3\nginx.exe" -ArgumentList "-s reload" -NoNewWindow –Wait 

.\nginx.exe -s stop 
```
# PowerShell Commands (Create Certificate) 

**(D:\nginx-1.31.3\conf\ssl)**
```
mkdir ssl 

cd ssl 

.\mkcert.exe localhost finearts.test lifestyles.test 
```




# 🏬🛍️Shopping Mall Web App (Multi-Tenant SaaS)

**We are developing a multi-tenant stores web application. A store is called tenant ⇄ A tenant is a store. (vice versa)** 

## **🛍️store is a 🛍️shop inside 🏬shopping mall**

The owner of the shop/store can be a legitimate business (with a trade license) or individual (not a professional seller, a freelance seller using the store for his or her own product selling purpose).  We treat the tenant as a shop. Anyone registering in the multitenant web application is the admin of a shop. The registration is for the shop with an email address from the multi-tenant portal:

## www.tenantors.com

The registration is for the shop with an email address from the portal (example: www.tenantors.com). An email can be used for multiple tenants. Just like a single sign in across all tenants. Read followng for understanding the detail, about the multi-tenant web application. We explain first, the application related keywords and objects. We must undertand them to get a complete view about the concept & story of the multi-tenant SaaS.

## Tenant🛍️ & User🔑 

## Further Details To Understand the story better next...

## 1. 🛍️Tenants:
Tenants are a shop. Tenant is created with an email address. The person who owns the email address is the first admin user of the shop, may be the owner. He can create it on behalf of the owner.

### 2. 🔑Users:
Initial tenant creation, this multi-tenant web application; assigns the user (email) as an admin of the shop. Then, he/she can add more users by invitation (email) link. A user can own multiple Tenant with the same email address. He/she can work under a Tenant (from an invite) keeping his/her own tenants. A user can work for multiple tenants with one email address.

## 🛍️Tenant Types (According to Hosting Plan)

<img width="758" height="152" alt="RootWebToAlphaBetaPath" src="https://github.com/user-attachments/assets/f666510e-8ec9-45e2-a44c-915d8b6d81da" />


## Tenant name is: **kaiassociates**

**kaiassociates** can host in 3 differnt ways to use our **Multi Tenant Web App Store Features**

## Domain: www.kaiassociates.com
After creating a tenant, the user can add a domain for the tenant. The tenant needs to add the web application Ip address to his domain provider as his shop website host. The tenant will use their own domain URL and use the multi-tenant web application provided with shop-related features.

## Subdomain: www.kaiassociates.tenantors.com
**Unique username is the name of the subdomain**
After creating a tenant, the user can add a sub domain for the tenant under the web application domain. The tenant needs no Ip address or domain provider. Rather, the tenant needs to provide a unique name (if nobody is using the same name) as their sub domain. The tenants use their own subdomain-based URL and use the multi-tenant web application provided with shop-related features.

## Subdirectory: www.tenantors.com/kaiassociates/
**Unique username is the name of the subdirectory**
After creating a tenant, the user by default gets a directory for the tenant under the web application domain. The sub directory will be created by the unique username during the creation of the Tenant. The tenants use their subdirectory-based URL and use the multi-tenant web application provided with shop-related features.

## 👤Login approach
Tenant is registered by an user email. Verify email link sent upon registration form submission. User gets admin role and a subdirectory for the tenant (default). He/She must must verify the registered email to activate the tenant account. 

Tenant user can login and access the store work space only using verified email.

**After email verification, user will login in the website, from where the user was registered. (example:tenantors.com)**

1. After successful Login, the tenant admin can add a domain for his own store. Once the domain is configured, the user can login from there own domain.

🆔🪪www.kaiassociates.com

2. After successful Login, the tenant admin can configure the subdomain in the website. They will get a sub domain for his store. the user will continue login from the same website or with the subdomain website.

📲🪪www.kaiassociates.tenantors.com

3. After successful Login, the tenant admin can configure the domain or subdomain in the website. They will get a domain/sub domain for his store. Otherwise the user from default subdirectory workspace continue login from the same website.

🏢🪪www.tenantors.com/kaiassociates

## Summary:

## 🛍️ Tenant View:
In the above three cases: it is the way how the tenant (store) users will login, use their own work space and how customers will find their online store. The tenants may boost their online presence using media, social media or use web url in a name card.

## 🛠️Technical View:
It will confirm the isolation of the store identity and their  independent work space for each tenant inside the multi tenant web application.

## 🛍️Tenant Work Space

## Tenant Shop Website (URL):

**Tenant has it's own work space. Users can upload products, add ads (images, links) and arrange the pages by their choice.**

**Think the workspace as your shop in a shopping mall:**

**You plan in similar concept: the way the owner and managers plan, design interior and arrange products of a shop to display!**

## 🏢 Store Features (Tenant):

1. **🏢🔄(80%) Tenant Profile: (domain setup / buy domain, invite users)**   
2. **🔒🔄(100%) Security & Isolation: Manage user account and change password**                    
3. **📦🔄(100%) Product Manager: Manage Products (add, edit, delete, view) with admin dashboard**                  
4. **🖼️🔄(100%) Advertisement Manager: (create ads with images, texts, links)** 
5. **🎛️🔄(100%) Page Manager: (small CMS to organize the products and ads with different templates)**
6. **🛒🟥(0%)Shopping Cart and Order Processing: (Pending Development**
7. **📝🟥(0%)Payment Manager: (Pending Development)**

# 👑Conecpt Of The Story
You have a business with a valid trade license and do business professionally. You can register to open a shop. To open a shop, you need an email address to register. You are the owner of the shop. You are the admin on the shop website.

You have staff in your business. After registering at the shop, you can add staff with an invite link. You need to provide the email address of the staff to work for your online shop. You are the administrator of the shop. You can invite staff to join. You can add an invitation to join as an admin or a manager.

The staff can accept or reject your invitation to join. If the staff accepts your invitation, they must have an account on the multi-tenant web application. Your invitation will create his account in the application. An email will be sent from the Web Application to verify his/her email address. Once he/she verifies the link in his/her email inbox, he/she can login with the email and password in your shop.  Based on the type of invitation, the staff can perform specific tasks of the shop.

# ☁️🐧Hosting Environment
**☁️VPS With 🐧Linux Environment**

**🌐➡️**     **Nginx Reverce Proxy🔒🛡️➡️**     **Router to Web App🖥️**

He/she will go to the link of the tenant URL; browser sends the request to the server (multi-tenant shopping host). Multi-tenant web application hosted on a Linux VPS: Nginx reverse proxy receives the request and acts as router for Multi-Tenant Web Application. This layer of security is a Sheild for the shopping host.

**Internet🌐➡️**     **Nginx Reverce Proxy🔒🛡️🔀**     **Load Balance🖥️🖥️🖥️**

Nginx convert https (encrypted) requests to http (decrypted) requests, Routing for (domain, sub domain) tenants to the host, Scale during high traffic times as load balancer, by shop visitors to multiple instances of VPS or different ports of the same VPS, limit the request per tenant to stop crashing the server by any abusive user or DDOS attack. The response from the shopping host is again encrypted and returned to the browser by Nginx.

# Solution Design (Monolithic and Clean Structure)

1. In that consideration, my data infrastructure (Model, Repository) is self-registered. This project has zero dependency over any Data Transfer Object or View Model.
2. Again, the service never communicates with the presentation layer with the Entity Models. They talk with Data Infrastructure in entity and business models.
3. While communicating with the Presentation layer (web project), they use business objects which have no connection or tracking with Data Infrastructure. This technique provides the application database more secure because in any mistake, code doesn’t have any chances to alter or change.
4. Since the Data Infrastructure is self-registered, we don’t need to keep any references for Data Infrastructure. We can even remove the Connection strings as well. We are using Code First Migration using Entity Framework Core. The migrator console app can take care of migrations and update the database.  (But currently, we are keeping the connetin string in web app project)
5. The repository works with the entities only in the Data Infrastructure. The Data Infrastructure has two projects (Main. Infrastructure and Main. Model).
6. They communicate with the Service layer (Project) happens using (Data Model & Entity Model) with the Data Infrastructure. Service sends Entity Model (for saving or update). The returned queries from the Data Infrastructure are re-created with new objects which have no connection with the database and entity model. It means communication initiates and closes inside the servers.
7. Browsers are not displayed with the model which the service project sends. They see and communicate with the controller and end points with the View Model. This is in the web project (presentation).
8. The service registrations, middleware is self-contained and reusable using dependency extensions. The parameters (connection string) are provided from the appsettigs.json from the web project.  

# 🚩Concerns For The 🛍️Tenant (Isolation)
**This SaaS Multi-Tenant Architecture: Each Tenant uses one application and one shared database & shared static resource files.**

Multi-Tenant applications often suffer from data leaks, which is an error because of the problem of isolation from one tenant to another. It is a challenge to address and solve, while designing the SaaS architecture. If the application architecture and cross cutting concerns, clean separation in code are implemented and configured properly; the tenant will not face data Leake, unauthorized access and security risks. We took the measures which are practiced in industry for such SaaS implementation. We addressed the isolation in the database level, implemented configuration for the authentication and authorization with Json Web Token (Jwt) which is secure and robust. Tenant and their data are safe in this multi-tenant Saas Web Application.

## ✅Isolation SaaS Architecture Solution
1. The shared database is partitioned using a global query filter in the database level using the resolved tenant id. Tenants use the database tables where only their records are present. Queries are done against the tenant records only. Other tenants' records are not available for the current tenant to query because of the global filter restrictions. This data isolation is done by tenant resolving middleware using a scoped tenant setting service.
2. The User has to prove that he/she is a user of the resolved tenant. Tenant security middleware validates the resolved tenant's id with the encrypted user's tenant claim as proof. Tenant information is isolated from users from other tenants.

**Check the Following Middlewares:**

## Middleware Order

<img width="759" height="235" alt="OrderOfMiddlewares" src="https://github.com/user-attachments/assets/8794a9b8-ecce-4357-944d-73a0ce902246" />

# Multi-Tenant Request Pipeline

<img width="641" height="536" alt="Request Plipeline" src="https://github.com/user-attachments/assets/4a4be819-efff-4c4b-8aa9-306febf8bd0d" />

## Global Safety Net

<img width="635" height="218" alt="SafetyNetExceptionResponcePipeline" src="https://github.com/user-attachments/assets/40b630d1-6363-48cf-b382-868fb149c70b" />


# Pipeline Starting Point: 

## TenantResolverHandlingMiddleware
**TenantResolverHandlingMiddleware:** 

  
In ASP.NET Core 8.0, a Multi-Tenant Request Pipeline isolates data and configuration per customer by resolving tenant context at the very beginning of an HTTP request.  
 
This architecture relies on this middleware to execute early in the pipeline to parse the incoming request, look up the tenant, and inject the context into a scoped service for downstream components. 
 
**Middleware Name: TenantResolverHandlingMiddleware** 
 
### Function: Tenant Identification, Faster Response and Tenant Data Isolation 
 
1. **Tenant Identification:** 
We first get the host and path from the HttpRequest object and extract the domain/subdomain or subdirectory. We check the extracted domain/subdomain or subdirectory in the database to identify if they are in the list of our tenants. If found, we consider the request to be valid and tenant is resolved. When the resolver gets a request from a logged user, it gets the resolved tenant id.
2. **Faster Response:** 
This middleware keeps resolved TenantId in memory cache (consider active memory in the server end from where the request entered) for a specific time (lifetime for 30 minutes). The tenant id is kept in memory to not hit the database for resolved tenants again. We set the cache for 30 minutes. We query the database again and reset the cache active for the next 30 minutes. This middleware does this for performance: faster response time for the request (reduce latency). 
 
2. **Tenant Data Isolation** 
ITenantSetter is a scoped (service) registered in the program.cs. Scoped means: for the entire request lifetime the service is active from the middleware to the end layer (ef core dbcontext). We set the resolved tenant id, in this service. We can access this scoped service using Dependency Injection (DI) from any (controllers, services, DbContext) components.  
 
The primary objective is to create a **Global Query Filter (isolated database partition for each tenant) ** from the shared database (for all tenants) using the resolved tenant id. Any data related to the tenant is isolated from other tenants because of this isolation. This data isolation using the scoped ITenantSetter is an architectural design for multi-tenant applications using .NET 8.0. Each request for a tenant can work and view only from the partitioned isolated database for the tenant.  
 
**Multi-Tenant SaaS: In this application, Database is shared. But it acts like a single database for each tenant.** 
 
**What is Global Query Filter?** 
1. It means that the tenant queries are done over the partitioned records (act and feel like a database for a tenant as his own database and not shared).  
2. Other tenants' data are not present when any records are fetched from the database by a tenant. Because globally it is filtered by Global Query Filter based on the tenant id (resolved).

## Routeing Middlewware
**To Understand the Route Middleware for Multi Tenant Architecture**
See the Web App Project Folder Structure:

<img width="503" height="349" alt="FolderStructureMonolithicWebProject" src="https://github.com/user-attachments/assets/0f1a9a08-cb26-440e-9ea0-f8354c971c35" />

**We are using monolithic architecture, the published output will be a single application instance. This is the default MVC Web application folder structure with separate Area controllers and standard controllers. In this folder structure, wwwroot folder contains the static resource files (styleing, images, js etc). This is a basic folder structure for MVC design pattern for Web App Project** 
 
 
**I am writing about the folder structure, to let me know about the default routing middleware. We are not doing any Tenant specific routings** 
 
 
### Tenants have: 
1. **Domain** 
2. **Sub Domain** 
3. **Sub Directory** 
 
 
1. **For (1 and 2), it is** always a default routing. If we provide tenant specific routes, it implies that we have special reasons and tenant specific directories. We are creating an architecture which serves multiple tenants with the same application (the multi-tenant SaaS).

2. For static resources, we can keep tenants specific: one style sheet [siteTennat1.css, siteTennat2.css, siteTennat3.css, siteTennat3.css, siteTennat3.css, siteTennat3.css ...] for each tenant for their own. This is a flexible and advanced method for styling the pages for tenants. But it requires special skills in CSS code writing to use the feature. 

**In our application, We are not usng static resources for each tenant. We are providing two features to let them structure the pages by providing different Templates (to structure the page) and few themes (colors).** 


## Tenant Concern: 
1. **We will provide Global Variables (colors) to create themes for tenants.** 
2. **Templates (1 col, 2 col, 3 col, 4 col), Banner (1 Image), Ad (Image with Link), Image Carousal.** 
3. **All templates will get the same color theme, set by the tenant.**
4. **Tenant can organize the rows as their wish (up, down, drag and drop) with the templates provided.**

## Technical Concern: 
1. **For the domain ad sub domain based tenants:** always get the default route bacause of the technology (asp.net core mvc). It is the defaut routeing middleware behavior in .Net 8.0 multi tennat SaaS.
2. **For the Sub Direcoty tenants:** we need to use extra measure to either rewire the the base path to perfrom the default behavior. Or implement the per tennat directory which we explained earlier. Technically, the tenant who use sub directory (www.tenantors.com/tt  khai, here ttkhai is the tenant), we are rewriteing the base path to tenantors.com/ inside the route pipeline, so the route donot consider the ttkhai as an end point. It will search for the end point but actualy, this is the name of the tenant. We are doing url rewrite touse the default route. As a reasult the  applcation is serving same dynamic and static and resources forsame one instance. From same same site.css forall tenants. We make the colors dynamic using the global variable to create themes.

# Multi Tenant Architecture 🔄(100%) 🟥(0%)

## SaaS Cross-Cutting Concerns 
In a .NET Software-as-a-Service (SaaS) architecture, cross-cutting concerns represent technical functionalities that span your entire system and must execute across various endpoints, layers, or microservices without altering the core business rules. In a multi-tenant SaaS application, these concerns become highly critical because they must almost always be evaluated in the context of a specific tenant.Core 

**SaaS Cross-Cutting Concerns:**
1. **Tenant Resolution:** Identifying the tenant from headers, subdomains, or access tokens on every incoming HTTP request.🔄(100%)
2. **Data Isolation:** Dynamically appending global query filters or swapping out connection strings based on the resolved tenant ID.🔄(100%)
3. **Authentication & Authorization:** Ensuring users are authenticated globally and verified for specific tenant-level permissions or subscription tiers.🔄(100%)
4. **Feature Management & Billing Flags:** Enabling or disabling code execution branches dynamically according to the tenant’s subscription tier.🟥(0%)
5. **Structured Logging & Tracing:** Injecting a TenantId attribute into every log context to isolate logs per customer across microservices.🔄(100%)
6. **Rate Limiting & Throttling:** Restricting request limits at the tenant level to prevent noisy neighbor scenarios.🟥(80%)
7. **Global Exception Handling:** Mapping all unexpected errors to standardized JSON problem details while hiding internal infrastructure quirks.🔄(100%)

## Structural Implementation Patterns in .NET:
To adhere to the Single Responsibility Principle and avoid mixing business logic with infrastructure noise, .NET applications use three distinct design patterns to implement these concerns:

1. **Middleware Pipeline** 
ASP.NET Core Middleware Pipeline: Middleware handles concerns at the outer HTTP level before a request ever reaches your API endpoints. Best used for: Tenant resolution, global rate limiting, global authentication, and top-level exception handling. The Rule of Ordering: The order of your middleware configuration in Program.cs dictates execution. Rate limiting should execute before authentication to conserve server resources, while authentication must run before authorization.🔄(100%) 

2. **MediatR/Sockets Pipeline Behaviors (Krestel Server behind Nginx Reverses Proxy** 
If your architecture uses Clean Architecture 🔄(100%) or Vertical Slices 🔄(100%) with the MediatR 🟥(Not Using) library on GitHub. The Nginx Reverse Proxy (two Sockets Pipeline for each Request Pipeline Behaviors 🔄(100%) ) operate as intra-application middleware 🔄(100%). 

3. **EF Core Global Query Filters & Interceptors** 
When relying on Entity Framework Core, your database access layer can natively manage data separation rules🔄(100%) . Best used for: Automated multi-tenant data filtering🔄(100%) , soft deletes, and automated database auditing🔄(100%)  (such as injecting CreatedByTenantId or timestamp fields)🔄(100%).


# Multi-Tenant Implementation 
Monolithic Application & Shared Database 


## Global Eception Handle (Middleware)

### **Key Features**
✅ **Global middleware** - Catches all unhandled exceptions without try-catch blocks 🔄(100%)  
✅ **Intelligent mapping** - 18 exception types → error codes + HTTP status codes 🔄(100%)  
✅ **Serilog logging** - 3 log streams (application, errors, JSON) with daily rotation in files. 🔄(100%)  
✅ **Database persistence** - Stores full context with multi-tenant isolation (application exceptions) 🔄(100%)  
✅ **Automatic deduplication Keeping** - Repeating exceptions increment counter (counter with extra records) (within 1 hour condition stopped) 🔄(100%)   
✅ **database indexes** - Optimized for fast queries and filtering 🔄(100%)  
✅ **Secure** - Excludes auth headers, cookies, API keys; generic user messages 🔄(100%)  
✅ **Developer Logging API** - export excel 📂 files 🔄(100%)  
✅ **Multi-tenant** - Automatic tenant scoping via query filters 🔄(100%)  
✅ **Separate Database for Log Exceptions** 🔄(100%)   

## Authentication 🔄(100%)

**Authentication involves validation, signing in, creating access and refreshing tokens using Jwt and cookies suffixed with resolved TenantId.** 

**Validation**🔄(100%)

- Does the Model State is valid? 

- Does the user have a verified email? 

- Does email exist among the global identity application   users? 

- is user locked. 

- Does user email belong to the tenant? 

**🔄(100%) If above validations are successful, then try the user using Identity Sign in Manager to match the password** 

Use the found user object and password to validate the user's credentials.  

The password is encrypted in the database. Identity own managed Sign in manager validates the password against the database encrypted password. If the result is successful, it confirms that the user provided the correct email and password. 

Create Short Lived Access Token (Jwt): Using: User Id, Tenant Id and No. Of Minutes to Expire with a Secret Key for encryption. 

Create Long Lived Refresh Token (string): using (Base64 encoding) with a random number (62 bytes long). 

🔄(100%) Create Access Token (Jwt) Cookie 1:  

Name: “.App.AccessToken. {resolved tenant Id}” with Value of the created Jwt Access Token with options: HttpOnly, Secure, Samesite, SameSiteMode.Strict, for 15 minutes and set the path (“/”) to access all pages. 

🔄(100%) Create Refresh Token Cookie 2:  

Name: “.App.RefreshToken. {resolved tenant Id}” with cookie options: HttpOnly, Secure, Samesite, SameSiteMode.Strict, (for 7 days), with path "/account/refresh-token" for accessing refresh end point. 

## Authorization:🔄(100%) 

If the user is a valid user in previous step successfully, find the user's role for this tenant.   

Create formatted tenant roles using: UserId, Tenant Id and Tenant Role.  

Create the List of Claims: NameIdentifier: UserId , ClaimTypes.Role: “User”, TenantId: TenantId, TenantRole: “{UserId}: {TenantId}: {TenantRole}”,  UserName: “”, Email: “” 

Add the IdentityClaim (with claims) to the authenticated User in the Response object. 

## Generate Refresh Tokens (Jwt & String)🔄(100%): 

**For Authenticated Users**

🔄(100%) Ajax, Fetch with unsafe requests will fail with error status 401 when the access token expires. This error status is captured by the global interceptor in the java script. The Form Submit with full page is verified by Jwt Event and if found that the token expired, it returns the same 401 status error code. The interceptor in each case gets the requested endpoint with the error status code. A refreshing token request may be processed immediately, If the interceptor code is not ready, it puts that error response in a queue.  

The new refresh token generation request is automatically called by the global interceptor of java script. Once the new token is attached to the browser cookie, the failed code is again called by the interceptor code.  

🔄(100%) The file (auth-interceptor.js) is referenced just after the (jquery.min.js) file, in the main layout page. In that file: any failed request from those (for submit, ajax, fetch) is intercepted, kept in queue, request a new token and upon getting the token, it calls the failed method again and keeps the user authenticated and continues the session. Behind the scenes, the above work is done by this global error interceptor for the expired authentication access token.  

🔄(100%) We added antiforgery middleware default configuration in program.cs. We left the token generation and retrieval over the default MVC middleware. For validation, we use action filters by overwriting the default anti-forgery validation. The token is generated by the middleware; we just paste the helper tag on each page where we use (post, delete, put) methods. MVC does token retrieval when the helper tag is wrapped in the form tag for the full page submit, For the ajax and fetch: we must add a line to fetch/ajax request to send the antiforgery token, back. Fetch/Ajax usually just needs to add an attribute either with credential true or default. One line code added; default behavior is continued. 

🔄(100%) We have made some changes in the process to do the task dynamically. We made the part of code global (ajax, fetch and form submit). Previously, we used to paste the tag helper in the body of all views. Now, we have placed that line in the body of the main layout page. Every view can access that. Now, the sending the token back part with request we created a java script file to work globally, and we never need to write any line of code for ajax or fetch anymore. That is the reason why we move the helper tag of MVC to the body of the layout page to be accessible by all pages. When we attach the code from the global script, we ignore the safe method requests. That code is in global-ajax.js and source is referenced just a line above the interceptor js file in the layout page; for the form to submit, we need to find the input field, read the value of the token, and add that manually in the form body. Mvc default middleware will create and set the token in every request response cycle. Our global java script will send the token back like the default settings. For that reason, the MVC default helper tag is moved to the main layout (global), and we are not required to write the tag helper for the views. 

🔄(100%) We come back to the failed requests to call again. We have a refresh token; we have the global java script code to attach the token for anti-forgery (ajax, fetch or for submit); we can call the failed request from the interceptor code. User gets the same authenticated feel even if it was an outdated token. The background code keeps it unto date. 

## Controller End Point:🔄(100%) 

Request for new tokens are made to the controller end point. It is configured in the cookie path option (during cookie creation: login, refreshing). 

End Point: In the web app standard controller: 

Controller: Refresh,  

Post (“/account/refresh-token”) 

Action: Refresh 

## How does the refresh work? 🔄(100%) 

We used tenant aware ITenantSetter to get the TenantId using DI in the controller.  

Create the cookie name using the TenantId (resolved tenant id from DI) in the suffix: “.App.RefreshToken. {resolved TenantId}”. 

Extract the token from the secure cookie from the Request.Cookies object using the created cookie name. We now have the token of the cookie. 

Using the _tokenService.RotateRefreshTokenAsync method, we create a new Access Token (Jwt) and Refresh Token (string). 

We save the newly created tokens as a new record and revoke the previous Access Token with an update command. We keep a reference of the newly created refresh token string, in the revoked token record in a property for creating a chain reference.  

We append the short-lived Access Token JWT (Expires in 15 minutes) cookie and the long-lived Refresh Token String (Expires in 7 days) in the Response object with same options and name convention. 

We return the fresh access JWT in the JSON payload with OK status.  

# Security (Identity Default Flow) User (login) 🔄(100%)

### ✅Broken Access Control & Enumeration
- Attackers input various email addresses into a forgotten password form to see which ones return a "User not found" error.
- This maps out registered user bases for targeted phishing.

### Mitigation 
- Anti-Enumeration Logic, the controller uses an identity-blind diversion step.
- We will check if the user exists and if the email is verified or not. If not exists and verified, the enumerating code from hacker or threat will be redirected, but we do not reveal if the user exists. This means that we will check the link with the existence and verified requirement of the link.
- The threat doesn't know if the user or email already exists. They are running code against the login. We will rather provide a confirmation that check your inbox for the link to set up your password. This is how we are mitigating the Anti-Enumeration Logic. 

### ✅Cryptographic Failures & Session Hijacking🔄(100%) 
- Predictable reset tokens (like simple base64 hashes or sequential numbers) can be guessed by automated scripts, allowing malicious password overrides.
- Cryptographic Token Lifecycles & Security Stamp Invalidation The workflow calls ASP.NET Core's internal GeneratePasswordResetTokenAsync(user). This function is from the Identity User Manager. This generates a time-bound, cryptographically random string during sending an email link.
- Once ResetPasswordAsync (Identity owned method) completes successfully, ASP.NET Core automatically refreshes the user's Security Stamp in the database. This instantly invalidates the token timestamp. The mail link is no longer usable.

### ✅Injection and Cross-Site Request Forgery🔄(100%) (Default with tenant specific cookie and custom validation) 
- Attackers spoof forms using unauthorized cross-domain scripts or target database flaws via inputs. The [ValidateAntiForgeryToken] attribute added to both POST endpoints (action) in controller.
- They work side-by-side with @Html.AntiForgeryToken() implicitly built into the views.
- Entity Framework Core acts as the data layer (ApplicationDbContext which is Identity configured). By utilizing parameterized LINQ parameters under the hood FindByEmailAsync(email) before signing in a user reduced the risk of password injection.

### ✅Identification and Authentication🔄(100%)
- Failure Weak reset pathways easily bypass initial account defenses, nullifying complex user passwords.
- The system explicitly requires email verification before allowing a password to reset flow (IsEmailConfirmedAsync) and login. 

# Solution Architecture (.NET 8.0):🔄(100%) 
Current Archiecture is:
1. ✅Monolithich:
- One deployable unit, can be scaled horizontally and vertically). Above has details about scaleing for this application.
2. ✅Clean Architecture:
  
- 🔄(100%) Makes the code organnized and maintainable.
- 🔄(100%) The primary objective was to make the code modular, reusable, separation of the concerns, and readable while doing the code for the solution.
- 🔄(100%) Another objective was to make sure it is Linux deployable and keeping the services completely separated from the presentation code (Web Project). Now, code is separated and using services but the API project is not there yet.
- 🔄(100%) My plan to separate the services from the presentation is to make the web project light weight and reuse the same in different cross platform non-computer devices (Mobile, Tab).
- 🔄(100%) Microsoft already has their own technology for app development (Xamarin) which uses the API (Web API) project hosted on any server. My plan was to keep the code common for everyone (web, mobile, & tablet). 

# 🔄(100%) The Best Practices by Research 
**Note:** 
- Before planning for the multitenant application saas, I didn't consider or research the scaling part. Still it is applicable with curret design. (Vertical ad Horizontal Scale)
- I started to create the architecture of the new solution, keeping in mind the best practices of design and architecture. 

**This objective created the Clean Architecture:**

# How to scale this application? (Google AI)
Even though you have separated the code into different projects (Web, Service, Domain, Infrastructure), they are compiled together and run as a single process on your Linux VPS. Here is exactly how to scale your monolithic architecture, how introducing a separate API project changes the strategy, and how routing works across multiple instances. Following are the strategies for scaling your current monolithic architecture and the potential benefits of introducing a separate API project:

## Strategy 1: Scaling Your Current Monolithic Architecture
Because your entire application runs as a single process, you cannot scale just the "Shared Service project" on its own. You must scale the entire monolith together.

- **Vertical Scaling First:**
Upgrade your Linux VPS to a higher tier with more CPU cores and RAM. This is the fastest, zero-code way to handle more traffic.
- **Database Connection Pooling:**
Since you use a shared database, multiple application threads will compete for database connections. You must optimize your connection pool size in your appsettings.json connection string so the database doesn't choke.
- **In-Memory Caching:**
Use .AddMemoryCache() in .NET 8.0 to store frequently requested, non-tenant-specific data directly in the VPS RAM to avoid hitting the shared database.

**Strategy 2: Scaling by Introducing an API Project**

If you split your architecture so that the Web Project (frontend/UI) communicates with a separate API Project (backend logic, services, and database access), you gain better scaling flexibility.

**Independent Scaling:**

You can deploy the Web project and the API project onto two completely separate Linux VPS instances.

**Targeted Resource Allocation:**

If your traffic involves heavy data processing, your API VPS will need high CPU/RAM. If your traffic is just users loading static web pages, your Web VPS can remain small and cheap.

**API Worker Pools:**

If the API becomes the bottleneck, you can duplicate only the API project across 3 or 4 separate Linux servers, while keeping only 1 server for the Web frontend.

## How one Instance Becomes Multiple Instances:

To turn one instance into multiple instances on a Linux VPS infrastructure, you use a process called Horizontal Scaling (Scaling Out).

You provision 2 or more separate Linux VPS instances (e.g., VPS 1, VPS 2, VPS 3).

**Deploy the Code:**

You deploy the exact same compiled .NET 8.0 published folder to all of those VPS instances.

**Keep them Stateless:**

For this to work, the application must not save files (like uploaded user images) or user sessions locally on the VPS disk. Sessions should be stored in a shared distributed cache (like Redis), and files should go to an object storage service (like AWS S3 or MinIO).

## How Traffic Routes to These Instances:

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

## Background: 
I started my code to build and run for a client (small shop). It was previously made for an online marketplace. That was in .NET Framework 4.6 where you must deploy the portal in a windows or cloud based (Microsoft Azure) in Platform Service as a web application. Deployment infrastructure/platform service is a windows server based. 

When I started looking at the code and searching on the internet, I found that Microsoft doesn’t have any support over the framework because of security vulnerability. 

### I decided to do a migration of my code in .NET 8.0 because it has long term support plan and it is portable both in Windows and Linux servers. Also, the technology supports cross platforms including mobile devices and tablets.


## Future Work: 
1. The web project communicates with the service project with Business Model objects. This is how I tried to keep the web project separate and make the service project reusable for other cross-platform projects using Web API.
2. Because of the saperatin and breaking the code modular, we can convert the solution into micro service based deployment and scale the heavy traffic api services. 

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

 

 Building a Multi-Tenant SaaS application in .NET 8.0 requires a clean architecture to handle tenant isolation, resolution, and database routing seamlessly. The most cost-effective and highly maintainable architecture for a startup or mid-sized storefront SaaS is the Shared Database with Column-Based Isolation (Global Query Filters) pattern, though it can easily scale to a database-per-tenant architecture. [1, 2, 3] 
Below is a complete, production-ready implementation guide utilizing .NET 8.0 Minimal APIs, Entity Framework Core (EF Core), and custom Middleware. [2, 4] 
------------------------------
## Step 1: The Tenant Context Model
First, create an interface and model to represent who the current tenant is throughout the request lifecycle. [2, 5] 

// Core/ITenantOwned.cspublic interface ITenantOwned
{
    public string TenantId { get; set; }
}
// Infrastructure/TenantContext.cspublic class TenantContext
{
    public string? TenantId { get; set; }
    public string? Name { get; set; }
}

## Step 2: Tenant Resolution Middleware
To intercept incoming HTTP requests and identify the tenant (via HTTP custom headers, query strings, or subdomains), write a custom middleware. [2, 5] 

// Middleware/TenantResolverMiddleware.csusing Microsoft.AspNetCore.Http;using System.Threading.Tasks;
public class TenantResolverMiddleware
{
    private readonly RequestDelegate _next;

    public TenantResolverMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, TenantContext tenantContext)
    {
        // 1. Resolve from custom HTTP Header (e.g., 'X-Tenant-Id')
        if (context.Request.Headers.TryGetValue("X-Tenant-Id", out var tenantId))
        {
            tenantContext.TenantId = tenantId.ToString();
        }
        // 2. Fallback: Query string (e.g., ?tenant=store1)
        else if (context.Request.Query.TryGetValue("tenant", out var tenantQuery))
        {
            tenantContext.TenantId = tenantQuery.ToString();
        }
        
        // If your business rule requires a tenant, reject requests without one
        if (string.IsNullOrEmpty(tenantContext.TenantId) && !context.Request.Path.StartsWithSegments("/api/tenants"))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Tenant identifier is missing.");
            return;
        }

        await _next(context);
    }
}

## Step 3: Entity Framework Core Setup with Global Query Filters
The core of data isolation is leveraging EF Core Global Query Filters. This guarantees that developers do not accidentally query another store's data. We also override SaveChangesAsync to automatically populate the TenantId column upon creation. [1, 6, 7] 

// Data/StoreDbContext.csusing Microsoft.EntityFrameworkCore;
public class Products : ITenantOwned
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string TenantId { get; set; } = string.Empty; // Enforces isolation
}
public class StoreDbContext : DbContext
{
    private readonly TenantContext _tenantContext;

    public StoreDbContext(DbContextOptions<StoreDbContext> options, TenantContext tenantContext)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    public DbSet<Products> Products => Set<Products>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply Global Query Filter to all entities implementing ITenantOwned
        modelBuilder.Entity<Products>().HasQueryFilter(p => p.TenantId == _tenantContext.TenantId);

        // Add index on TenantId for high-performance cross-tenant filtering
        modelBuilder.Entity<Products>().HasIndex(p => p.TenantId);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Automatically inject TenantId to newly created items
        foreach (var entry in ChangeTracker.Entries<ITenantOwned>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.TenantId = _tenantContext.TenantId 
                    ?? throw new InvalidOperationException("Tenant ID context cannot be null during creation.");
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}

## Step 4: Register Services in Program.cs
In .NET 8.0, dependencies are structured in Program.cs using the Minimal API paradigm. The TenantContext must be registered as Scoped so it is unique to individual HTTP requests. [2, 4] 

// Program.csusing Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
// Register TenantContext as Scoped (per-request)
builder.Services.AddScoped<TenantContext>();
// Register SQL Server / PostgreSQL DbContext
builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Inject Tenant Resolution Middleware early in the HTTP Pipeline
app.UseMiddleware<TenantResolverMiddleware>();
// --- MINIMAL API ENDPOINTS ---
// Fetch products (Automatically filtered by the Middleware + EF Filter)
app.MapGet("/api/products", async (StoreDbContext db) =>
{
    return await db.Products.ToListAsync();
});
// Create product (TenantId will automatically inject via SaveChangesAsync override)
app.MapPost("/api/products", async (Products product, StoreDbContext db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/api/products/{product.Id}", product);
});

app.Run();

------------------------------
## Alternative: Dynamic Database-per-Tenant Pattern
If you need strict isolation (e.g., compliance mandates that each store has its own physical database), you can modify the StoreDbContext runtime instantiation by resolving connection strings dynamically based on the current tenant metadata: [3, 8] 

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    if (!optionsBuilder.IsConfigured)
    {
        // Lookup tenant connection string dynamically from an admin DB or configurations
        string tenantConnectionString = GetTenantConnectionString(_tenantContext.TenantId); 
        optionsBuilder.UseSqlServer(tenantConnectionString);
    }
}

## Key Considerations for SaaS Architectures

   1. Performance Indexing: Always ensure the TenantId has a composite or standalone database index. Because it is present in every generated WHERE clause, your multi-tenant app will experience severe query degradation without it. [1, 9] 
   2. Cross-Tenant Admin Operations: If an internal global admin needs to run cross-tenant reporting, use .IgnoreQueryFilters() in EF Core queries to lift the automatic restriction. [6] 
   3. Robust Onboarding Automation: Implement automated integration pipelines (such as Azure Resource Manager / SDKs) to seamlessly spin up compute and run background database migrations whenever a new user registers a store. [3, 10] 

If you'd like to dive deeper into certain components, let me know:

* 
* Do you prefer a Shared Database Schema or Database-per-Tenant architecture?
* Do you need assistance mapping Custom Domains/Subdomains (like ://mysaas.com)?
* Would you like an implementation framework for integrating Multi-Tenant Identity & Authentication (e.g., custom user management or Microsoft Entra ID)? [2, 3, 11, 12, 13] 
* 


[1] [https://www.linkedin.com](https://www.linkedin.com/pulse/how-build-multi-tenant-saas-application-aspnet-core-hagxc)
[2] [https://oneuptime.com](https://oneuptime.com/blog/post/2026-01-26-multi-tenant-apps-dotnet/view)
[3] [https://www.youtube.com](https://www.youtube.com/watch?v=Q-CI5SeCaT8&t=755)
[4] [https://www.sarikayadev.com](https://www.sarikayadev.com/en-US/blog/designing-multitenant-saas-with-net-8-minimal-apis-ef-core-sharding-azure-ad-b2c-and-postgresql-flexible-server)
[5] [https://www.alertu.io](https://www.alertu.io/implementing-multi-tenancy-in-dotnet/)
[6] [https://www.youtube.com](https://www.youtube.com/watch?v=3uWeyEbV4c4&t=6)
[7] [https://medium.com](https://medium.com/@convergesol/building-secure-and-scalable-multi-tenant-saas-with-net-and-angular-0ebb07eb099e)
[8] [https://www.youtube.com](https://www.youtube.com/watch?v=Gf1sCvikpgI)
[9] [https://www.youtube.com](https://www.youtube.com/watch?v=p067nKdRJWg)
[10] [https://medium.com](https://medium.com/ascentic-technology/multi-tenant-saas-application-design-patterns-cost-effective-deployment-options-in-azure-and-e99b23d3156f)
[11] [https://dev.to](https://dev.to/cristiansifuentes/microsoft-entra-id-multi-tenant-saas-net-8-web-api-a-production-grade-playbook-3-tenants-3-53f5)
[12] [https://www.commercepundit.com](https://www.commercepundit.com/blog/developing-multi-tenant-applications-in-dotnet-a-guide/)
[13] [https://balramchavan.medium.com](https://balramchavan.medium.com/how-to-build-a-scalable-multi-tenant-saas-platform-using-angular-c-net-8fafd1fd01f6)
