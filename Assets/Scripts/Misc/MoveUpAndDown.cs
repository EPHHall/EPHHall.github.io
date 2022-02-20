using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveUpAndDown : MonoBehaviour
{
    public float speed;
    public Vector2 startPoint;
    public Vector2 endPoint;
    public float threshold;

    public RectTransform viewport;
    public RectTransform content;

    float timer;
    public bool startToEnd;
    private Image image;

    private void Start()
    {
        timer = 0;

        image = GetComponent<Image>();
    }

    void Update()
    {
        //Debug.Log("Content height: " + content.GetComponent<VerticalLayoutGroup>().preferredHeight + "; Viewport height: " + viewport.rect.height);
        //Debug.Log("If value: " + (content.GetComponent<VerticalLayoutGroup>().preferredHeight - viewport.rect.height));

        if (content.GetComponent<VerticalLayoutGroup>().preferredHeight - viewport.rect.height >= threshold)
        {
            image.enabled = true;

            float workingSpeed = speed / 100;

            transform.localPosition = Vector2.Lerp(startPoint, endPoint, timer);

            if (startToEnd)
            {
                timer += workingSpeed * Time.deltaTime;

                if (timer > 1)
                {
                    timer = 1;
                    startToEnd = false;
                }
            }
            else
            {
                timer -= workingSpeed * Time.deltaTime;

                if (timer < 0)
                {
                    timer = 0;
                    startToEnd = true;
                }
            }
        }
        else
        {
            image.enabled = false;
        }
    }
}
