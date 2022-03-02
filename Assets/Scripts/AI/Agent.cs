using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Spells;

namespace SS.AI
{
    public class Agent : SS.GameController.TurnTaker
    {
        public List<AIPackage> packages;

        public bool activate = false;

        //[System.NonSerialized]
        public List<Target> targets = new List<Target>();
        public List<Vector2> targetPositions = new List<Vector2>();
        public List<Vector2> movementPositions = new List<Vector2>();
        public List<Vector2> positionsCloserToTarget = new List<Vector2>();
        public Vector2 closest;
        public Target currentTarget;

        public TargetType filter;

        public Target mainTarget;
        public Faction.FactionName targetFaction;
        public Faction.FactionName previousTargetFaction;
        private Faction.FactionName defaultFaction;
        public Target defaultTarget;

        public List<Spell> spells;
        public Spell spellToCast;

        public SS.Character.CharacterStats characterStats;

        private void Start()
        {
            InitializePackagesAndBehaviors();

            characterStats = GetComponent<SS.Character.CharacterStats>();

            defaultFaction = targetFaction;
            defaultTarget = mainTarget;
        }

        private void Update()
        {
            if (activate)
            {
            }
        }

        public void ResetFactionsAndTargets()
        {
            targetFaction = defaultFaction;
            mainTarget = defaultTarget;
        }

        public void InitializePackagesAndBehaviors()
        {
            foreach (AIPackage package in packages)
            {
                package.SetAttachedAgent(this);

                foreach (AIBehavior behavior in package.behaviors)
                {
                    behavior.agent = this;
                }
            }
        }

        public override void StartTurn()
        {
            base.StartTurn();

            SS.Util.CharacterStatsInterface.ResetAP(characterStats);
            SS.Util.CharacterStatsInterface.ResetMana(characterStats);

            foreach (AIPackage package in packages)
            {
                package.InvokeAI();
            }

            EndTurn();
        }

        public override void EndTurn()
        {
            base.EndTurn();
        }

        public void SetPosition(Vector2 newPosition)
        {
            transform.position = newPosition;
        }
    }
}
