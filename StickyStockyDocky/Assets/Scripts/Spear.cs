using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Spear : MonoBehaviour
{
    bool shouldCount = false;
    float time;
    float threshold = 2;
    [SerializeField] private new PolygonCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<PolygonCollider2D>();
    }
    private void Update()
    {
        if (!shouldCount)
            return;

        time += Time.deltaTime;
        if(time > threshold)
        {
            shouldCount = false;
            time = 0;
            Spikes.InvokePlayerDebuffDeactivate();
            collider.enabled = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Spikes.InvokePlayerDebuffActivate();
        collider.enabled = false;
    }
}
