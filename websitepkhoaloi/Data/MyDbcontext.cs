using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using websitepkhoaloi.Models.Enitity;


namespace websitepkhoaloi.Data
{
    public class MyDbcontext : IdentityDbContext<ApplicationUser>
    {
        public MyDbcontext(DbContextOptions<MyDbcontext> options) : base(options)
        {
        }
        #region
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Titlemenu> Titlemenus { get; set; }
        #endregion

            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);
    
                // table name
                builder.Entity<Titlemenu>().ToTable("TitleMenus");
                builder.Entity<Menu>().ToTable("Menus");
                // ===== menu cha =====
                builder.Entity<Titlemenu>(entity =>
                {
                    entity.HasKey(x => x.Id);
                    entity.Property(x => x.Id).ValueGeneratedOnAdd();
                    entity.Property(x => x.title).IsRequired().HasMaxLength(200);
                    entity.Property(x => x.thumnail).HasMaxLength(500);
                    entity.Property(x => x.DateCreated).HasDefaultValueSql("GETDATE()");
                });

                // ===== menu con =====
                builder.Entity<Menu>(entity =>
                {
                    entity.HasKey(x => x.Id);
                    entity.Property(x => x.Id).ValueGeneratedOnAdd();
                    entity.Property(x => x.thumnail).HasMaxLength(300);
                    entity.Property(x => x.url).HasMaxLength(500);
                    entity.Property(x => x.DateCreated).HasDefaultValueSql("GETDATE()");
                    entity.HasOne(x => x.titlemenu).WithMany(x => x.menus).HasForeignKey(x => x.TitlemenuId).OnDelete(DeleteBehavior.Cascade);
                });
            }
    }
}
