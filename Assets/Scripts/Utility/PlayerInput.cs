using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private GameEvent PlayerTapInput;

    [SerializeField]
    private Vector3Variable MouseTapPos;

    void Update()
    {
        GetPlayerInput();
    }

    /// <summary>
    /// Takes input from player
    /// updates the position of tap
    /// fires tap event
    /// </summary>
    void GetPlayerInput()
    {
        if (Input.GetMouseButtonUp(0))
        {
            MouseTapPos.Value = Input.mousePosition;
            PlayerTapInput.Raise();
        }
    }
}
