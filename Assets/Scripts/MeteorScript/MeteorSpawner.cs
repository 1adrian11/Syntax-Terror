using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class MeteorSpawner : MonoBehaviour
{
    [field: SerializeField]
    public float spawnrate {get; set;}  // milyen gyorsan spawnol
    [field: SerializeField]
    public float lastspawn {get; private set;}
    [field: SerializeField]
    public float minrot {get; private set;} = -90;
    [field: SerializeField]
    public float maxrot {get; private set;} = 90;  // forgás gyorsasága

    [field: SerializeField]
    public Transform minspawnpoint {get; private set;}
    [field: SerializeField]
    public Transform maxspawnpoint {get; private set;}

    [field: SerializeField]
    public Vector2 minspeed  {get; private set;}
    [field: SerializeField]
    public Vector2 maxspeed {get; private set;}

    public Vector2 minspawnvector => minspawnpoint.position;
    public Vector2 maxspawnvector => maxspawnpoint.position;

    [field: SerializeField]
    public MeteorControll Template1 {get; private set;}
    [field: SerializeField]
    public MeteorControll Template2 {get; private set;}
    [field: SerializeField]
    public MeteorControll Template3 {get; private set;}
    [field: SerializeField]
    public MeteorControll Template4 {get; private set;}

    [field: SerializeField]
    public ShipSpawner Controll {get; private set;}

    private List<MeteorControll> templates; // List to hold templates

    void Start()
    {
        lastspawn = Time.time;

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
        // random template valasztas
        MeteorControll selectedTemplate = templates[Random.Range(0, templates.Count)];

        float RotationSpeed = Random.Range(minrot, maxrot);

        Vector2 Speed = new (Random.Range(minspeed.x, maxspeed.x), Random.Range(minspeed.y, maxspeed.y));  // Milyen gyorsan jöjjenek 
        MeteorControll mc = MeteorControll.Spawn(selectedTemplate, RotationSpeed, Speed, Controll);

        Vector2 spawnpoint = new Vector2(Random.Range(minspawnpoint.position.x, maxspawnpoint.position.x), minspawnpoint.position.y);
        mc.transform.position = spawnpoint;
        lastspawn = Time.time;
    }

    private bool ready()
    {
        return Time.time > (lastspawn + spawnrate);
    }
}