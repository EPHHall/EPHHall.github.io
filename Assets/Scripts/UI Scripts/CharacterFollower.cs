using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SS.Character;
using SS.Spells;

namespace SS.UI
{
    public class CharacterFollower : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        static StatsCard card;

        [Space(5)]
        [Header("Don't Touch")]
        public StatsCard statsCard;
        public Transform following;
        private CharacterStats stats;
        public bool showing;
        List<Target> targets = new List<Target>();
        public bool tryToShowStatsCard;

        private void Start()
        {
            //this.enabled = false;

            statsCard = FindObjectOfType<StatsCard>();

            if (card == null)
            {
                card = statsCard;
            }
            if (statsCard == null)
            {
                statsCard = card;
            }

            following = GetComponent<Follow>().toFollow;

            stats = following.GetComponent<CharacterStats>();
        }

        private void LateUpdate()
        {
            if (tryToShowStatsCard && !CastingTile.PointerOverAtLeastOne && !CastingTile.PointerWasOverAtLeastOne)
            {
                statsCard.ActivateStatsCard(transform.position.x, GetTargets(), targets[0].GetComponent<Character.CharacterStats>());
                tryToShowStatsCard = false;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (SS.GameController.NoInteractableIfObjectsAreActive.noInteract == null || SS.GameController.NoInteractableIfObjectsAreActive.noInteract.CanInteract())
            {
                if (eventData.button == PointerEventData.InputButton.Right)
                {
                    ShowRange(eventData);
                }

                //There shouldn't need to be anything in here that deactivate the stats card, that's handled in CastingTile
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    tryToShowStatsCard = true;
                }
            }
        }

        public List<Target> GetTargets()
        {
            targets = SS.Util.GetOnlyTargets.GetTargets(Physics2D.OverlapBoxAll(transform.position, new Vector2(.5f, .5f), 0));

            return targets;
        }



        public void OnPointerEnter(PointerEventData eventData)
        {
        }

        public void OnPointerExit(PointerEventData eventData)
        {
        }

        public void ShowRange(PointerEventData eventData)
        {

            if (eventData.button == PointerEventData.InputButton.Right && GameController.TurnManager.instance.CurrentTurnTaker.tag == "Player")
            {
                if (Tutorial.TutorialHandler.rangeWasShown != null)
                {
                    Tutorial.TutorialHandler.rangeWasShown.Set(true);
                }

                if (!showing)
                {
                    if (stats.GetComponent<AI.Agent>() != null)
                    {
                        AI.Agent agent = stats.GetComponent<AI.Agent>();

                        if (agent.spells.Count > 0)
                        {
                            stats.ShowRangeOfAbilities(agent.spells[0]);

                            GameController.TurnManager.instance.CurrentTurnTaker.GetComponent<PlayerMovement.SS_PlayerController>().PauseMovement_RangeShown();
                        }
                        else
                        {
                            stats.ShowRangeOfAbilities(null);
                        }

                        showing = true;
                    }
                    else
                    {
                        stats.ShowRangeOfAbilities(null);
                        showing = true;
                    }
                }
                else
                {
                    GameController.TurnManager.instance.CurrentTurnTaker.GetComponent<PlayerMovement.SS_PlayerController>().UnPauseMovement_RangeShown();

                    if (GameController.TurnManager.instance.CurrentTurnTaker.tag == "Player")
                    {
                        GameController.TurnManager.instance.CurrentTurnTaker.GetComponent<PlayerMovement.SS_PlayerMoveRange>().SpawnRange("Character Follower, ShowRange");
                        showing = false;
                    }
                }
            }
        }
    }
}
