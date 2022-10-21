using System;
using System.Collections;
using savings_app_backend.Models;

namespace savings_app_backend.Models
{
    public class SearchingList<T> : IEnumerable<T>
    {
      
        public enum SortBy
        {
            Name,
            Id,
            Price
        };

        public SortBy Sort { get; set; }

        private List<T> _data;
        public SearchingList(IEnumerable<T> _data)
        {
            this._data = _data.ToList();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
           foreach(var item in _data){
                if(item == null)
                    break;
                yield return item;
           }
        }


        public IEnumerable<T> Search(string search, Func<T, string> ToSearchabableText)
        {
            if (search != null && search != ""){
               
                string searchText = search.ToLower();
                _data.RemoveAll(el => !ToSearchabableText(el).ToLower().Contains(searchText));
            }
            return _data;
        }

        public IEnumerable<T> SortByName(Func<T, string> ToObjName)
        {
            _data.Sort((T x, T y) =>
            {
                string firstName = ToObjName(x);
                string secongName = ToObjName(y);

                if (firstName == null && secongName == null) return 0;
                if (firstName == null) return -1;
                if (secongName == null) return 1;
                return firstName.CompareTo(secongName);
            });
            return _data;
        }

        public IEnumerable<T> Filtration(string[] filters, Func<T, string> ToCategory)
        {
            if (filters.Length == 0)
                return _data;

            _data.RemoveAll(el => {
                bool delete = false;

                foreach (string filter in filters)
                {
                    delete = ToCategory(el).Equals(filter) ? true : delete;
                }

                return !delete;
            });

            return _data;
        }

    }
}