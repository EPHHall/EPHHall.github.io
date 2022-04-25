using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Character;
using SS.Util;
using SS.Animation;

namespace SS.Spells
{
    [ExecuteAlways]
    public class Spell : MonoBehaviour
    {
        private static EventBool CAST_WAS_ATTEMPTED;
        private static EventBool CAST_WAS_SUCCESSFUL;

        [Space(5)]
        [Header("Stats")]
        public int range;
        public int damage;
        public int manaCost;
        public int apCost;
        public int duration;

        public int currentSpellPoints = 10;
        public int maxSpellPoints = 10;

        public int modifiedDamage;

        [Space(5)]
        [Header("Casting Variables")]
        public Effect main; private Effect previousMain;
        public List<Effect> targetMain;
        public List<Effect> deliveredByMain;
        public List<Modifier> modifiers;
        public Vector2 spellOrigin;

        [Space(5)]
        [Header("Resources")]
        public EffectResources er;
        public GameObject targetableTile;

        [Space(5)]
        [Header("For stats screen")]
        public string description;
        public string statusesDescription;

        [Space(5)]
        [Header("Animation Stuff")]
        public List<AnimationObject> animationsToPlay;

        [Space(5)]
        [Header("Misc")]
        public string spellName;
        public Sprite icon;
        public CharacterStats caster;
        public int timesCastThisRound = 0;
        public Animation.AnimationManager animationManager;
        public Animation.AnimationObjectManager animationObjectManager;

        public virtual void Start()
        {
            if (CAST_WAS_ATTEMPTED == null)
            {
                if (GameObject.Find("Cast Was Attempted?") != null)
                {
                    CAST_WAS_ATTEMPTED = GameObject.Find("Cast Was Attempted?").GetComponent<EventBool>();
                    CAST_WAS_ATTEMPTED.Set(false);
                }
                else
                {
                    CAST_WAS_ATTEMPTED = new GameObject("Cast Was Attempted?").AddComponent<EventBool>();
                    CAST_WAS_ATTEMPTED.Set(false);
                }
            }
            if (CAST_WAS_SUCCESSFUL == null)
            {
                if (GameObject.Find("Cast Was Successful?") != null)
                {
                    CAST_WAS_SUCCESSFUL = GameObject.Find("Cast Was Successful?").GetComponent<EventBool>();
                    CAST_WAS_SUCCESSFUL.Set(false);
                }
                else
                {
                    CAST_WAS_SUCCESSFUL = new GameObject("Cast Was Successful?").AddComponent<EventBool>();
                    CAST_WAS_SUCCESSFUL.Set(false);
                }
            }
            
            previousMain = main;

            er = GameObject.FindGameObjectWithTag("Effect Resources").GetComponent<EffectResources>();

            targetableTile = er.GetCastingTile();

            animationObjectManager = FindObjectOfType<Animation.AnimationObjectManager>();
            animationManager = FindObjectOfType<Animation.AnimationManager>();
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

                        //modifiedDamage = 0;
                        ApplyModifiers();
                    }
                    else
                    {
                        SetAllStats();

                        //modifiedDamage = 0;
                        ApplyModifiers();
                    }

