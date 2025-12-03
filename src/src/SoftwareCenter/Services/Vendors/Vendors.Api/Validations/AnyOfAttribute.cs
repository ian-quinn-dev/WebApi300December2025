using System.ComponentModel.DataAnnotations;

namespace Vendors.Api.Validations;

/// <summary>
///     This is a custom validation attribute that ensures at least one of the specified properties is non-null and, if a
///     string, non-empty.
/// </summary>
/// <param name="Properties"></param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class AnyOfAttribute(params string[] properties) : ValidationAttribute
{
    public IList<string> AnyOfProperties { get; } = properties;

    public override bool IsValid(object? value)
    {
        if (value == null) return true;

        var type = value.GetType();
        var propertyInfos = type.GetProperties();
        var validCount = 0;

        foreach (var propertyName in AnyOfProperties)
        {
            var propertyInfo = propertyInfos.FirstOrDefault(p => p.Name == propertyName);
            if (propertyInfo == null) continue;

            var propertyValue = propertyInfo.GetValue(value);
            if (propertyValue == null) continue;
            if (propertyInfo.PropertyType == typeof(string))
            {
                if (!string.IsNullOrWhiteSpace((string)propertyValue)) validCount++;
            }
            else
            {
                validCount++;
            }
        }

        return validCount > 0;
    }
}