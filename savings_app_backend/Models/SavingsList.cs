using System;
using System.Collections;
using savings_app_backend.Models;

namespace savings_app_backend.Models
{
    public class SavingsList<T> : System.Collections.Generic.IEnumerable<T> where T : SavingsAppObj
    {
      
        private List<T> _data = new List<T>();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}