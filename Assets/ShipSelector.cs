using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlighter : MonoBehaviour
{
    public static ButtonHighlighter Instance { get; private set; } // Singleton referencia shipspawnernek

    public GameObject highlightPrefab;
    private GameObject currentHighlight;
    private Button[] buttons;
    private string selectedButtonName;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClicked(button));
        }
    }

    void OnButtonClicked(Button clickedButton)
    {
        if (currentHighlight != null)
        {
            Destroy(currentHighlight);
        }

        currentHighlight = Instantiate(highlightPrefab, clickedButton.transform);
        currentHighlight.transform.SetAsFirstSibling();

        selectedButtonName = clickedButton.name;
        Debug.Log("Selected button: " + selectedButtonName);
    }

    public string GetSelectedButtonName()
    {
        return selectedButtonName;
    }
}