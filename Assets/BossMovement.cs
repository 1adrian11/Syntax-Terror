using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossMovement : MonoBehaviour
{
    [field: SerializeField]
    public Transform BulletSpawn1 { get; private set; }
    [field: SerializeField]
    public Transform BulletSpawn2 { get; private set; }
    [field: SerializeField]
    public GameObject Bullet { get; private set; }
    [field: SerializeField]
    public float ShootFrequency { get; private set; } // hány másodpercenként lő
    [field: SerializeField]
    public float IfFire { get; private set; } = 0;

    public Transform midPoint; 
    public Transform leftPoint; 
    public Transform rightPoint; 
    public float speedToMidPoint = 2f; // Sebesség középig
    public float speedBetweenPoints = 1f; // Sebesség bal és jobb pont közt
    public float delayBeforeStart = 2f; // Késleltetés az első mozgás előtt
    public float delayAtPoints = 2f; // Várakozás a célpontokon

    private Transform currentTarget; // Az aktuális pont
    private bool movingToMidPoint = true;
    private float delayTimer; // Késleltetési időzítő
    public int BossHp;
    public int maxhp;
    private bool StartAttack = false;
    private bool isWaiting = false; // Jelzi, hogy várakozik-e
    public Hp_Bar HPBar;
    public GameObject ShowHPBar; //boss előtt ne jelenjen meg a hp bar
    public GameObject Show_ui; 
    public GameObject ShowWictory;
    public bool boss_hp_active = false;

    void Start()
    {
        ShowHPBar.gameObject.SetActive(false);
        HPBar.SetMaxHealth(BossHp);
        transform.position = new Vector2(0, 8); // Kezdő pozíció
        currentTarget = midPoint; // Kezdésként az első célpont
        delayTimer = delayBeforeStart; // Késleltetési időzítő
    }

    void Update()
    {
        if (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
        }
        else
        {
            MoveObject(); //időzítő után mozgás 
            if(boss_hp_active == false){
                ShowHPBar.SetActive(true);
                boss_hp_active = true;
            }
        }
        if (StartAttack == true)
        {
            Fire();
        }
        /*if(Time.time > delayBeforeStart && boss_hp_active == false) {
            ShowHPBar.SetActive(true); //boss hp mutatása
            boss_hp_active = true;
        }*/
    }

    void MoveObject()
    {
        if (isWaiting)
            return; // Ha várakozik, ne mozduljon tovább

        if (movingToMidPoint)
        {
            MoveTowardsTarget(midPoint, speedToMidPoint);

            if (Vector2.Distance(transform.position, midPoint.position) < 0.1f)
            {
                movingToMidPoint = false;
                StartCoroutine(WaitAtPoint(leftPoint));
                StartAttack = true;
            }
        }
        else
        {
            MoveTowardsTarget(currentTarget, speedBetweenPoints);

            if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
            {
                StartCoroutine(WaitAtPoint(currentTarget == leftPoint ? rightPoint : leftPoint));
            }
        }
    }

    void MoveTowardsTarget(Transform target, float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    IEnumerator WaitAtPoint(Transform nextTarget)
    {
        isWaiting = true; // Jelzi, hogy az objektum várakozik
        yield return new WaitForSeconds(delayAtPoints); // Várakozás
        isWaiting = false; // Várakozás vége
        currentTarget = nextTarget; // Célpont frissítése
    }

    private void Fire()
    {
        if (Time.time > (IfFire + ShootFrequency))
        {
            GameObject bull1 = Instantiate(Bullet);
            bull1.transform.position = BulletSpawn1.position; // Az első lövedék pozíciója

            GameObject bull2 = Instantiate(Bullet);
            bull2.transform.position = BulletSpawn2.position; // A második lövedék pozíciója

            IfFire = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // Lövedékkel ütközés
    {
        ShootControll Bullet = other.GetComponent<ShootControll>();
        if (Bullet != null)
        {
            IfHit(Bullet);
        }
    }

    private void IfHit(ShootControll bullet)
    {
        if (StartAttack == true) // Ne sebződjön, amíg ő sem lő
        {
            if (BossHp >= 0)
            {
                BossHp--;
                HPBar.SetHealth(BossHp);
            }
            else
            {
                Destroy(this.gameObject);
                SceneManager.LoadScene("Victory");
                //ShowHPBar.SetActive(false); //hp bar is eltűnik
                //Show_ui.SetActive(false); 
                //ShowWictory.SetActive(true); 
            }
            Destroy(bullet.gameObject);
        }
    }
}
