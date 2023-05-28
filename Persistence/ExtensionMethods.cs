using FastMember;
using Npgsql;
namespace art_gallery_api.Persistence
{
    public static class ExtensionMethods
    {
        public static void MapTo<T>(this NpgsqlDataReader dr, T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var fastMember = TypeAccessor.Create(entity.GetType());
            var props = fastMember.GetMembers().Select(x => x.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < dr.FieldCount; i++)
            {
                var prop = props.FirstOrDefault(x => x.Equals(ToCamelCase(dr.GetName(i)), StringComparison.OrdinalIgnoreCase));
                if (!string.IsNullOrEmpty(prop)) fastMember[entity, prop] = dr.IsDBNull(i) ? null : dr.GetValue(i);
            }
        }

        public static string ToCamelCase(this string str)
        {
            var words = str.Split(new[] { "_", " " }, StringSplitOptions.RemoveEmptyEntries);
            words = words
                .Select(word => char.ToUpper(word[0]) + word.Substring(1))
                .ToArray();
            return string.Join(string.Empty, words);
        }
    }
}