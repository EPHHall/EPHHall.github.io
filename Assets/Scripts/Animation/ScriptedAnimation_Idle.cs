using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedAnimation_Idle : MonoBehaviour
{
    public Sprite sprite2;
    public float frequency;

    private Sprite sprite1;
    private SpriteRenderer spriteRenderer;
    private bool reverse;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite1 = spriteRenderer.sprite;
        StartCoroutine(SwitchSprites());
    }

    public IEnumerator SwitchSprites()
    {
        while (true)
        {
            if(spriteRenderer.sprite == sprite1)
            {
                spriteRenderer.sprite = sprite2;
            }
            else
            {
                spriteRenderer.sprite = sprite1;
            }

            yield return new WaitForSeconds(frequency);
        }
    }
}
