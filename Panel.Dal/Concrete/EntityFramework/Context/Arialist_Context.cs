using Panel.Entities.DataModels;
using Panel.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Panel.Dal.Concrete.EntityFramework.Context
{
    public partial class PanelContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=HALISPC;Initial Catalog=Arialist_DB;Integrated Security=true;Trust Server Certificate=true;");
            // optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Panel_DB;Integrated Security=false;User ID=sa;Password=Sony46464646aa..;Connection Timeout=30;Trust Server Certificate=true;");

        }







        public virtual DbSet<SupportRecord>? SupportRecord { get; set; }

        public virtual DbSet<User>? User { get; set; }





        public virtual DbSet<Role>? Role { get; set; }

        public virtual DbSet<il>? il { get; set; }
        public virtual DbSet<ilce>? ilce { get; set; }

        public virtual DbSet<Ulke>? Ulke { get; set; }
        public virtual DbSet<Tanim>? Tanim { get; set; }
        public virtual DbSet<TanimGrup>? TanimGrup { get; set; }

        public virtual DbSet<UserView>? UserView { get; set; }





        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //extracted m:n mapping for demonstration puporses

            //one user two min








            //builder.Entity<UserMinProfile>(b =>
            //{
            //    b.ToTable("Users");

            //});



        }









    }




}

