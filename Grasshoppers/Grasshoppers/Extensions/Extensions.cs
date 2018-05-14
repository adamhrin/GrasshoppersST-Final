using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Grasshoppers.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Adds all the data to a ObservableCollection
        /// </summary>
        public static void AddRange<T>(this ObservableCollection<T> list, IEnumerable<T> data)
        {
            if (list == null || data == null)
            {
                return;
            }

            foreach (T t in data)
            {
                list.Add(t);
            }
        }
    }
}
