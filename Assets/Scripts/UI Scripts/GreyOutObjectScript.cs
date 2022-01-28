using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.UI
{
    public class GreyOutObjectScript : MonoBehaviour
    {
        public enum GreyOutSprite
        {
            None,
            All,
            NextTurn,
            Health,
            Mana,
            ActionMeter,
            Attack,
            Move,
            Spell1,
            Spell2,
            Spell3,
            Spell4,
            ActionsBar,
            StatsBar,
            PlayArea
        }

        public Sprite all;
        public Sprite allButStatsBar;
        public Sprite allButActionsBar;
        public Sprite allButPlayArea;
        public Sprite allButNextTurn;
        public Sprite allButHealth;
        public Sprite allButMana;
        public Sprite allButActionMeter;
        public Sprite allButAttack;
        public Sprite allButMovement;
        public Sprite allButSpell1;
        public Sprite allButSpell2;
        public Sprite allButSpell3;
        public Sprite allButSpell4;

        Dictionary<GreyOutSprite, Sprite> NameToSprite;

        private void Start()
        {
            NameToSprite = new Dictionary<GreyOutSprite, Sprite>()
            {
                {GreyOutSprite.None, null},
                {GreyOutSprite.All, all},
                {GreyOutSprite.NextTurn, allButNextTurn},
                {GreyOutSprite.Health, allButHealth},
                {GreyOutSprite.Mana, allButMana},
                {GreyOutSprite.ActionMeter, allButActionMeter},
                {GreyOutSprite.Attack, allButAttack},
                {GreyOutSprite.Move, allButMovement},
                {GreyOutSprite.Spell1, allButSpell1},
                {GreyOutSprite.Spell2, allButSpell2},
                {GreyOutSprite.Spell3, allButSpell3},
                {GreyOutSprite.Spell4, allButSpell4},
                {GreyOutSprite.StatsBar, allButStatsBar},
                {GreyOutSprite.ActionsBar, allButActionsBar},
                {GreyOutSprite.PlayArea, allButPlayArea},

            };

            DeactivateGreyOut();
        }

        public void ChangeSprite(GreyOutSprite greyOutSprite)
        {
            GetComponent<Image>().enabled = true;

            GetComponent<Image>().sprite = NameToSprite[greyOutSprite];

            if (GetComponent<Image>().sprite == null)
                GetComponent<Image>().enabled = false;
        }

        public void DeactivateGreyOut()
        {
            GetComponent<Image>().enabled = false;
        }
        public void ActivateGreyOut()
        {
            GetComponent<Image>().enabled = true;
        }


    }
}
