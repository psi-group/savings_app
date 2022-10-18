using System;
using System.Collections;
using savings_app_backend.Models;

namespace savings_app_backend.Models
{
    public class SearchingList<T> : IEnumerable<T>
    {
      
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
            List<T> result = new List<T>(_data);

          
            if (search != null && search != ""){
               
                string searchText = search.ToLower();
                result.RemoveAll(el => !ToSearchabableText(el).ToLower().Contains(searchText));
            }
            return result;
        }

    }
}