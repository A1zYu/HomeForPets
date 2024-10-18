using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Core.Extensions;

public static class EfCoreFluentApiExtensions
{
    public static PropertyBuilder<IReadOnlyList<TValueObject>> ValueObjectCollectionJsonConversion<TValueObject, TDto>(
        this PropertyBuilder<IReadOnlyList<TValueObject>> builder,
        Func<TValueObject, TDto> toDtoSelector,
        Func<TDto, TValueObject> toValueObjectSelector)
    {
        return builder.HasConversion(
            valueObjects => SerializeValueObjectsCollections(valueObjects, toDtoSelector),
            json => DeserializeDtoValueObjectsCollections(json, toValueObjectSelector),
            CreateValueComparer<TValueObject>())
            .HasColumnType("jsonb");
    }

    private static string SerializeValueObjectsCollections<TValueObject,TDto>(
        IReadOnlyList<TValueObject> valueObjects,
        Func<TValueObject,TDto> selector)
    {
        var dtos = valueObjects.Select(selector);
        
        return JsonSerializer.Serialize(dtos,JsonSerializerOptions.Default);
    } 
    private static IReadOnlyList<TValueObject> DeserializeDtoValueObjectsCollections<TValueObject,TDto>(
        string json,
        Func<TDto,TValueObject> selector)
    {
        var dtos = JsonSerializer.Deserialize<IEnumerable<TDto>>(json,JsonSerializerOptions.Default)??[];
        return dtos.Select(selector).ToList();
        
    }

    private static ValueComparer<IReadOnlyList<T>> CreateValueComparer<T>()
    {
        return new ValueComparer<IReadOnlyList<T>>(
            (c1,c2)=>c1!.SequenceEqual(c2!),
            c=>c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c=> (IReadOnlyList<T>)c.ToList()
        );
    }
}