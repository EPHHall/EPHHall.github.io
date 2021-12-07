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
        public List<Item.Weapon> activeWeapons;

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
            if (!Application.isPlaying)
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
                damage = main.GetTotalDamage();
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

                main.deliveredEffects.Clear();
                foreach (Effect de in deliveredByMain)
                {
                    main.deliveredEffects.Add(de);
                }

                main.targetMeEffects.Clear();
                foreach (Effect te in targetMain)
                {
                    main.targetMeEffects.Add(te);
                }

                main.InvokeEffect(Target.selectedTargets, main.normallyValid);

                //Not sure yet if this should be handled in the Effect script(s) or here.
                //foreach (Effect delivered in deliveredByMain)
                //{
                //    if (main.CanDeliverThisEffect(delivered))
                //    {
                //        delivered.InvokeEffect(Target.selectedTargets, delivered.normallyValid);
                //    }
                //}
                //foreach (Effect tm in targetMain)
                //{
                //    if (main.CanBeTargetedBy(tm))
                //    {
                //        main.ModifyViaEffect(tm);
                //    }
                //}
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
                SS.Util.SpawnRange.SpawnTargetingRange<GameObject>(transform.position, range, targetableTile, targetableTile);

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

            main.spellAttachedTo = this;
        }
        public void UnsetMain(Effect newEffect)
        {
            main = null;

            SetAllStats();
            ApplyModifiers();
        }

        public void SetDelivered(Effect newEffect, int index)
        {
            while (deliveredByMain.Count < index + 1)
            {
                deliveredByMain.Add(null);
            }

            deliveredByMain[index] = newEffect;

            SetAllStats();
            ApplyModifiers();

            if(newEffect != null)
                newEffect.spellAttachedTo = this;
        }

        public void SetTargetsMain(Effect newEffect, int index)
        {
            while (targetMain.Count < index + 1)
            {
                targetMain.Add(null);
            }

            targetMain[index] = newEffect;

            SetAllStats();
            ApplyModifiers();

            newEffect.spellAttachedTo = this;
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
