using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using CarSale.Data.Entities;

namespace carpass.be.Infrastructure.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }


    public DbSet<Car> Cars { get; set; }
    public DbSet<CarFeature> CarFeatures { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var param = Expression.Parameter(entityType.ClrType);
            var propMethodInformation = typeof(EF).GetMethod("Property")?.MakeGenericMethod(typeof(bool));
            if (propMethodInformation != null)
            {
                var deletedProp = Expression.Call(propMethodInformation, param, Expression.Constant("Deleted"));
                var compareExpression = Expression.MakeBinary(ExpressionType.Equal, deletedProp, Expression.Constant(false));
                var lambda = Expression.Lambda(compareExpression, param);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }

        }
    }
}
