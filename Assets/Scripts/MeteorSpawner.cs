using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class MeteorSpawner : MonoBehaviour
{
    public float spawnrate;  // milyen gyorsan spawnol
    public float lastspwan;
    public float minrot, maxrot;  // forgás gyorsasága

    public Transform minspawnpoint, maxspawnpoint;

    public Vector2 minspeed, maxspeed; 
    public Vector2 minspawnvector => minspawnpoint.position;
    public Vector2 maxspawnvector => maxspawnpoint.position;

    public MeteorControll Template1, Template2, Template3, Template4;

    private List<MeteorControll> templates; // List to hold templates

    void Start()
    {
        lastspwan = Time.time;

        // Add templates to the list
        templates = new List<MeteorControll> { Template1, Template2, Template3, Template4 };
    }

    void Update()
    {
        if (ready())
        {
            spawn();
        }
    }

    private void spawn()
    {
        // Select a random template
        MeteorControll selectedTemplate = templates[Random.Range(0, templates.Count)];
        
        // Instantiate the selected template
        MeteorControll ac = Instantiate(selectedTemplate);
        
        Vector2 spawnpoint = new Vector2(Random.Range(minspawnpoint.position.x, maxspawnpoint.position.x), minspawnpoint.position.y);
        
        // 2 pont között random megjelenés
        ac.transform.position = spawnpoint;
        ac.RotationSpeed = Random.Range(minrot, maxrot);
        ac.Speed = new Vector2(Random.Range(minspeed.x, maxspeed.x), Random.Range(minspeed.y, maxspeed.y));  // Milyen gyorsan jöjjenek 
        
        lastspwan = Time.time;
    }

    private bool ready()
    {
        return Time.time > (lastspwan + spawnrate);
    }
}