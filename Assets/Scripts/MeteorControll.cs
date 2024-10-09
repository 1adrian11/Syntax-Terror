using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MeteorControll : MonoBehaviour
{
    public float RotationSpeed;  // forgás sebessége
    public Vector2 Speed; // mozgás sebessége
    public MeteorControll SpawnAfterKill;

    void Update(){
        RotMeteor();
        MovMeteor();
    }

    private void OnTriggerEnter2D(Collider2D other) {    // lövedékkel ütközés
        ShootControll Bullet = other.GetComponent<ShootControll>();
        if(Bullet != null) {
            OnHit(Bullet);
        }
    }

    private void OnHit(ShootControll bullet) {
        Destroy(bullet.gameObject);

        if (SpawnAfterKill != null){
            MeteorControll meteor1 = Instantiate(SpawnAfterKill);
            meteor1.transform.position = this.transform.position;
            meteor1.RotationSpeed = RotationSpeed;
            meteor1.Speed = Speed; // eredeti sebesség

            MeteorControll meteor2 = Instantiate(SpawnAfterKill);
            meteor2.transform.position = this.transform.position;
            meteor2.RotationSpeed = RotationSpeed;
            meteor2.Speed = new Vector2(-Speed.x, Speed.y); // X koordináta negált

        }

        Destroy(this.gameObject); // az eredeti meteor megsemmisítése
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