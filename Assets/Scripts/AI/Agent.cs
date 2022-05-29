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

        public Vector2 positionAtStartofTurn;

        public SS.Character.CharacterStats characterStats;

        private GameController.TurnManager turnManager;

        private bool running = false;

        public override void Awake()
        {
            base.Awake();

            InitializePackagesAndBehaviors();

            characterStats = GetComponent<SS.Character.CharacterStats>();

            if (targetFaction == Faction.FactionName.PlayerFaction)
            {
                mainTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Target>();
            }
            else
            {
                mainTarget = null;
            }

            defaultFaction = targetFaction;
            defaultTarget = mainTarget;

            turnManager = FindObjectOfType<GameController.TurnManager>();
        }

        private void Update()
        {
            if (activate)
            {

            }

            if (running)
            {
                bool completed = true;

                foreach(AIPackage pack in packages)
                {
                    if(pack.run)
                    {
                        completed = false;
                        break;
                    }
                }

                if(completed)
                {
                    running = false;
                    //turnManager.ChangeTurnTaker(-1);
                }
            }
        }

        public void FinishedOverride()
        {
            //turnManager.ChangeTurnTaker(-1);
        }

        public void ResetFactionsAndTargets()
        {
            targetFaction = defaultFaction;
            mainTarget = defaultTarget;
        }

        public void InitializePackagesAndBehaviors()
        {
            if (name == "Ogre Room 1")
            {
                Debug.Log("In Thing");
            }
            
            //well this is ugly
            foreach (AIPackage package in packages)
            {
                if (name == "Ogre Room 1")
                {
                    Debug.Log("In Thing 2");
                }

                package.SetAttachedAgent(this);

                foreach (AIBehavior behavior in package.behaviors)
                {
                    if (name == "Ogre Room 1")
                    {
                        Debug.Log("In Thing 3");
                    }

                    behavior.agent = this;
                    behavior.package = package;
                }
                foreach (BehaviorGroup group in package.behaviorGroups)
                {
                    if (name == "Ogre Room 1")
                    {
                        Debug.Log("In Thing 4");
                    }

                    foreach (AIBehavior behavior in group.behaviors)
                    {
                        if (name == "Ogre Room 1")
                        {
                            Debug.Log("In Thing 5");
                        }

                        behavior.package = package;
                        behavior.agent = this;
                    }
                }
            }
        }

        public override void StartTurn()
        {
            Debug.Log("In Start Turn");

            base.StartTurn();

            SS.Util.CharacterStatsInterface.ResetAP(characterStats);
            SS.Util.CharacterStatsInterface.ResetMana(characterStats);

            positionAtStartofTurn = transform.position;

            foreach (AIPackage package in packages)
            {
                if (package.groupsVersion)
                {
                    package.InvokeAI(true);
                }
                else
                {
                    package.InvokeAI();
                }
            }

            running = true;
            //EndTurn();
        }

        public void SetPosition(Vector2 newPosition)
        {
            transform.position = newPosition;
        }
    }
}
