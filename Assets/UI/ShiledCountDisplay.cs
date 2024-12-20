using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShieldCountDisplay : MonoBehaviour
{
    [SerializeField] public Player_Movement player; // Hivatkozás a játékos scriptjére
    [SerializeField] public TextMeshProUGUI shieldCountText;   // Hivatkozás a szöveg UI elemére

    void Start()
    {
        if (player != null)
        {
            // Feliratkozunk a Shieldpow változás eseményére
            player.IfChange.AddListener(UpdateShieldCount);
        }
        UpdateShieldCount(player);
    }

    void UpdateShieldCount(Player_Movement updatedPlayer)
    {
        if (shieldCountText != null)
        {
            shieldCountText.text = $"{updatedPlayer.Shieldpow}";
        }
    }
}