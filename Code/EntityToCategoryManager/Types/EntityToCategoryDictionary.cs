using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabaseActionReusables.EntityToCategoryManager.Types
{
    public class EntityToCategoryDictionary : IReadOnlyDictionary<int, IList<int>>
    {

        private IReadOnlyDictionary<int, IList<int>> internalEntityToCatDictionary;

        internal EntityToCategoryDictionary(IReadOnlyDictionary<int, IList<int>> entityToCatDictionary)
        {
            internalEntityToCatDictionary = entityToCatDictionary;
        }



        public IList<int> this[int key] => internalEntityToCatDictionary[key];

        public IEnumerable<int> Keys => internalEntityToCatDictionary.Keys;

        public IEnumerable<IList<int>> Values => internalEntityToCatDictionary.Values;

        public int Count => internalEntityToCatDictionary.Count;

        public bool ContainsKey(int key)
        {
            return internalEntityToCatDictionary.ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<int, IList<int>>> GetEnumerator()
        {
            return internalEntityToCatDictionary.GetEnumerator();
        }

        public bool TryGetValue(int key, out IList<int> value)
        {
            return internalEntityToCatDictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return internalEntityToCatDictionary.GetEnumerator();
        }


    }
}
