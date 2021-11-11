using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Util;

namespace SS.UI
{
    public class DisplayInfo_TargetButton : SS_DisplayInfoBox
    {
        TargetButtonRadialMenu targetButton;

        public override void Start()
        {
            base.Start();

            targetButton = GetComponent<TargetButtonRadialMenu>();
        }

        public override void SetInfoString()
        {
            base.SetInfoString();

            int placeToReturnTo = 0;
            int prevPlace = placeToReturnTo;
            int counter = 0;
            string specialString = ReturnFirstSpecialString(infoString, ref placeToReturnTo);
            string temp = infoString;
            while (specialString != null)
            {
                if (counter > 99999)
                {
                    infoString = "Oops";
                    return;
                }

                EvaluateSpecialString(ref specialString);
                temp = ReplaceFirstSpecialString(temp, specialString);

                specialString = ReturnFirstSpecialString(infoString, ref placeToReturnTo);

                counter++;
            }

            infoString = temp;
        }

        public string ReturnFirstSpecialString(string og, ref int placeToReturnTo)
        {
            for (int i = placeToReturnTo; i < og.Length; i++)
            {
                placeToReturnTo = i+1;

                if (og[i] == '<')
                {
                    int j;
                    bool foundCloser = false;
                    //+2 because if the very next character is a closer, then there's no point. There's no special string associated with the opener.
                    for (j = i+2; j < og.Length; j++)
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
                    if (targetButton.associatedTarget.targetName != "")
                        evaluation = targetButton.associatedTarget.targetName;
                    else
                        evaluation = targetButton.associatedTarget.name;
                    break;

                case "current spell":
                    if (SS.Spells.SpellManager.activeSpell.spellName != "")
                        evaluation = SS.Spells.SpellManager.activeSpell.spellName;
                    else
                        evaluation = SS.Spells.SpellManager.activeSpell.name;
                    break;
                default:
                    evaluation = "No case";
                    break;
            }
        }
    }
}
