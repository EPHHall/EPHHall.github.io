using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Util
{
    public class ArrayContains
    {
        public static bool Contains<T>(T[] array, T item)
        {
            bool contains = false;

            foreach (T t in array)
            {
                if (t.Equals(item))
                {
                    contains = true;
                    break;
                }
            }

            return contains;
        }
    }
}
