using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HomeForPets.Volunteers.Infrastucture.Configurations.Extensions;
public static class ModelBuilderExtensions
{
    public static void ConfigureCustomConversion<TEntity, TDto, TValue>(
        this ModelBuilder modelBuilder,
        Expression<Func<TEntity, IReadOnlyList<TValue>>> propertyExpression,
        Func<TDto, TValue> createFunc,
        string columnName) where TEntity : class
    {
        modelBuilder.Entity<TEntity>()
            .Property(propertyExpression)
            .HasConversion(
                valueList => JsonSerializer.Serialize(valueList, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<TDto>>(json, JsonSerializerOptions.Default)!
                    .Select(createFunc)
                    .ToList(),
                new ValueComparer<IReadOnlyList<TValue>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => (IReadOnlyList<TValue>)c.ToList())
            ).HasColumnName(columnName);
    }
}