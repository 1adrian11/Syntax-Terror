using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Movement : MonoBehaviour
{
    public float speed;
    public float BoostDuration;  //mennyi ideig legyen immunis pawn utan
    public Vector2 velocity = new (0,0); //hajo helyzet
    public Vector2 maxpos, minpos; // max/min pozicio (ne menjen ki a kepbol)
    public Transform MainWeapon;
    public GameObject BulletType;
    public PlayerSpawner PlayerSpawner;

    void Update(){
        Movement();
        Fire();
        Boost();
        Moveship();
    }

    private void Fire (){
        if(Input.GetButtonDown("Fire1")) {
            GameObject bullet = Instantiate(BulletType);
            bullet.transform.position = MainWeapon.position;
        }
    }

    private void Movement(){
        velocity = HorizontalPos(Input.GetAxis("Horizontal"));  // x koordinata update
        velocity += VerticalPos(Input.GetAxis("Vertical"));      // y koo update, += kell, mert felulirna az elozot es csak vertical menne
        // IMPUTBAN FUNTOS, HOGY PONTOSAN UGYANAZ LEGYEN MEGADVA STRINGNEK, MINT ITT
    }

    private Vector2 VerticalPos(float ver){
        return new Vector2(0, Mathf.Clamp(ver, -1, 1));  //elotte 0 ignoralja az x koordinatat
    }

    private Vector2 HorizontalPos(float hor){
        return new Vector2(Mathf.Clamp(hor, -1, 1),0); //soft move, utana levo nullaval ignoralja az y koordinatat
        /*  durvabb move
        if(hor < 0) {
            return new Vector2(-1,0);
        }
        else if (hor > 0){
            return new Vector2(1,0);
        }
        return new Vector2(0,0);
        */
    }
    
    private void Moveship(){
        float new_x = transform.position.x + (velocity.x*speed*Time.deltaTime);  //mozgas
        float new_y = transform.position.y + (velocity.y*speed*Time.deltaTime);
        new_x = Mathf.Clamp(new_x, minpos.x, maxpos.x);  //ne menjen ki a kepbol
        new_y = Mathf.Clamp(new_y, minpos.y, maxpos.y);
        transform.position = new Vector2(new_x, new_y);
    }

    //utkozes
    private void OnTriggerEnter2D(Collider2D other) {
        MeteorControll i = other.GetComponent<MeteorControll>();
        if(i != null && BoostDuration <= 0) {  //boostnal ne destroyoljon (spawn utan x masodpercig)
            PlayerSpawner.DestroyMark(this);

        }
    }

    private void Boost (){
        BoostDuration -= Time.deltaTime; //idovel jarjon le a boost
        BoostDuration = Mathf.Max(0,BoostDuration);
    }
}