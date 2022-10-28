using Entities.UserEntities.SiteUser;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UserData
{
    public class SiteUserContext : IdentityDbContext<SiteUser>
    {
        public SiteUserContext(DbContextOptions<SiteUserContext> options) : base(options)
        {

        }
    }
}
