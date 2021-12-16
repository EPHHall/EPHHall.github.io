using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.UI
{
    public class DisplayInfo_SpellButton : SS_DisplayInfoBox
    {
        public SS.Spells.Spell spell;

        public override void Start()
        {
            base.Start();
        }

        public override void SetInfoString()
        {
            base.SetInfoString();

            infoString = spell.spellName;

            //int placeToReturnTo = 0;
            //int prevPlace = placeToReturnTo;
            //int counter = 0;
            //string specialString = GetFirstSpecialString(infoString, ref placeToReturnTo);
            //string temp = infoString;
            //while (specialString != null)
            //{
            //    if (counter > 99999)
            //    {
            //        infoString = "Oops";
            //        return;
            //    }

            //    EvaluateSpecialString(ref specialString);
            //    temp = ReplaceFirstSpecialString(temp, specialString);

            //    specialString = GetFirstSpecialString(infoString, ref placeToReturnTo);

            //    counter++;
            //}

            //infoString = temp;
        }

        public string GetFirstSpecialString(string og, ref int placeToReturnTo)
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

        public string ReplaceFirstSpecialString(string og, string replacement)
        {
            Debug.Log(og);

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

        public override void EvaluateSpecialString(ref string evaluation)
        {
            base.EvaluateSpecialString(ref evaluation);

            switch (evaluation)
            {
                case "name":
                case "Name":
                    if (spell.name != "")
                        evaluation = spell.name;
                    else
                        evaluation = name;
                    break;

                case "range":
                    evaluation = SS.Spells.SpellManager.activeSpell.spellName;
                    break;
                default:
                    evaluation = "No case";
                    break;
            }
        }
    }
}
