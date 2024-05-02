using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private GameEvent PlayerTapInput;

    [SerializeField]
    private Vector3Variable MouseTapPos;

    [SerializeField]
    private GameObject CannonBody;

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
            if (CannonBody.transform.rotation.eulerAngles.z < 225)
            {
                return;
            }
            PlayerTapInput.Raise();
        }
    }
}
