using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 6f;

    [SerializeField] private Animator animator;

    private PlayerDash dash;

    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dash = GetComponent<PlayerDash>();
    }

    // NEW INPUT SYSTEM (Vector2)
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = moveInput.normalized;

        //  movement tetap jalan walau dash
        Vector2 targetVelocity = direction * speed;

        if (dash.IsDashing())
        {
            //  blend (jangan override total)
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity + rb.linearVelocity, 0.2f);
        }
        else
        {
            rb.linearVelocity = targetVelocity;
        }

        dash.SetMoveDirection(direction);

        animator.SetFloat("velocity", rb.linearVelocity.magnitude);

        // flip karakter
        if (rb.linearVelocity.x < -0.1f)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        else if (rb.linearVelocity.x > 0.1f)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void OnMovementPlayer(float dirX, float dirY)
    {
        moveInput = new Vector2(dirX, dirY);
    }

    public void PlayWalkSFX()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.walk);
    }

}