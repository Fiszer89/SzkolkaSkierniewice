using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SzkolkaSkierniewice.Domain.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SzkolkaSkierniewice.Domain.Concrete
{
    public class EFDbContext:DbContext
    {
        /*public EFDbContext(): base("EFDbContext")
        {
            
        }*/

        public DbSet<Product> Products { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<ProductInPromotion> ProductsInPromotion { get; set; }
        public DbSet<Box> Boxes { get; set; }
        public DbSet<DrzewoAlejowe> DrzewaAlejowe { get; set; }
        public DbSet<IglakGrunt> IglakiGrunt { get; set; }
        public DbSet<IglakPojemnik> IglakiPojemnik { get; set; }
        public DbSet<KrzewLisciasty> KrzewyLisciaste { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<IglakPojemnik>()
                .HasMany(c => c.Boxes).WithMany(i => i.IglakPojemnik)
                .Map(t => t.MapLeftKey("ProductID").MapRightKey("BoxID").ToTable("BoxesForIglak"));

            modelBuilder.Entity<KrzewLisciasty>()
               .HasMany(c => c.Boxes).WithMany(i => i.KrzewLisciastyPojemnik)
               .Map(t => t.MapLeftKey("ProductID").MapRightKey("BoxID").ToTable("BoxesForKrzewLisciasty"));                
        }
    }
}