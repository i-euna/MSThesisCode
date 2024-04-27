using System.Collections;
using TMPro;
using UnityEngine;

public class TextUpdate : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI TextComponent;

    [SerializeField]
    private IntVariable TextValue;
    [SerializeField]
    private string Name;

    public void UpdateText() {
        TextComponent.text = Name + TextValue.Value;
    }
}
