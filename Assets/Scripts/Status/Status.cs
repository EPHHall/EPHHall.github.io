using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.GameController;


namespace SS.StatusSpace
{
    [System.Serializable]
    public class Status
    {
        public enum StatusName
        {
            FireDamage,
            ArcaneDamage,
            Controlled,
            Possessed
        }

        //main stats
        [Space(5)]
        [Header("Main stats")]
        public StatusName statusName;
        public int magnitude;
        public int duration;

        public TurnTaker applier;

        //possible stats
        [Space(5)]
        [Header("Possible stats")]
        public int radius;
        public GameObject controller;
        public List<Status> statusesToApply = new List<Status>();
        public bool unarmedOnly;
        public Spells.Effect applyingEffect;
        public string explanation_DealsDamage;

        
        public Status(Status status)
        {
            this.statusName = status.statusName;
            this.magnitude = status.magnitude;
            this.duration = status.duration;
            this.applier = status.applier;
            this.radius = status.radius;

            SetExplanations();
        }
        public Status(StatusName name, int magnitude, int duration, TurnTaker applier)
        {
            this.statusName = name;
            this.magnitude = magnitude;
            this.duration = duration;
            this.applier = applier;

            SetExplanations();
        }
        public Status(StatusName name, int magnitude, int duration, TurnTaker applier, int radius)
        {
            this.statusName = name;
            this.magnitude = magnitude;
            this.duration = duration;
            this.applier = applier;
            this.radius = radius;

            SetExplanations();
        }

        public void SetExplanations()
        {
            explanation_DealsDamage = "deals ";
            if (GetDamageExplanation() == "no")
            {
                explanation_DealsDamage += GetDamageExplanation() + " damage.";
            }
            else
            {
                explanation_DealsDamage += magnitude + " " + GetDamageExplanation() + " damage at the end of the target's turn.";
            }
        }

        public void AddStatusToApply(Status status)
        {
            statusesToApply.Add(status);

            status.applyingEffect = applyingEffect;
        }

        public string GetName()
        {
            string toReturn;

            switch (statusName)
            {
                case StatusName.ArcaneDamage:
                    toReturn = "Enchanted";
                    break;
                case StatusName.Controlled:
                    toReturn = "Hypnotized";
                    break;
                case StatusName.FireDamage:
                    toReturn = "Burning";
                    break;
                case StatusName.Possessed:
                    toReturn = "Possessed";
                    break;
                default:
                    toReturn = "No Name";
                    break;
            }

            return toReturn;
        }

        public string GetDamageExplanation()
        {
            string toReturn;

            switch (statusName)
            {
                case StatusName.ArcaneDamage:
                    toReturn = "arcane damage";
                    break;
                case StatusName.Controlled:
                    toReturn = "no";
                    break;
                case StatusName.FireDamage:
                    toReturn = "fire damage";
                    break;
                case StatusName.Possessed:
                    toReturn = "no";
                    break;
                default:
                    toReturn = "No Name";
                    break;
            }

            return toReturn;
        }
    }
}
