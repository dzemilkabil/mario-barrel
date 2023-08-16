using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 move;
    private Rigidbody2D rbPlayer;
    private Animator animatorPlayer;

    //OverlapsCircle
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;

    [SerializeField] float speed = 150;
    [SerializeField] float jumpHigh = 6;
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        Debug.Log(isGrounded);

        if (move.x < -0.01)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (move.x > 0.01)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        animatorPlayer.SetFloat("speed",Mathf.Abs(move.x));
        rbPlayer.velocity = new Vector2(move.x*speed*Time.fixedDeltaTime, rbPlayer.velocity.y);
    }

    public void Movement(InputAction.CallbackContext value)
    {
        move = value.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext value)
    {
        if (value.started) {
            if (isGrounded) {
                rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, jumpHigh);
            }
        }
    }
}
