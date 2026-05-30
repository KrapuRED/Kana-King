using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    public float speed;
    [SerializeField] private Animator animator;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void OnMovementPlayer(float dirX, float dirY)
    {
        Vector2 direction = new Vector2(dirX, dirY).normalized;
        _rb2d.linearVelocity = direction * speed;
        animator.SetFloat("velocity", _rb2d.linearVelocity.magnitude);
        if (_rb2d.linearVelocity.x < -0.1f)
        {
            // Menghadap Kiri: Putar Y sebesar 180 derajat
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (_rb2d.linearVelocity.x > 0.1f)
        {
            // Menghadap Kanan: Kembalikan Y ke 0 derajat
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
