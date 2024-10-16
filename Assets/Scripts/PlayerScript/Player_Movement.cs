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
    [field: SerializeField]
    public float speed {get; private set;}
    [field: SerializeField]
    public float BoostDuration {get; private set;}  //mennyi ideig legyen immunis pawn utan
    [field: SerializeField]
    public Vector2 velocity {get; private set;} = new (0,0); //hajo helyzet
    [field: SerializeField]
    public Vector2 maxpos {get; private set;} 
    [field: SerializeField]
    public Vector2 minpos {get; private set;} // max/min pozicio (ne menjen ki a kepbol)
    [field: SerializeField]
    public Transform MainWeapon {get; private set;}
        [field: SerializeField]
    public Transform MainWeapon2 {get; private set;}
        [field: SerializeField]
    public Transform MainWeapon3 {get; private set;}
    [field: SerializeField]
    public GameObject BulletType {get; private set;}
    [field: SerializeField]
    public PlayerSpawner PlayerSpawner {get; private set;}

    public static Player_Movement Spawn(Player_Movement pm, PlayerSpawner i){
        Player_Movement pl = Instantiate(pm);
        pl.PlayerSpawner = i;
        return pl;
    }
    void Update(){
        Movement();
        Fire();
        Boost();
        Moveship();
    }

    private void Fire() {
        if (Input.GetButtonDown("Fire1")) {
            // összes fegyver
            List<Transform> weapons = new List<Transform> { MainWeapon, MainWeapon2, MainWeapon3 };

            // Minden fegyverhez külön lövedék
            foreach (Transform weapon in weapons) {
                if (weapon != null) {
                    GameObject bullet = Instantiate(BulletType);
                    bullet.transform.position = weapon.position;

                    // Lövedék dőlése = hajó aktuális dőlése
                    bullet.transform.rotation = transform.rotation;

                    ShootControll bulletScript = bullet.GetComponent<ShootControll>();
                    if (bulletScript != null) {
                        // Sebesség a lövedék irányához (szögéhez) képest
                        Vector2 direction = bullet.transform.up;
                        bulletScript.SetSpeed(direction * bulletScript.GetSpeedMagnitude());
                    }
                }
            }
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
        // Mozgatás
        float new_x = transform.position.x + (velocity.x * speed * Time.deltaTime);
        float new_y = transform.position.y + (velocity.y * speed * Time.deltaTime);
        new_x = Mathf.Clamp(new_x, minpos.x, maxpos.x);
        new_y = Mathf.Clamp(new_y, minpos.y, maxpos.y);
        transform.position = new Vector2(new_x, new_y);
    
        if (velocity.x != 0) {
            // Dőlés szög meghatározás
            float tiltAngle = Mathf.Lerp(0, 50, Mathf.Abs(velocity.x));  // x foknyi döntés
            tiltAngle *= Mathf.Sign(velocity.x);
        
            Quaternion targetRotation = Quaternion.Euler(0, 0, -tiltAngle);  // Negatív szög a jobbra dőléshez
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);  // Fokozatos dőlés
        } 
        else {
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);  // Alaphelyzet
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);  // Lassan térjen vissza
        }
    }

    //utkozes
    private void OnTriggerEnter2D(Collider2D other) {
        MeteorControll i = other.GetComponent<MeteorControll>();
        if(i != null && BoostDuration <= 0) {  //boostnal ne destroyoljon (spawn utan x masodpercig)
            PlayerSpawner.DestroyMark(this);

        }
    }

    private void Boost() {
        // Ellenőrizzük, hogy van-e Renderer komponens
        Renderer renderer = this.GetComponent<Renderer>();

        if (BoostDuration > 0) {
            // elso f-es szamok a szin, masodik helyen a szamok a villogas intenzitas
            float alpha = Mathf.Lerp(0.05f, 1f, Mathf.PingPong(Time.time * 4, 1)); 

            Color color = renderer.material.color;
            color.a = alpha;
            renderer.material.color = color;
        } 
        else {
            // Boost után, eredeti alpha érték
            Color color = renderer.material.color;
            color.a = 1f; // teljesen átlátszatlan
            renderer.material.color = color;
        }

        BoostDuration -= Time.deltaTime; // Idővel járjon le a boost
        BoostDuration = Mathf.Max(0, BoostDuration);
    }
}