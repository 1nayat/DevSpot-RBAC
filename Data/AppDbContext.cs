using DevSpot.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace DevSpot.Data
{
    public class AppDbContext:IdentityDbContext
        //identity means any kind of user will be added means user will be able to create account roles will be there
        //because using identity dbcontext and then adding mig without creating any model tables will be created in the database automatically 
    {
        public DbSet<JobPosting>JobPostings { get; set; }    
        public AppDbContext(DbContextOptions<AppDbContext>options )
            : base(options) 
        {
            
        }
    }
    // we have to remember scafoldding is imp as if we want to modify register login
    // passwrod reset page or any other page 

    //for sacfloding go to project rt click new scafold item pick identity and toggle on
    //those pages which we want to modify manually
}
