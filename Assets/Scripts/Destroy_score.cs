using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Destroy_score : MonoBehaviour
{
    [field: SerializeField]
    public ShipSpawner Controller { get; set; } 

    [field: SerializeField]
    public int Score { get; private set; }

    [field: SerializeField]
    private UnityEvent<ShootControll, Destroy_score> Triggered { get; set; } // találatkor mi történjen

    [field: SerializeField]
    private UnityEvent<Destroy_score> Destroyed { get; set; } // droppoljon boostert

    private void OnTriggerEnter2D(Collider2D a)
    {
        ShootControll Bullet = a.GetComponent<ShootControll>();
        if (Bullet != null) // ha ez egy bullet
        {
            if (Triggered.GetPersistentEventCount() == 0)
            {
                // self destroy
                ObjectRemove(Bullet);
            }
            else
            {
                Triggered.Invoke(Bullet, this);
            }
        }
    }

    public void ObjectRemove(ShootControll bululet)
    {
        if (Controller != null)
        {
            Controller.ScoreManager(Score);
        }
        Destroy(bululet.gameObject);
        Destroyed.Invoke(this);
        Destroy(this.gameObject);
    }
}