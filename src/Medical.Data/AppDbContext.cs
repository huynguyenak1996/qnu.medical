using Medical.Entities.System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medical.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
         IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, ApplicationRoleClaim, IdentityUserToken<string>>
    {
        public AppDbContext()
        {
        }
       

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
     
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);       
        }

        #region "#============ Add Customer DB Here =======================# "
        public DbSet<vnc_Modules> VNC_Modules { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationUserPhoto> ApplicationUserPhotos { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRole { get; set; }
        public DbSet<ApplicationRoleClaim> ApplicationRoleClaim { get; set; }
        public DbSet<ApplicationClaim> ApplicationClaim { get; set; }
        public DbSet<Entities.Model.UserProfile> UserProfiles { get; set; }
        public DbSet<VNC_DoiTuong> VNC_DoiTuong { get; set; }
        public DbSet<VNC_ThanhVien> VNC_ThanhVien { get; set; }
        public DbSet<VNC_PhacDoDoiTuong> VNC_PhacDoDoiTuong { get; set; }
        #endregion
    }
}
