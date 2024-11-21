using UnityEngine;
using TMPro;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    public float startTime = 10f; // kezdőérték sec
    private float currentTime;
    public TextMeshProUGUI countdownText; // visszaszámláló
    public TextMeshProUGUI messageText; 
    public GameObject conditionObject; // vége e a gamenek

    public MeteorSpawner meteorSpawner;
    bool BossIsActive = false;

    private bool hasModifiedSpawnRate = false;

    void Start()
    {
        currentTime = startTime; // Kezdőérték
        messageText.gameObject.SetActive(false);
    }

    void Update()
    {
        // vége e a gamenek
        if (conditionObject.activeSelf)
        {
            StopCountdown();
            return;
        }

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            countdownText.text = Mathf.Ceil(currentTime).ToString(); // Kerekítés felfelé és megjelenítés
        }
        else
        {
            if (countdownText.gameObject.activeSelf)
            {
                countdownText.gameObject.SetActive(false); 
                messageText.gameObject.SetActive(true);
                StartCoroutine(HideMessageTextAfterSeconds(4f)); // hány sec után tűnjön el 
                HandleTimeExpired();
            }
        }
    }

    void StopCountdown()
    {
        countdownText.gameObject.SetActive(false);
        currentTime = 0;
    }

    void HandleTimeExpired()
    {
        if (!hasModifiedSpawnRate)
        {
            hasModifiedSpawnRate = true; 
            meteorSpawner.spawnrate = 1000; // ne legyen meteor boss intrón
            StartCoroutine(ResetSpawnRateAfterSeconds(10f)); // utána legyen
            BossIsActive = true;
        }
    }

    IEnumerator ResetSpawnRateAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        meteorSpawner.spawnrate = 0.5f; // Spawnrate visszaállítása
    }

    IEnumerator HideMessageTextAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds); 
        messageText.gameObject.SetActive(false);
    }
}