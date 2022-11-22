using Microsoft.EntityFrameworkCore;
using savings_app_backend.Models.Entities;
using System.Text;

namespace savings_app_backend.Extention
{
    public static class SearchUtility
    {
        public static bool SearchObject<T>(T obj, string? searchText, params Func<T, string?>[] objToText)
        {
            string searchableText = GetSearchableText<T>(obj, objToText);

            return String.IsNullOrEmpty(searchText) ||  searchableText.Contains(searchText);
        }

        static public string GetSearchableText<T>(T obj, params Func<T, string?>[] objToText)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var text in objToText)
            {
                sb.Append(text(obj));
            }

            return sb.ToString();
        }


        public static T? GetById<T>(this IQueryable<T> queryable, Guid id) where T : IDable
        {
            return queryable.FirstOrDefault(i => i.Id.Equals(id));
        }
    }
}
