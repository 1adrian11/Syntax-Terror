using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class PlayerMovementTests
{
    private GameObject playerObject;
    private Player_Movement playerMovement;

    [SetUp]
    public void SetUp()
    {
        // Létrehozunk egy üres GameObject-et, és hozzáadjuk a Player_Movement komponenst
        playerObject = new GameObject();
        playerMovement = playerObject.AddComponent<Player_Movement>();

        // Inicializáljuk az alapvető értékeket
        playerMovement.speed = 5f;
        playerMovement.minpos = new Vector2(-10, -10);
        playerMovement.maxpos = new Vector2(10, 10);
    }

    [TearDown]
    public void TearDown()
    {
        // Eltávolítjuk a teszt során létrehozott objektumot
        Object.DestroyImmediate(playerObject);
    }

    [UnityTest]
    public IEnumerator PlayerMovesHorizontally()
    {
        // Beállítjuk a bemenetet (vízszintes irány: jobbra)
        float input = 1f;

        // Kézzel meghívjuk a HorizontalPos metódust
        Vector2 horizontalVelocity = playerMovement.HorizontalPos(input);

        // Beállítjuk a velocity értéket és meghívjuk a Move metódust
        playerMovement.velocity = horizontalVelocity;
        playerMovement.Move();

        // Várunk egy keretet
        yield return null;

        // Ellenőrizzük, hogy a játékos helyzete változott-e
        Assert.Greater(playerObject.transform.position.x, 0);
    }

    [UnityTest]
    public IEnumerator PlayerMovesVertically()
    {
        // Beállítjuk a bemenetet (függőleges irány: felfelé)
        float input = 1f;

        // Kézzel meghívjuk a VerticalPos metódust
        Vector2 verticalVelocity = playerMovement.VerticalPos(input);

        // Beállítjuk a velocity értéket és meghívjuk a Move metódust
        playerMovement.velocity = verticalVelocity;
        playerMovement.Move();

        // Várunk egy keretet
        yield return null;

        // Ellenőrizzük, hogy a játékos helyzete változott-e
        Assert.Greater(playerObject.transform.position.y, 0);
    }

    [UnityTest]
    public IEnumerator PlayerPositionClampedWithinBounds()
    {
        // Beállítjuk a bemenetet és helyzetet, amely túllépi a határokat
        playerMovement.velocity = new Vector2(20, 20);

        // Meghívjuk a Move metódust
        playerMovement.Move();

        // Várunk egy keretet
        yield return null;

        // Ellenőrizzük, hogy a játékos helyzete a megadott határok között marad
        Vector2 position = playerObject.transform.position;
        Assert.LessOrEqual(position.x, playerMovement.maxpos.x);
        Assert.GreaterOrEqual(position.x, playerMovement.minpos.x);
        Assert.LessOrEqual(position.y, playerMovement.maxpos.y);
        Assert.GreaterOrEqual(position.y, playerMovement.minpos.y);
    }
}