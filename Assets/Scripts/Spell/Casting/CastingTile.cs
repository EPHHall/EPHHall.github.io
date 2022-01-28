using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using SS.UI;

namespace SS.Spells
{
    public class CastingTile : Tile, IPointerEnterHandler, IPointerExitHandler
    {
        public static bool PointerOverAtLeastOne;
        public static bool PointerWasOverAtLeastOne;
        private static bool CheckToDeactivateAll;

        public Color defaultColor;
        public Color highlightColor;
        public Color selectedColor;

        public bool mouseIsOver;
        public bool underFollower;
        public static List<CastingTile> selectedTiles = new List<CastingTile>();
        public int maxSelections = 1;

        [SerializeField]
        private List<Target> targets;

        UnityEvent selectedTilesChange;

        public StatsCard statsCard;

        public TileResources resources;

        // Start is called before the first frame update
        void Start()
        {
            selectedTilesChange = new UnityEvent();

            resources = GameObject.FindObjectOfType<TileResources>();

            SS.UI.TargetMenu targetMenu = GameObject.FindObjectOfType<SS.UI.TargetMenu>();
            if (targetMenu != null)
            {
                selectedTilesChange.AddListener(targetMenu.OnTargetTilesChange);
            }

            selectedTilesChange.AddListener(Target.ClearSelectedTargets);

            statsCard = resources.statsCard;
        }

        // Update is called once per frame
        void Update()
        {
            //Setting these up for LateUpdate
            CheckToDeactivateAll = true;
            PointerWasOverAtLeastOne = false;

            if (mouseIsOver)
            {
                PointerOverAtLeastOne = true;

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (selectedTiles.Contains(this))
                    {
                        DeselectTile();
                    }
                    else
                    {
                        SelectTile();
                    }
                }

                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    if (selectedTiles.Contains(this))
                    { }
                    else
                    {
                        SelectTile();
                    }
                }
            }
        }

        private void LateUpdate()
        {
            //this should only need to run once since Pointer... is static, so resetting the variable shouldn't be an issue
            if (CheckToDeactivateAll && !PointerOverAtLeastOne && Input.GetKeyDown(KeyCode.Mouse0) && !statsCard.pointerIsOver)
            {
                while (selectedTiles.Count > 0)
                {
                    selectedTiles[0].DeselectTile();
                }

                statsCard.DeactivateStatsCard();
            }

            if (PointerOverAtLeastOne)
            {
                PointerWasOverAtLeastOne = true;
            }

            PointerOverAtLeastOne = false;
            CheckToDeactivateAll = false;
        }

        public virtual void SelectTile()
        {
            while (selectedTiles.Count >= maxSelections)
            {
                DeselectTile(selectedTiles[0]);
            }

            selectedTiles.Add(this);
            GetComponent<SpriteRenderer>().color = selectedColor;

            selectedTilesChange.Invoke();

            statsCard.ActivateStatsCard(transform.position.x, GetTargets(), targets[0].GetComponent<Character.CharacterStats>());
        }

        public List<Target> GetTargets()
        {
            //List<Target> targets = SS.Util.GetOnlyTargets.GetTargets(Physics2D.OverlapBoxAll(transform.position, new Vector2(.5f, .5f), 0));
            targets = SS.Util.GetOnlyTargets.GetTargets(Physics2D.OverlapBoxAll(transform.position, new Vector2(.5f, .5f), 0));



            return targets;
        }
        public List<Target> GetObstacles()
        {
            //List<Target> targets = SS.Util.GetOnlyTargets.GetTargets(Physics2D.OverlapBoxAll(transform.position, new Vector2(.5f, .5f), 0));
            targets = SS.Util.GetOnlyTargets.GetTargets(Physics2D.OverlapBoxAll(transform.position, new Vector2(.5f, .5f), 0));

            List<Target> obstacles = new List<Target>();
            foreach (Target target in targets)
            {
                if (target.targetType.creature || target.targetType.obj)
                {
                    obstacles.Add(target);
                }
            }

            return obstacles;
        }
        public bool GetFollowers()
        {
            return SS.Util.GetFollowers.TargetsPresent(Physics2D.OverlapBoxAll(transform.position, new Vector2(.5f, .5f), 0));
        }

        public virtual void DeselectTile()
        {
            selectedTiles.Remove(this);
            foreach (Target target in GetTargets())
            {
                if (statsCard.statsToDisplay == target.GetComponent<Character.CharacterStats>())
                {
                    statsCard.DeactivateStatsCard();
                }
            }

            GetComponent<SpriteRenderer>().color = defaultColor;

            selectedTilesChange.Invoke();
        }
        public void DeselectTile(CastingTile toRemove)
        {
            selectedTilesChange.Invoke();
            selectedTiles.Remove(toRemove);

            foreach (Target target in toRemove.GetTargets())
            {
                if (toRemove.statsCard.statsToDisplay == target.GetComponent<Character.CharacterStats>())
                {
                    toRemove.statsCard.DeactivateStatsCard();
                }
            }

            if (toRemove != null)
                toRemove.GetComponent<SpriteRenderer>().color = defaultColor;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            //if (col.GetComponent<Target>() != null)
            //{
            //    targets.Add(col.GetComponent<Target>());
            //}
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            //if (col.GetComponent<Target>() != null)
            //{
            //    targets.Remove(col.GetComponent<Target>());
            //}
        }

        private void OnMouseOver()
        {
        }

        private void OnMouseExit()
        {
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void PointerEntered()
        {
            if (!selectedTiles.Contains(this))
            {
                GetComponent<SpriteRenderer>().color = highlightColor;
            }

            underFollower = false;
            if (GetFollowers())
            {
                underFollower = true;
            }

            mouseIsOver = true;
        }

        public void PointerExited()
        {
            if (!selectedTiles.Contains(this))
            {
                GetComponent<SpriteRenderer>().color = defaultColor;
            }
            mouseIsOver = false;
        }
    }
}
