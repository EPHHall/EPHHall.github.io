using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.Spells;

namespace SS.UI
{
    public class TargetMenu : MonoBehaviour
    {
        public GameObject onOff;
        public List<Target> targets;
        public List<CastingTile> activeTiles;

        public enum ActiveType
        {
            All, Creature, Object, Weapon, Tile
        }
        public ActiveType activeType;
        public int activeTypeIndex = 0;
        public TargetType toDisplay;

        public RectTransform contentPane;
        public GameObject targetButton;

        public Text currentTypeText;

        public void Start()
        {
            DeactivateMenu();

            activeType = (ActiveType)activeTypeIndex;
            SetToDisplay();
        }

        public void ChangeActiveType(bool positive)
        {
            if (positive)
                activeTypeIndex++;
            else
                activeTypeIndex--;

            if (activeTypeIndex >= System.Enum.GetNames(typeof(ActiveType)).Length)
            {
                activeTypeIndex = 0;
            }
            else if (activeTypeIndex < 0)
            {
                activeTypeIndex = System.Enum.GetNames(typeof(ActiveType)).Length - 1;
            }

            currentTypeText.text = System.Enum.GetNames(typeof(ActiveType))[activeTypeIndex];
            activeType = (ActiveType)activeTypeIndex;
            FillTargetsList();
        }

        public void SetToDisplay()
        {
            toDisplay = new TargetType(false);

            switch (activeType)
            {
                case ActiveType.All:
                    toDisplay = new TargetType(true);
                    break;
                case ActiveType.Creature:
                    toDisplay.creature = true;
                    break;
                case ActiveType.Object:
                    toDisplay.obj = true;
                    break;
                case ActiveType.Tile:
                    toDisplay.tile = true;
                    break;
                case ActiveType.Weapon:
                    toDisplay.weapon = true;
                    break;
            }
        }

        public void ActivateMenu()
        {
            onOff.SetActive(true);

            FillTargetsList();
        }

        public void DeactivateMenu()
        {
            targets.Clear();

            onOff.SetActive(false);
        }

        public void FillTargetsList()
        {
            targets.Clear();
            activeTiles.Clear();

            foreach (CastingTile tile in CastingTile.selectedTiles)
            {
                activeTiles.Add(tile);

                foreach (Target target in tile.GetTargets())
                {
                    targets.Add(target);
                }

                if(tile != null && tile.GetComponent<Target>() != null)
                    targets.Add(tile.GetComponent<Target>());
            }

            SpawnTargetButtons();
        }

        public void SpawnTargetButtons()
        {
            SetToDisplay();

            foreach (TargetButton button in GameObject.FindObjectsOfType<TargetButton>())
            {
                Destroy(button.gameObject);
            }

            int i = 0;
            foreach (Target target in targets)
            {
                if (target.targetType.DoesGTETOneTypeMatch(toDisplay))
                {
                    GameObject newButton = Instantiate(targetButton, contentPane);
                    newButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 51.5f + (-11*i));
                    newButton.GetComponent<RectTransform>().rotation = Quaternion.identity;

                    newButton.GetComponent<TargetButton>().Start();
                    newButton.GetComponent<TargetButton>().SetAssociatedTarget(target);

                    i++;
                }
            }
        }

        //This event can run twice, one for the tile being selected and one for the tile being deselected.
        public void OnTargetTilesChange()
        {
            FillTargetsList();
        }
    }
}
