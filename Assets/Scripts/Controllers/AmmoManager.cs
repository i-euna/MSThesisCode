using System.Collections;
using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [SerializeField]
    private IntVariable AmmoCount;

    [SerializeField]
    private TextMeshProUGUI TextComponent;

    [SerializeField]
    private GameEvent PrepareCannonEvent;

    private void Awake()
    {
        AmmoCount.Value = 3;
    }
    void Start()
    {
        UpdateAmmoText();
    }

    public void DecreaseAmmo() {
        AmmoCount.Value--;
        UpdateAmmoText();

        PrepareNextCannon();
    }

    public void IncreaseAmmo() {
        //AmmoCount.Value++;
        //UpdateAmmoText();

        PrepareNextCannon();
    }

    void PrepareNextCannon() {
        PrepareCannonEvent.Raise();
    }

    void UpdateAmmoText() {
        TextComponent.text = "Ammo: " + AmmoCount.Value;
    }
}
