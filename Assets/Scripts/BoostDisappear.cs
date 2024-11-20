using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BoostDisappear : MonoBehaviour
{
    public bool disappeard => TimeLeft <= 0;
    public bool shines => TimeLeft < 3;
    private SpriteRenderer rend;

    [field: SerializeField]
    public float TimeLeft { get; private set; }

    private Color originalColor;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        originalColor = rend.color; // Eredeti szín mentése
    }

    void Update()
    {
        TimeLeft -= Time.deltaTime;

        if (disappeard)
        {
            Destroy(this.gameObject);
        }
        else if (shines)
        {
            // Villogás szürküléssel
            float alpha = 0.5f + 0.5f * Mathf.Sin(Time.time * 10); // 0.0 és 1.0 között változó alpha
            rend.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        }
        else
        {
            // Alap szín visszaállítása, ha nem villog
            rend.color = originalColor;
        }
    }
}
