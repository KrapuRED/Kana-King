using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    public float speed;


    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void OnMovementPlayer(float dirX, float dirY)
    {
        Vector2 direction = new Vector2(dirX, dirY).normalized;
        _rb2d.linearVelocity = direction * speed;
    }
}
