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
        Debug.Log("Test " + CannonBody.transform.rotation.eulerAngles.z);
        if (Input.GetMouseButtonUp(0))
        {
            MouseTapPos.Value = Input.mousePosition;

            if (CannonBody.transform.rotation.eulerAngles.z >= 280 &&
                CannonBody.transform.rotation.eulerAngles.z <= 355)
                PlayerTapInput.Raise();
        }
    }
}
