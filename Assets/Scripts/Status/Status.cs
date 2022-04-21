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
            None,
            FireDamage,
            ArcaneDamage,
            Controlled,
            Possessed,
            ControlledByEnemy
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
        private List<Status> statusesToApply_Original = new List<Status>();
        public bool unarmedOnly;
        public Spells.Effect applyingEffect;
        public string explanation_DealsDamage;

        [Space(5)]
        public string explanation_Main;
        public string explanation_Main_Base;
        public string explanation_Main_Name;

        
        public Status(Status status)
        {
            this.statusName = status.statusName;
            this.magnitude = status.magnitude;
            this.duration = status.duration;
            this.applier = status.applier;
            this.radius = status.radius;
            this.statusesToApply = new List<Status>(status.statusesToApply);
            this.statusesToApply_Original = new List<Status>(statusesToApply);

            SetExplanations();
        }
        public Status(StatusName name, int magnitude, int duration, TurnTaker applier)
        {
            this.statusName = name;
            this.magnitude = magnitude;
            this.duration = duration;
            this.applier = applier;
            this.statusesToApply = new List<Status>();
            this.statusesToApply_Original = new List<Status>(statusesToApply);

            SetExplanations();
        }
        public Status(StatusName name, int magnitude, int duration, TurnTaker applier, int radius)
        {
            this.statusName = name;
            this.magnitude = magnitude;
            this.duration = duration;
            this.applier = applier;
            this.radius = radius;
            this.statusesToApply = new List<Status>();
            this.statusesToApply_Original = new List<Status>(statusesToApply);

            SetExplanations();
        }

        public void ResetStatus()
        {
            statusesToApply = new List<Status>(statusesToApply_Original);
        }

        public Character.Damage.DamageType GetDamageType()
        {
            Character.Damage.DamageType toReturn;

            switch (statusName)
            {
                case StatusName.ArcaneDamage:
                    toReturn = Character.Damage.DamageType.Arcane;
                    break;
                case StatusName.Controlled:
                    toReturn = Character.Damage.DamageType.None;
                    break;
                case StatusName.FireDamage:
                    toReturn = Character.Damage.DamageType.Fire;
                    break;
                case StatusName.Possessed:
                    toReturn = Character.Damage.DamageType.None;
                    break;
                default:
                    toReturn = Character.Damage.DamageType.None;
                    break;
            }

            return toReturn;
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
                explanation_DealsDamage += magnitude + " " + GetDamageExplanation() + " at the end of the target's turn.";
            }

            explanation_Main = GetName() + " " + GetMainExplanation();
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
        public string GetMainExplanation()
        {
            string toReturn;

            switch (statusName)
            {
                case StatusName.ArcaneDamage:
                    toReturn = "infuses the target with arcane energy. The target takes damage.";
                    break;
                case StatusName.Controlled:
                    toReturn = "allows you to control the target's movements.";
                    break;
                case StatusName.FireDamage:
                    toReturn = "sets the target on fire. The target takes damage.";
                    break;
                case StatusName.Possessed:
                    toReturn = "makes the target fight for you.";
                    break;
                default:
                    toReturn = "No Name";
                    break;
            }

            return toReturn;
        }
    }
}
