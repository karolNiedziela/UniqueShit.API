using System.Reflection;
using UniqueShit.Domain.Core.Primitives.Results;

namespace UniqueShit.Domain.Core.Primitives
{
    public abstract class Enumeration<TEnum> : IEqualityComparer<Enumeration<TEnum>>
           where TEnum : Enumeration<TEnum>
    {
        private static readonly Dictionary<int, TEnum> Enumerations = GetEnumerations();

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; protected init; }

        public string Name { get; protected init; }

        public static IReadOnlyCollection<TEnum> List => [.. Enumerations.Values];

        public static Result<TEnum> FromValue(int id)
        {
            return Enumerations.TryGetValue(id, out TEnum? enumeration) ? enumeration : new Error($"{typeof(TEnum).Name}.NotFound", $"{typeof(TEnum).Name} with given id '{id}' does not exist.");
        }

        public static Result<TEnum> FromName(string name)
        {
            var enumeration = Enumerations
                .Values
                .FirstOrDefault(e => e.Name == name);

            if (enumeration == null)
            {
                return new Error($"{typeof(TEnum).Name}.NotFound", $"{typeof(TEnum).Name} with given name '{name}' does not exist.");
            }

            return enumeration;
        }
        public bool Equals(Enumeration<TEnum>? x, Enumeration<TEnum>? y)
        {
            if (x is null && y is null)
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            return x.GetType() == y.GetType() && x.Id == y.Id;
        }

        public override bool Equals(object? obj)
        {
            return obj is Enumeration<TEnum> other && Equals(this, other);
        }

        public int GetHashCode(Enumeration<TEnum> obj)
        {
            return obj.Id.GetHashCode();
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        private static Dictionary<int, TEnum> GetEnumerations()
        {
            var enumerationType = typeof(TEnum);

            var fieldsForType = enumerationType
                .GetFields(
                    BindingFlags.Public |
                    BindingFlags.Static |
                    BindingFlags.FlattenHierarchy)
                .Where(fieldInfo =>
                    enumerationType.IsAssignableFrom(fieldInfo.FieldType))
                .Select(fieldInfo =>
                    (TEnum)fieldInfo.GetValue(default)!);

            return fieldsForType.ToDictionary(x => x.Id);
        }
    }
}