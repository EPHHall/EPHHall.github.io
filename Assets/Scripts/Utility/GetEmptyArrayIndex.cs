using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Util
{
    public class GetEmptyArrayIndex
    {
        public static int GetArrayIndex<T>(T[] array)
        {
            int index = -1;

            for (int i = 0; i < array.Length; i++)
            {
                if(array[i] == null)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }
    }
}
