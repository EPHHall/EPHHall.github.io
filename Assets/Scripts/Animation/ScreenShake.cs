using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Animation
{
    public class ScreenShake : MonoBehaviour
    {
        public bool activate;
        public float speed;
        public float maxRange;
        public float minRange;
        public int timesToComplete = 2;

        [Space (15)]
        [Header("Debug")]
        public Vector2 point1;
        public Vector2 point2;
        public Vector2 originalPos;
        public Vector2 lastPoint;
        public bool shake;
        public float t;
        public bool point1To2;
        public bool point2To1;
        public int timesCompleted;

        private void Start()
        {
            originalPos = transform.position;
        }

        private void Update()
        {
            if (activate)
            {
                activate = false;

                Run();
            }

            if (shake)
            {
                if (timesCompleted < timesToComplete)
                {
                    if (point1To2)//Then this second
                    {
                        transform.position = Vector2.Lerp(point1, point2, t);
                        if (t == 1)
                        {
                            point2To1 = true;
                            point1To2 = false;
                            t = 0;
                            timesCompleted++;
                        }
                    }
                    else if (point2To1)//Then this third
                    {
                        transform.position = Vector2.Lerp(point2, point1, t);
                        if (t == 1)
                        {
                            point1To2 = true;
                            point2To1 = false;
                            t = 0;
                            timesCompleted++;
                        }
                    }
                    else //This should run first
                    {
                        transform.position = Vector2.Lerp(originalPos, point1, t);
                        if (t == 1)
                        {
                            point1To2 = true;
                            t = 0;
                        }
                    }

                    lastPoint = transform.position;
                }
                else//Then this last
                {
                    transform.position = Vector2.Lerp(lastPoint, originalPos, t);
                    if (t == 1)
                    {
                        shake = false;
                        transform.position = originalPos;
                    }
                }

                transform.position = new Vector3(transform.position.x, transform.position.y, -10);

                t += Time.deltaTime * speed;
                t = Mathf.Clamp(t, 0, 1);
            }
            else
            {
                originalPos = transform.position;
            }
        }

        public void Run()
        {
            if (shake) return;

            float point1XOffset = Random.Range(minRange, maxRange);
            if (Random.Range(-1, 1) >= 0) point1XOffset *= -1;

            float point1YOffset = Random.Range(minRange, maxRange);
            if (Random.Range(-1, 1) >= 0) point1YOffset *= -1;

            point1 = new Vector2(point1XOffset + originalPos.x, point1YOffset + originalPos.y);

            float point2XOffset = -point1XOffset;
            float point2YOffset = -point1YOffset;
            point2 = new Vector2(point2XOffset + originalPos.x, point2YOffset + originalPos.y);

            shake = true;
            t = 0;
            point1To2 = false;
            point2To1 = false;
            timesCompleted = 0;
            lastPoint = Vector2.negativeInfinity;
        }
    }
}
