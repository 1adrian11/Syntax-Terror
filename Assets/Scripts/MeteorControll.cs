using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MeteorControll : MonoBehaviour
{
    public float RotationSpeed;  //forgas sebessege
    public Vector2 Speed; //mozgas

    void Update(){
        RotMeteor();
        MovMeteor();
    }

    private void RotMeteor(){
        float rot_z = transform.rotation.eulerAngles.z + (RotationSpeed * Time.deltaTime);
        Vector3 rot_r = new (0, 0, rot_z);
        transform.rotation = Quaternion.Euler(rot_r);
    }

    public void MovMeteor (){
        float new_x = transform.position.x + (Speed.x*Time.deltaTime);
        float new_y = transform.position.y + (Speed.y*Time.deltaTime);
        transform.position = new Vector2(new_x, new_y);
    }
}
