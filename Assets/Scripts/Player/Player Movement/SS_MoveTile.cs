using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.PlayerMovement
{
    public class SS_MoveTile : Tile
    {
        public Color defaultColor;
        public Color highlightColor;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnMouseOver()
        {
            GetComponent<SpriteRenderer>().color = highlightColor;
        }

        private void OnMouseExit()
        {
            GetComponent<SpriteRenderer>().color = defaultColor;
        }
    }
}
