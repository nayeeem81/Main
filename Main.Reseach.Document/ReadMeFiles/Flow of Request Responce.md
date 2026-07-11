## Web App Project Folder Structure

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

I am writting about the folder structure, to let know that the route is default. We are not doing any Tenant specific routeing. 

### Tenants have:
1. Domain
2. Sub Domain
3. Sub Directory

For (1 and 2), it is always a default routeing. If we provide tenant specific route; it implies that we have special reasons and tenant specific directories. We are creating the architecture which serves multiple tenants with same application (the multi-tenant SaaS).

For static resources which we can keep tenants specific: one style sheet [siteTennat1.css, siteTennat2.css, siteTennat3.css ... ] for each tenant for their own. This is a flexible and advaced method for styleing the pages for tenants. But it requres special skills in css code writeing to use the feature. 

In our application, We are not usng static resources for each tenant. We are providing two features to let them structure the pages by providing different Templates (to structure the page) and few themes (colors). 

## Tenant Concern: (What we are doing is)

**1. We will provide Vlobal Variables (colors) to create themes for tenants.** 
**2. Templates (1 col, 2 col, 3 col, 4 col), Banner (1 Image), Ad (Image with Link), Image Carousal.** 
**3. All templates will get the same color theme, set by the tenant.**
**4. Tenant can organize the rows as their wish (up, down, drag and drop) with the templates provided.**

## Technical Concern: (What we are doing is)

1. For the domain ad sub domain based tenants always get the default route bacause of the technology (asp.net core mvc). It is the defaut routeing middleware behavior in .Net 8.0 multi tennat SaaS.
2. For the Sub Direcoty tenants: we need to use extra measure to either rewire the the base path to perfrom the default behavior. Or implement the per tennat directory which we explained earlier. Technically, the tenant who use sub directory (www.tenantors.com/ttkhai, here ttkhai is the tenant), we are rewriteing the base path to tenantors.com/ inside the route pipeline, so the route donot consider the ttkhai as an end point. It will search for the end point but actualy, this is the name of the tenant. We are doing url rewrite touse the default route. As a reasult the  applcation is serving same dynamic and static and resources forsame one instance. From same same site.css forall tenants. We make the colors dynamic using the global variable to create themes. 
