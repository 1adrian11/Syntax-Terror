using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using TMPro;

public class Player_Movement : MonoBehaviour
{
    [field: SerializeField]
    public float speed { get; private set; }
    [field: SerializeField]
    public UnityEvent<Player_Movement> IfChange { get; private set; }
    [field: SerializeField]
    public float BoostDuration { get; private set; }
    [field: SerializeField]
    public Vector2 velocity { get; private set; } = new(0, 0);
    [field: SerializeField]
    public Vector2 maxpos { get; private set; }
    [field: SerializeField]
    public Vector2 minpos { get; private set; }
    [field: SerializeField]
    public Transform MainWeapon { get; private set; }
    [field: SerializeField]
    public Transform MainWeapon2 { get; private set; }
    [field: SerializeField]
    public Transform MainWeapon3 { get; private set; }
    [field: SerializeField]
    public GameObject BulletType { get; private set; }
    [field: SerializeField]
    public ShipSpawner PlayerSpawner { get; private set; }
    [field: SerializeField]
    public float DmgBoost { get; private set; }
    public bool HasDmgBoost => DmgBoost > 0;
    public bool Visible => !HasDmgBoost || Mathf.Sin(Time.time * 10) > 0;
    [field: SerializeField]
    private int shieldcount = 0;
    
    // UI Text referencia a képernyőn való megjelenítéshez
    [SerializeField] private TextMeshProUGUI shieldCountText; 

    public int Shieldpow
    {
        get => shieldcount;
        set
        {
            shieldcount = Mathf.Min(value, 3); // max 3 shield lehet
            IfChange.Invoke(this);
            UpdateShieldCountDisplay();  // Frissíti a képernyőn megjelenő értéket
        }
    }

    void Start(){
        IfChange.Invoke(this);
          if (shieldCountText == null)
        {
            shieldCountText = GameObject.Find("ShieldCountText")?.GetComponent<TextMeshProUGUI>();
        }

        // Egyéb inicializálás
        IfChange.Invoke(this);
    }

    /*public static Player_Movement Spawn(Player_Movement pm, ShipSpawner i){
        Player_Movement pl = Instantiate(pm);
        pl.PlayerSpawner = i;
        return pl;
    }*/
    public static Player_Movement Spawn(Player_Movement pm, ShipSpawner i){
        Player_Movement pl = Instantiate(pm);
        pl.PlayerSpawner = i;
        return pl;
    }
    // A Shieldcount értékének frissítése a képernyőn
    private void UpdateShieldCountDisplay()
    {
        if (shieldCountText != null)
        {
            shieldCountText.text = "Shield: " + shieldcount.ToString();  // A Shieldcount értéke a szövegben
        }
    }

    void Update()
    {
        // Ha van hozzá rendelt Text, frissítsük a shieldcount értéket
        if (shieldCountText != null)
        {
            shieldCountText.text = Shieldpow.ToString();
        }
        Movement();
        Fire();
        Boost();
        Move();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            List<Transform> weaponSpawnpoint = new List<Transform> { MainWeapon, MainWeapon2, MainWeapon3 };

            foreach (Transform weapon in weaponSpawnpoint)
            {
                if (weapon != null)
                {
                    GameObject bullet = Instantiate(BulletType);
                    bullet.transform.position = weapon.position;
                    bullet.transform.rotation = transform.rotation;

                    ShootControll bulletScript = bullet.GetComponent<ShootControll>();
                    if (bulletScript != null)
                    {
                        Vector2 direction = bullet.transform.up;
                        bulletScript.SetSpeed(direction * bulletScript.GetSpeedMagnitude());
                    }
                }
            }
        }
    }

    private void Movement()
    {
        velocity = HorizontalPos(Input.GetAxis("Horizontal"));
        velocity += VerticalPos(Input.GetAxis("Vertical"));
    }

    private Vector2 VerticalPos(float ver)
    {
        return new Vector2(0, Mathf.Clamp(ver, -1, 1));
    }

    private Vector2 HorizontalPos(float hor)
    {
        return new Vector2(Mathf.Clamp(hor, -1, 1), 0);
    }

    private void Move()
    {
        float new_x = transform.position.x + (velocity.x * speed * Time.deltaTime);
        float new_y = transform.position.y + (velocity.y * speed * Time.deltaTime);
        new_x = Mathf.Clamp(new_x, minpos.x, maxpos.x);
        new_y = Mathf.Clamp(new_y, minpos.y, maxpos.y);
        transform.position = new Vector2(new_x, new_y);

        if (velocity.x != 0)
        {
            float tiltAngle = Mathf.Lerp(0, 50, Mathf.Abs(velocity.x));
            tiltAngle *= Mathf.Sign(velocity.x);

            Quaternion targetRotation = Quaternion.Euler(0, 0, -tiltAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageToPlayer damage = other.GetComponent<DamageToPlayer>();
        if (damage != null && BoostDuration <= 0)
        {
            damage.Hit(this);
            if (Shieldpow <= 0)
            {
                PlayerSpawner.DestroyMark(this);
            }
            else
            {
                Shieldpow--;
            }
        }
    }

    private void Boost()
    {
        Renderer renderer = this.GetComponent<Renderer>();

        if (BoostDuration > 0)
        {
            float alpha = Mathf.Lerp(0.05f, 1f, Mathf.PingPong(Time.time * 4, 1));
            Color color = renderer.material.color;
            color.a = alpha;
            renderer.material.color = color;
        }
        else
        {
            Color color = renderer.material.color;
            color.a = 1f;
            renderer.material.color = color;
        }

        BoostDuration -= Time.deltaTime;
        BoostDuration = Mathf.Max(0, BoostDuration);
    }
}