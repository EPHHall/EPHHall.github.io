using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Spells
{
    [ExecuteAlways]
    public class Spell : MonoBehaviour
    {
        [Space(5)]
        [Header("Stats")]
        public int range;
        public int damage;
        public int castSpeed;
        public int manaCost;
        public int apCost;

        public int currentSpellPoints = 10;
        public int maxSpellPoints = 10;

        [Space(5)]
        [Header("Casting Variables")]
        public Effect main; private Effect previousMain;
        public List<Effect> targetMain;
        public List<Effect> deliveredByMain;
        public List<Modifier> modifiers;
        public Vector2 spellOrigin;
        public GameObject targetableTile;

        [Space(5)]
        [Header("For stats screen")]
        public string description;
        public string statusesDescription;

        [Space(5)]
        [Header("Misc")]
        public string spellName;
        public Sprite icon;

        public virtual void Start()
        {
            previousMain = main;
        }

        public virtual void Update()
        {
            if (Application.isEditor)
            {
                //if (previousMain != main || previousMain == null)
                {
                    previousMain = main;

                    if (main == null)
                    {
                        SetAllStats(0);
                        ApplyModifiers();
                    }
                    else
                    {
                        SetAllStats();
                        ApplyModifiers();
                    }

                    if (main != null)
                    {
                        icon = main.icon;
                    }
                }
            }
        }

        public void SetAllStats()
        {
            if (main != null)
            {
                range = main.range;
                damage = main.damage;
                castSpeed = main.speed;
                manaCost = main.manaCost;
                apCost = main.actionPointCost;

                currentSpellPoints = maxSpellPoints - main.spellPointCost;
            }
            else
            {
                SetAllStats(0);
            }
        }
        public void SetAllStats(int newValue)
        {
            range = newValue;
            damage = newValue;
            castSpeed = newValue;
            manaCost = newValue;
            apCost = newValue;

            currentSpellPoints = maxSpellPoints;
        }

        public void ApplyModifiers()
        {
            if (main != null)
            {
                foreach (Modifier modifier in modifiers)
                {
                    range += modifier.range;
                    damage += modifier.damage;
                    castSpeed += modifier.speed;
                    manaCost += modifier.manaCost;
                    apCost += modifier.actionPointCost;

                    currentSpellPoints -= modifier.spellPointCost;
                }
            }
        }

        public virtual void CastSpell(bool overrideTileRequirement)
        {
            //SS.GameController.TurnManager.staticPrintTurnTaker = true;

            SS.Character.CharacterStats character = SS.GameController.TurnManager.currentTurnTaker.GetComponent<SS.Character.CharacterStats>();
            if ((CastingTile.selectedTiles.Count > 0 || overrideTileRequirement) && character != null && character.mana >= manaCost && character.actionPoints >= apCost)
            {
                SS.Util.CharacterStatsInterface.DamageMana(character, manaCost);
                SS.Util.CharacterStatsInterface.DamageActionPoints(character, apCost);

                //modifies the effect, so foreach isn't appropriate. Clears out the previous effects 
                //delivered by main, then adds the current effects that need to be delivered.
                for (int i = 0; i < main.deliveredEffects.Count; i++)
                {
                    main.deliveredEffects[i].EndEffect();
                }
                for (int i = 0; i < deliveredByMain.Count; i++)
                {
                    main.deliveredEffects.Add(deliveredByMain[i]);
                }

                if (main.targetable)
                {
                    foreach (Effect effect in targetMain)
                    {
                        effect.TargetGivenEffect(main);
                    }
                }

                main.InvokeEffect(Target.selectedTargets, main.normallyValid);
            }
        }

        public virtual void ShowRange()
        {
            if (currentSpellPoints < 0)
            {
                GameObject.Find("Player Update Text").GetComponent<SS.UI.UpdateText>().SetMessage("Negative spell points!", Color.red);
                return;
            }

            if (main != null)
            {
                SS.Util.SpawnRange.SpawnTargetingRange(transform.position, range, targetableTile, targetableTile);

                SpellManager.activeSpell = this;
            }
            else
            {
                GameObject.Find("Player Update Text").GetComponent<SS.UI.UpdateText>().SetMessage("No main effect!", Color.red);
            }
        }

        //SPELL CRAFTING
        public void SetMain(Effect newEffect)
        {
            main = newEffect;

            SetAllStats();
            ApplyModifiers();
        }
        public void UnsetMain(Effect newEffect)
        {
            main = null;

            SetAllStats();
            ApplyModifiers();
        }

        public void AddModifier(Modifier modifier)
        {
            modifiers.Add(modifier);

            SetAllStats();
            ApplyModifiers();
        }

        public void RemoveModifier(Modifier modifier)
        {
            modifiers.Remove(modifier);

            SetAllStats();
            ApplyModifiers();
        }
    }
}
