using TMPro;
using UnityEngine;

public class TowerHealthManager : MonoBehaviour
{
    [SerializeField]
    private IntVariable Health;

    [SerializeField]
    private TextMeshProUGUI TextComponent;

    [SerializeField]
    private GameEvent CheckGameOver;

    private void Start()
    {
        Health.Value = 100;
        UpdateHealth();
    }

    public void DecreaseHealth() {
        Health.Value -= 10;
        UpdateHealth();

        if (Health.Value <= 0)
            CheckGameOver.Raise();
    }

    void UpdateHealth() {
        TextComponent.text = "Health: " + Health.Value;
    }
}
