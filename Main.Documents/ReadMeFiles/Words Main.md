🚪 Select Your Authentication Method:
 ├── 🔑 Standard Account (Username + Password)
 ├── 🪪 Corporate Login (Single Sign-On / SAML)
 └── 📲 Passwordless Access (Email Magic Link)


🌐 www.tenantors.com  (Main Web Portal)
 ├── 📡 www.kaiassociates.com   (Tenant Alpha Endpoint: Domain)
 ├── 📡 www.kaiassociates.tenantors.com   (Tenant Beta Endpoint: SubDomain)
 └── 🔗 www.tenantors.com/kaiassociates  (Path-based Tenant Fallback)


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

1. After successful Login, the tenant admin can add a domain for his own store. Once the domain is configured, the user can login from there own domain.

🆔🪪www.kaiassociates.com

2. After successful Login, the tenant admin can configure the subdomain in the website. They will get a sub domain for his store. the user will continue login from the same website or with the subdomain website.

📲🪪www.kaiassociates.tenantors.com

3. After successful Login, the tenant admin can configure the domain or subdomain in the website. They will get a domain/sub domain for his store. Otherwise the user from default subdirectory workspace continue login from the same website.

🏢🪪www.tenantors.com/kaiassociates

### Summary:

### 🛍️ Tenant View:
In the above three cases: it is the way how the tenant (store) users will login, use their own work space and how customers will find their online store. The tenants may boost their online presence using media, social media or use web url in a name card.

### 🛠️Technical View:
It will confirm the isolation of the store identity and their  independent work space for each tenant inside the multi tenant web application.

## 🛍️Tenant Work Space

### Tenant Shop Website (URL):

**Tenant has it's own work space. Users can upload products, add ads (images, links) and arrange the pages by their choice.**


**Think the workspace as your shop in a shopping mall:**


**You plan in similar concept: the way the owner and managers plan, design interior and arrange products of a shop to display!**

### 🏢 Store Features (Tenant):

1. **🏢Tenant Profile: (domain setup / buy domain, invite users) 🔄(50%)**   2. **🔒 Security & Isolation: Manage user account and change password 🔄(100%)**                    
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

## 🚀 Architecture

**Monolithic Architecture:** Monolithic architecture is a traditional software design where all components of an application—such as the user interface, business logic, and database access—are combined into a single, unified, and self-contained codebase. It runs and deploys as one single unit. We chose monolithic architecture for this project because it simplifies development, testing, and deployment. It allows us to focus on building a cohesive application without the overhead of managing multiple services or microservices. This approach is particularly suitable for small to medium-sized applications where the complexity of a distributed system is not justified.

**Tenant Isolation:** We provided effort in the scaling process by providing optimized database queries by query filters and implementing efficient caching strategies and tenant isolation strategies using .Net 8.0 middleware pipeline. We are provideing Tenant resolution for domain, subdomains and subdirectory based tenants and Policy based Tenant Roles handelig and validating against the request and server memory cache. In the process of Tenant Role assignment, we keet isolatin in our concern. We keep the tenant, country, region, host type, user and tenant roles inconsideration in each tables in database. TenantId is the primary filter. But we validate together with the tenant's other attributes in combinatin and logging and carefully doing cacheing the request pipeline, tryig to keep the tenant isolatin as the number one priority of the application design and architecture design structure.

**Scale the Monolithic Application:** To scale a monolithic architecture for a multi-tenant store using a shared database, must optimize, how the single application handle resources and how the database separates tenant data. Scaling a monolith involves two distinct paths: vertical scaling (making the single server stronger) and horizontal scaling (cloning the entire monolith across multiple servers). For a multi-tenant store, horizontal scaling is often more effective, as it allows for better load distribution and fault tolerance. However, it requires careful management of shared resources (distributed cache redis) and tenant data isolation.

## 🚀Multi-Tenant application .NET 8.0
### Monolithic Architecture: Clean Architecture - One Application - Shared Database

Multi-tenant applications in .NET 8.0 are highly scalable because they pool infrastructure, allowing a single deployed instance or web cluster to serve thousands of customers (tenants) efficiently.Instead of scaling the entire web project, developers scale specific high-traffic services horizontally, such as background jobs, database queries, and microservices.

## How to scale this application:

Even though you have separated the code into different projects (Web, Service, Domain, Infrastructure), they are compiled together and run as a single process on your Linux VPS.

Here is exactly how to scale your monolithic architecture, how introducing a separate API project changes the strategy, and how routing works across multiple instances.

Following are the strategies for scaling your current monolithic architecture and the potential benefits of introducing a separate API project:

### Strategy 1: Scaling Your Current monolithic Architecture

Because your entire application runs as a single process, you cannot scale just the "Shared Service project" on its own. You must scale the entire monolith together.

Vertical Scaling First:

Upgrade your Linux VPS to a higher tier with more CPU cores and RAM. This is the fastest, zero-code way to handle more traffic.

Database Connection Pooling:

Since you use a shared database, multiple application threads will compete for database connections. You must optimize your connection pool size in your appsettings.json connection string so the database doesn't choke.

In-Memory Caching:

Use .AddMemoryCache() in .NET 8.0 to store frequently requested, non-tenant-specific data directly in the VPS RAM to avoid hitting the shared database.

