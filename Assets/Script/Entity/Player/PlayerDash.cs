using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerDash : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashForce = 18f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 0.5f;

    [Header("Drag Control")]
    public float dashDrag = 8f;
    public float normalDrag = 0f;

    private Rigidbody2D rb;

    private Vector2 dashDirection = Vector2.right;
    private Vector2 lastMoveDirection = Vector2.right;

    private bool isDashing = false;
    private bool canDash = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // dipanggil dari movement
    public void SetMoveDirection(Vector2 dir)
    {
        if (dir != Vector2.zero)
        {
            dashDirection = dir;
            lastMoveDirection = dir;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        Vector2 dir = dashDirection == Vector2.zero ? lastMoveDirection : dashDirection;
        dir.Normalize();

        // 🔥 kasih dorongan (bukan overwrite terus)
        rb.linearVelocity = dir * dashForce;

        // 🔥 bikin cepat berhenti (biar enak feel nya)
        rb.linearDamping = dashDrag;

        yield return new WaitForSeconds(dashDuration);

        // balik normal
        rb.linearDamping = normalDrag;

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public bool IsDashing()
    {
        return isDashing;
    }
}