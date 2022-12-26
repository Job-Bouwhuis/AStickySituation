using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public float speed;
    public int startingPoint;
    public Transform[] points;
    public Vector2 translation = Vector2.zero;

    private int i;

    bool canMove = false;

    void Start()
    {
        transform.position = points[startingPoint].position;
        StartCoroutine(randomWait());

        IEnumerator randomWait()
        {
            yield return new WaitForSeconds(Random.value);
            canMove = true;
        }
    }


    void Update()
    {
        if (!canMove) return;
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }
        transform.position = translation = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }
}
