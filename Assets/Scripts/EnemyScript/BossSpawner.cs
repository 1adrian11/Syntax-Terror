using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnPoint; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Ha a játékos megnyomja a Space billentyűt
        {
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        // z pozi 0-ra kell!! valamiért elállítja vec2-n is
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
        Vector3 newPosition = spawnedObject.transform.position;
        newPosition.z = 0;
        spawnedObject.transform.position = newPosition;
    }

}
