using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Util
{
    public class AddListToOriginal
    {
        //Seems to work
        public static void AddList<T>(List<T> og, List<T> addition)
        {
            foreach (T t in addition)
            {
                if(!og.Contains(t))
                    og.Add(t);
            }
        }
    }
}