Strategy 2: Scaling by Introducing an API Project

If you split your architecture so that the Web Project (frontend/UI) communicates with a separate API Project (backend logic, services, and database access), you gain better scaling flexibility.

Independent Scaling:

You can deploy the Web project and the API project onto two completely separate Linux VPS instances.

Targeted Resource Allocation:

If your traffic involves heavy data processing, your API VPS will need high CPU/RAM. If your traffic is just users loading static web pages, your Web VPS can remain small and cheap.

API Worker Pools:

If the API becomes the bottleneck, you can duplicate only the API project across 3 or 4 separate Linux servers, while keeping only 1 server for the Web frontend.

How one Instance Becomes Multiple Instances:

To turn one instance into multiple instances on a Linux VPS infrastructure, you use a process called Horizontal Scaling (Scaling Out).

You provision 2 or more separate Linux VPS instances (e.g., VPS 1, VPS 2, VPS 3).

Deploy the Code:

You deploy the exact same compiled .NET 8.0 published folder to all of those VPS instances.

Keep them Stateless:

For this to work, the application must not save files (like uploaded user images) or user sessions locally on the VPS disk. Sessions should be stored in a shared distributed cache (like Redis), and files should go to an object storage service (like AWS S3 or MinIO).

How Traffic Routes to These Instances:

To split traffic among your multiple VPS instances, you must place a Reverse Proxy / Load Balancer in front of them. On Linux, Nginx or HAProxy are the industry standards for this.

### Step 1: [ Incoming User Traffic ]
When a user goes to your domain name (e.g., www.example.com). The DNS resolves this to the public IP of your Nginx server:

### Step 2: [ Nginx Load Balancer ]
(Public IP: 192.168.1.1) :
Seneds the traffic to your multiple VPS instances based on the load balancing algorithm you choose:

### Step 3:
1. [ VPS Instance 1 ] :(Port: 5000)
2. [ VPS Instance 2 ] :(Port: 5000)
3. [ VPS Instance 3 ] :(Port: 5000)

The Entry Point:

Your domain name points directly to the single IP address of the Nginx server.

The Routing Rules:
You configure Nginx with the private IP addresses of your .NET 8.0 VPS instances.

Load Balancing Algorithms:

Nginx passes the traffic using specific rules:
1. Round Robin:
Sends request 1 to VPS 1, request 2 to VPS 2, request 3 to VPS 3, and repeats.

2. Least Connections:
Sends the next user request to whichever VPS is currently handling the least amount of active traffic.

3. Ip Hash (Sticky Sessions):
Ensures a specific tenant or user always hits the exact same VPS instance, which is helpful if you cannot use a shared distributed cache yet.

## Knowledge Cultivation

### Project goals

### Volunteering for Code Reuse
Volunteering your time is one of the best examples of giving back to the community. While this can be one of the more time-consuming ways to give back, it can also be the most fulfilling and hands-on.

“Knowledge is meant to be shared. I am dedicated to empowering the next generation of engineers. Goal is to create a community with same motivation.”

**Deliver code & build community**

This community is tring to deliver and engage engineers to practice collaborative production-grade software engineering. Our goal is to collectively design, build, and document a complete Multi-Tenant SaaS Storefront using .NET 8.0 ASP.NET Core MVC. Our focus is to encourage, engage new engineers and learners to join, learn and contribute in this community & project.

**Learn & Create Code**

**🚑 Real-World Problem Solving**
We solve production challenges like tenant isolation pipelines, global database filtering with EF Core, and secure client-side scripting.

**📝 Interactive Course Materials**
Every major architectural component is accompanied by step-by-step documentation, real-life examples, and actionable lab challenges.

**✨Co-Authoring Code**
Community members collaborate directly through GitHub Pull Requests, code reviews, and automated CI/CD pipeline building.

**👍 DevOps & Delivery**
We hopefully will finish by publishing our codebases to live Linux VPS servers, mastering the entire software development lifecycle (SDLC).

**Hi there, I'm Naim Ul Islam Prodhan 👋**
                   
I am an engineer, father, and community advocate from Bangladesh. My journey has taken me from local corporate offices to travels abroad, and now into the world of open-source and self-taught engineering.

**📖 Perseverance**
In January 2024, I survived a mild brain stroke that temporarily took away my ability to speak. Facing a long recovery and time away from full-time work, I chose to redirect my energy toward continuous learning and technology. I rebuilt my skills step-by-step—using my phone and tools like Google Colab to learn new programming languages and frameworks.

**Today, I am healthy, speaking again, and driven by a new purpose: to share my knowledge and give back to the tech community.**

**“Knowledge is meant to be shared. I am dedicated to empowering the next generation of engineers. Goal is to create a community with same motivation.”**



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

**🎬📺 SaaS Business Model:** In SaaS business model users/tenants instead of (buying and installing software) locally, access tools (applicatin features) via web browsers or apps, eliminating upfront infrastructure and maintenance costs.

**🔑🏢 Multi-Tenant SaaS:** This is a specific underlying technical architecture where all customers share the exact same software instance and physical infrastructure for receiving the application features. 

**🏬🛍️🛒Multi-Tenant Web Application:** is one example of the underling multi-Tenant SaaS. In this Model, Tenant is a store/shop.



