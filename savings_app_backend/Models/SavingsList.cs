using System;
using System.Collections;
using savings_app_backend.Models;

namespace savings_app_backend.Models
{
    public class SavingsList<T> : System.Collections.Generic.IEnumerable<T> where T : SavingsAppObj
    {
      
        private List<T> _data;
        public SavingsList(IEnumerable<T> _data)
        {
            this._data = _data.ToList();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
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


        public IEnumerable<T> Search(string[] filters, string search)
        {
            List<T> result = new List<T>(_data);

          
            if (search != null && search != ""){
               
                string searchText = search.ToLower();
                result.RemoveAll(el => !el.Name.ToLower().Contains(searchText));
            }

            if(filters.Length == 0)
                return result;

            result.RemoveAll(el => {
                bool delete = false;

                foreach(string filter in filters){
                    delete = el.Category.Equals(filter) ? true : delete;
                }

                return !delete;
            });


            return result;
        }

    }
}