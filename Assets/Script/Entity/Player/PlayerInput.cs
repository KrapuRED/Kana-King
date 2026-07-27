using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private GameObject dictionaryPanel;

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 moveDir = ctx.ReadValue<Vector2>();

        playerMovement.OnMovementPlayer(moveDir.x, moveDir.y);
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            PlayerAttackMelee.instance.Attack();
        }
    }

    public void OnDictionary(InputAction.CallbackContext ctx)
    {
        if (ctx.started && !PauseSystem.instance.IsPaused)
        {
            dictionaryPanel.SetActive(!dictionaryPanel.activeSelf);
        }
    }
}