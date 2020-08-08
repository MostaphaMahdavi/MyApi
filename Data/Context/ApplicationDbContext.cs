using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using Entities.Bases;
using Entities.Categories;
using Entities.Posts;
using Entities.Roles;
using Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }



        #region OnModelCreating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            var entityAssembly = typeof(IEntity).Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

           // modelBuilder.RegisterAllEntities<IEntity>(entityAssembly);

            //   modelBuilder.RegisterEntityTypeConfiguration(entityAssembly);
            // OR


            modelBuilder.AddRestrictDeleteBehaviorConvention();

           // modelBuilder.AddSequentialGuidForIdConvention();

            //تغییر default value در sql server
             //   modelBuilder.Entity<Post>().Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

           // modelBuilder.AddPluralizingTableNameConvention();

        }

        #endregion



        #region Save

        #region SaveChanges
        public override int SaveChanges()
        {
            _cleanString();
            CustomSaveChange();

            return base.SaveChanges();
        }

        #endregion

        #region SaveChanges

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _cleanString();
            CustomSaveChange();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        #endregion

        #region SaveChangesAsync
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            _cleanString();
            CustomSaveChange();


            return base.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region SaveChangesAsync

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            _cleanString();
            CustomSaveChange();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        #endregion


        #region CustomSaveChangeMethod

        private void CustomSaveChange()
        {

            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                                e.State == EntityState.Added
                                || e.State == EntityState.Modified));



            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdateDate = DateTime.Now;
                ((BaseEntity)entityEntry.Entity).UpdateBy = "1";
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreateDate = DateTime.Now;
                    ((BaseEntity)entityEntry.Entity).CreateBy = "1";
                }
            }


        }

        #endregion

        #region CleanString

        private void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.Fa2En().FixPersianChars();
                        if (newVal == val)
                            continue;
                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }

        #endregion
        #endregion

    }
}