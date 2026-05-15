using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class ApplicationDbContext ( DbContextOptions options )
    : IdentityDbContext ( options )  
{

}
