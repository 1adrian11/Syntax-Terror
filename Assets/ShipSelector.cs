using UnityEngine;
using UnityEngine.UI;

public class ShipSelector : MonoBehaviour
{
    public GameObject highlightPrefab; // Az a prefab, ami a kijelölést mutatja (pl. egy UI Image keret)
    private GameObject currentHighlight; // Az aktuális kijelölés
    private Button[] buttons; // A gombok listája

    void Start()
    {
        // Az összes gombot összegyűjtjük a szülő objektumból
        buttons = GetComponentsInChildren<Button>();

        // Minden gombra hozzárendeljük a kattintás eseményt
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClicked(button));
        }
    }

    void OnButtonClicked(Button clickedButton)
    {
        // Ha van aktív kijelölés, azt eltüntetjük
        if (currentHighlight != null)
        {
            Destroy(currentHighlight);
        }

        // Új kijelölés létrehozása a kattintott gomb körül
        currentHighlight = Instantiate(highlightPrefab, clickedButton.transform);
        currentHighlight.transform.SetAsFirstSibling(); // Az overlay biztosítása
    }
}