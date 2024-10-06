using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class MeteorSpawner : MonoBehaviour
{
    public float spawnrate;  //milyen gyorsan spawnol
    public Transform spawnpoint;
    public float lastspwan;
    public float minrot, maxrot;  //forgas gyorsasaga
    public Vector2 minspeed, maxspeed; 
    public MeteorControll Template;
    void Start()
    {
        lastspwan = Time.time;
    }

    void Update()
    {
        if (ready())
        {
            spawn();
        }
    }

    private void spawn(){
        MeteorControll ac = Instantiate(Template);
        ac.transform.position = spawnpoint.position;
        ac.RotationSpeed = Random.Range(minrot, maxrot);
        ac.Speed = new (Random.Range(minspeed.x, maxspeed.x), Random.Range(minspeed.y, maxspeed.y));  //milyen gyorsan jojjenek 
        lastspwan = Time.time;
    }

    private bool ready(){
        return Time.time > (lastspwan + spawnrate);
    }
}
