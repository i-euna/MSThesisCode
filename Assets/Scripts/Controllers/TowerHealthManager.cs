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

    [SerializeField]
    private GameEventWithStr CastleBreachEvent;

    private void Start()
    {
        Health.Value = 100;
        UpdateHealth();
        CastleBreachEvent.Event.AddListener(DecreaseHealth);
    }

    public void DecreaseHealth(string enemyType) {
        Health.Value -= GetDamage(enemyType);
        UpdateHealth();

        //if (Health.Value <= 0)
        CheckGameOver.Raise();
    }

    private int GetDamage(string enemyType) {
        EnemyType type = (EnemyType)System.Enum.Parse(typeof(EnemyType), enemyType);
        int damage = 0;
        switch (type) {
            case EnemyType.FAST_WALKER_2SHOTS:
                damage = 50;
                break;
            default:
                damage = 10;
                break;
        }
        return damage;
    }

    void UpdateHealth() {
        TextComponent.text = "Health: " + Health.Value;
    }
}
