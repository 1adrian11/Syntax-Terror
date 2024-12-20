using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Destroy_score))]
public class MeteorControll : MonoBehaviour
{
    [field: SerializeField]
    public float RotationSpeed {get; private set;}  // forgás sebessége
    [field: SerializeField]
    public Vector2 Speed {get; private set;} // mozgás sebessége
    [field: SerializeField]
    //private ShipSpawner Cont;
    public MeteorControll SpawnAfterKill {get; private set;}

    /*public static MeteorControll Spawn(MeteorControll i, float RotSpeed, Vector2 speed1, ShipSpawner Controller){
        MeteorControll meteor = Instantiate(i);
        meteor.GetComponent<Destroy_score>().Controller = Controller;
        meteor.RotationSpeed = RotSpeed;
        meteor.Speed = speed1;
        return meteor;
    }*/
    public static MeteorControll Spawn(MeteorControll i, float RotSpeed, Vector2 speed1, ShipSpawner Controller) {
        MeteorControll meteor = Instantiate(i);
        meteor.GetComponent<Destroy_score>().Controller = Controller;
        meteor.RotationSpeed = RotSpeed;
        meteor.Speed = speed1;
        return meteor;
    }

    void Update(){
        RotMeteor();
        MovMeteor();
    }

    /*public void OnHit(ShootControll bullet, Destroy_score dest) { // UJ METEOROK PONTSZÁMÁT NEM SZÁMOLJA
        //Destroy(bullet.gameObject);

        if (SpawnAfterKill != null){

            //MeteorControll i = Spawn(ObjectRemove, RotationSpeed, Speed, GetComponent<Destroy_score>().Controller = Controller);
            //i.transofr.position = this.transform.position;
            MeteorControll meteor1 = Instantiate(SpawnAfterKill);
            meteor1.transform.position = this.transform.position;
            meteor1.RotationSpeed = RotationSpeed;
            meteor1.Speed = Speed; // eredeti sebesség

            MeteorControll meteor2 = Instantiate(SpawnAfterKill);
            meteor2.transform.position = this.transform.position;
            meteor2.RotationSpeed = RotationSpeed;
            meteor2.Speed = new Vector2(-Speed.x, Speed.y); // X koordináta negált 

        }
        dest.ObjectRemove(bullet);
        //Destroy(this.gameObject); // az eredeti meteor megsemmisítése
    }*/
    public void OnHit(ShootControll bullet, Destroy_score dest) {
        //Destroy(bullet.gameObject);

        if (SpawnAfterKill != null){
            // Az új meteorokat a Spawn metódussal hozzuk létre
            MeteorControll meteor1 = MeteorControll.Spawn(SpawnAfterKill, RotationSpeed, Speed, dest.Controller);
            meteor1.transform.position = this.transform.position;
            
            // ugyanaz negált iránnyal
            MeteorControll meteor2 = MeteorControll.Spawn(SpawnAfterKill, RotationSpeed, new Vector2(-Speed.x, Speed.y), dest.Controller);
            meteor2.transform.position = this.transform.position;
        }

        // Eredeti meteor eltávolítása és pontszám frissítése
        dest.ObjectRemove(bullet);
    }

    private void RotMeteor(){
        float rot_z = transform.rotation.eulerAngles.z + (RotationSpeed * Time.deltaTime);
        Vector3 rot_r = new Vector3(0, 0, rot_z);
        transform.rotation = Quaternion.Euler(rot_r);
    }

    public void MovMeteor () {
        float new_x = transform.position.x + (Speed.x * Time.deltaTime);
        float new_y = transform.position.y + (Speed.y * Time.deltaTime);
        transform.position = new Vector2(new_x, new_y);
    }
}