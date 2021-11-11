using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Util
{
    public class EvaluateStrings : MonoBehaviour
    {
        public static string SetInfoString(string infoString, Dictionary<string, string> evaluateOn)
        {
            int placeToReturnTo = 0;
            int prevPlace = placeToReturnTo;
            int counter = 0;
            string specialString = GetFirstSpecialString(infoString, ref placeToReturnTo);
            string temp = infoString;
            while (specialString != null)
            {
                if (counter > 99999)
                {
                    infoString = "Oops";
                    return infoString;
                }

                EvaluateSpecialString(ref specialString, evaluateOn);
                temp = ReplaceFirstSpecialString(temp, specialString);

                specialString = GetFirstSpecialString(infoString, ref placeToReturnTo);

                counter++;
            }

            infoString = temp;

            return infoString;
        }


        public static string GetFirstSpecialString(string og, ref int placeToReturnTo)
        {
            for (int i = placeToReturnTo; i < og.Length; i++)
            {
                placeToReturnTo = i + 1;

                if (og[i] == '<')
                {
                    int j;
                    bool foundCloser = false;
                    //+2 because if the very next character is a closer, then there's no point. There's no special string associated with the opener.
                    for (j = i + 2; j < og.Length; j++)
                    {
                        if (og[j] == '>')
                        {
                            foundCloser = true;
                            break;
                        }
                    }

                    if (foundCloser)
                    {
                        return og.Substring(i + 1, (j - 1) - i);
                    }
                }
            }

            return null;
        }

        public static string ReplaceFirstSpecialString(string og, string replacement)
        {
            //Debug.Log(og);

            for (int i = 0; i < og.Length; i++)
            {
                if (og[i] == '<')
                {
                    int j;
                    bool foundCloser = false;

                    for (j = i + 2; j < og.Length; j++)
                    {
                        if (og[j] == '>')
                        {
                            foundCloser = true;
                            break;
                        }
                    }

                    if (foundCloser)
                    {
                        // Hello <World>!
                        string piece1 = og.Substring(0, i);
                        string piece2 = "";
                        if (j + 1 < og.Length)
                        {
                            piece2 = og.Substring(j + 1, og.Length - (j + 1));
                        }

                        Debug.Log(piece1 + " | " + replacement + " | " + piece2);

                        return piece1 + replacement + piece2;
                    }
                }
            }

            return og;
        }

        public static void EvaluateSpecialString(ref string toEvaluate, Dictionary<string, string> evaluateOn)
        {
            foreach (string key in evaluateOn.Keys)
            {
                if (toEvaluate == key)
                {
                    toEvaluate = evaluateOn[key];
                }
            }
        }
    }

}