                    if (main != null)
                    {
                        icon = main.icon;
                    }
                }
            }
        }

        public void TurnChangeReset()
        {
            timesCastThisRound = 0;
        }

        public void SetAllStats()
        {
            if (main != null)
            {
                range = main.range;
                damage = main.GetTotalDamage();
                manaCost = main.manaCost;
                apCost = main.actionPointCost;
                duration = main.duration;

                currentSpellPoints = maxSpellPoints - main.spellPointCost;

                foreach (Effect e in deliveredByMain)
                {
                    if (e == null) continue;

                    currentSpellPoints -= e.spellPointCost;
                }
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
            manaCost = newValue;
            apCost = newValue;
            duration = newValue;

            currentSpellPoints = maxSpellPoints;
        }

        public void ApplyModifiers()
        {
            if (main != null)
            {
                modifiedDamage = 0;

                foreach (Modifier modifier in modifiers)
                {
                    range += modifier.range;

                    damage += modifier.damage;
                    modifiedDamage += modifier.damage;

                    manaCost += modifier.manaCost;
                    apCost += modifier.actionPointCost;

                    currentSpellPoints -= modifier.spellPointCost;

                    duration += modifier.duration;
                }
            }
        }

        public virtual void CastSpell(bool overrideTileRequirement)
        {
            caster = SS.GameController.TurnManager.currentTurnTaker.GetComponent<SS.Character.CharacterStats>();

            animationsToPlay = new List<AnimationObject>();
            foreach (Effect e in deliveredByMain)
            {
                if (e == null) continue;

                animationsToPlay.Add(e.animationToPlay);
            }
            animationsToPlay.Insert(0, main.animationToPlay);



            //SS.GameController.TurnManager.staticPrintTurnTaker = true;
            SS.Character.CharacterStats character = caster;

            int cost = 0;
            if (apCost < 1)
            {
                if (timesCastThisRound < Mathf.Abs(1 - apCost))
                {
                    cost = 0;
                }
                else
                {
                    cost = 1;
                }
            }
            else
            {
                cost = apCost;
            }

            if (character.actionPoints < cost)
            {
                GameObject.FindObjectOfType<SS.UI.UpdateText>().SetMessage("Too few action points!", Color.red);
            }

            if ((CastingTile.selectedTiles.Count > 0 || overrideTileRequirement) && character != null && character.mana >= manaCost && character.actionPoints >= cost)
            {
                CAST_WAS_SUCCESSFUL.Set(true);

                if (GetComponent<AudioSource>() != null)
                {
                    if (main.soundEffect == null)
                    {
                        GetComponent<AudioSource>().clip = er.GetDefaultSpellAudio();
                    }
                    else
                    {
                        GetComponent<AudioSource>().clip = main.soundEffect;
                    }
                    GetComponent<AudioSource>().Play();
                }
                else
                {
                    Debug.Log("It was null");
                }

                SpellManager.SetTargetsList(Target.selectedTargets);
                SpellManager.caster = caster.transform;

                SS.Util.CharacterStatsInterface.DamageMana(character, manaCost);

                SS.Util.CharacterStatsInterface.DamageActionPoints(character, cost);

                timesCastThisRound++;

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

                foreach (Target target in Target.selectedTargets)
                {
                    Damage newDamage = new Damage(Damage.DamageType.Arcane, modifiedDamage);

                    TargetInterface.DamageTargetWhenAnimationHits(target, newDamage, main);

                    foreach (AnimationObject animation in animationsToPlay)
                    {
                        animationManager.AddAnimation(new AnimationPlusObject(animation, target.transform, "Play"));
                    }
                }

                if(caster.tag == "Player")
                {
                    animationManager.RunAnimations();
                }
            }
        }
        public virtual void CastSpell(bool overrideTileRequirement, bool ignoreManaCost, bool ignoreAPCost)
        {
            CastSpell(overrideTileRequirement);
            SS.Character.CharacterStats character = SS.GameController.TurnManager.currentTurnTaker.GetComponent<SS.Character.CharacterStats>();

            if (ignoreManaCost)
            {
                SS.Util.CharacterStatsInterface.AddMana(character, manaCost);
            }
            if (ignoreAPCost)
            {
                SS.Util.CharacterStatsInterface.AddActionPoints(character, apCost);
            }
        }

        public virtual void ShowRange()
        {
            CAST_WAS_ATTEMPTED.Set(true);

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

            if (main != null)
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

            if(newEffect != null)
                newEffect.spellAttachedTo = this;
        }

        public void AddModifier(Modifier modifier)
        {
            //Debug.Log("In Add Modidifer");

            if (modifier == null) return;

            modifiers.Add(modifier);

            SetAllStats();
            ApplyModifiers();
        }

        public void RemoveModifier(Modifier modifier)
        {
            //
            //Debug.Log("In Remove Modidifer");

            modifiers.Remove(modifier);

            SetAllStats();
            ApplyModifiers();
        }

        public static bool Get_IfCastWasSuccessful()
        {
            bool temp = CAST_WAS_SUCCESSFUL.Get();
            CAST_WAS_SUCCESSFUL.Set(false);
            return temp;
        }
        public static bool Get_IfCastWasAttempted()
        {
            bool temp = CAST_WAS_ATTEMPTED.Get();
            CAST_WAS_ATTEMPTED.Set(false);
            return temp;
        }
    }
}
