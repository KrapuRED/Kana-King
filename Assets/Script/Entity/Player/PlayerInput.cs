using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttackMelee playerAttackMelee;
    [Header("Key Bind")]
    [SerializeField] private KeyCode Up;
    [SerializeField] private KeyCode Down;
    [SerializeField] private KeyCode Left;
    [SerializeField] private KeyCode Right;

    void Update()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;
        if (Input.GetKey(Up))
        {
            verticalInput = 1f;
        }
        else if (Input.GetKey(Down))
        {
            verticalInput = -1f;
        }

        if (Input.GetKey(Right))
        {
            horizontalInput = 1f;
        }
        else if (Input.GetKey(Left))
        {
            horizontalInput = -1f;
        }

        playerMovement.OnMovementPlayer(horizontalInput, verticalInput);
    }


    public void OnAttack(InputAction.CallbackContext ctx)
    {
        Debug.Log("attack");
        if(ctx.performed)
            playerAttackMelee.Attack();
    }
}
