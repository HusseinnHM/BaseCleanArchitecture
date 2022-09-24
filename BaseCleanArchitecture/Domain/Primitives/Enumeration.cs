using System.Reflection;

namespace BaseCleanArchitecture.Domain.Primitives;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>, IComparable<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum>
{
    private static readonly Lazy<Dictionary<int, TEnum>> EnumerationsDictionary =
        new Lazy<Dictionary<int, TEnum>>(() => GetAllEnumerationOptions().ToDictionary(item => item.Value));


    protected Enumeration(int value, string name)
    {
        Value = value;
        Name = name;
    }


    protected Enumeration()
    {
        Value = default;
        Name = string.Empty;
    }


    public static IReadOnlyCollection<TEnum> List => EnumerationsDictionary.Value.Values.ToList();


    public int Value { get; private set; }


    public string Name { get; private set; }


    public static TEnum FromValue(int value) => EnumerationsDictionary.Value.TryGetValue(value, out var enumeration)
        ? enumeration!
        : default!;


    public static bool ContainsValue(int value) => EnumerationsDictionary.Value.ContainsKey(value);

    public static bool operator ==(Enumeration<TEnum> a, Enumeration<TEnum> b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Enumeration<TEnum> a, Enumeration<TEnum> b) => !(a == b);


    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null)
        {
            return false;
        }

        return GetType() == other.GetType() && other.Value.Equals(Value);
    }


    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (!(obj is Enumeration<TEnum> otherValue))
        {
            return false;
        }

        return GetType() == obj.GetType() && otherValue.Value.Equals(Value);
    }


    public int CompareTo(Enumeration<TEnum>? other) => other is null ? 1 : Value.CompareTo(other.Value);


    public override int GetHashCode() => Value.GetHashCode();


    private static IEnumerable<TEnum> GetAllEnumerationOptions()
    {
        Type enumType = typeof(TEnum);

        IEnumerable<Type> enumerationTypes = Assembly
            .GetAssembly(enumType)!
            .GetTypes()
            .Where(type => enumType.IsAssignableFrom(type));

        var enumerations = new List<TEnum>();

        foreach (Type enumerationType in enumerationTypes)
        {
            List<TEnum> enumerationTypeOptions = GetFieldsOfType<TEnum>(enumerationType);

            enumerations.AddRange(enumerationTypeOptions);
        }

        return enumerations;
    }


    private static List<TFieldType> GetFieldsOfType<TFieldType>(Type type) =>
        type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fieldInfo => type.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (TFieldType)fieldInfo.GetValue(null)!)
            .ToList();
}