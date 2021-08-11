using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rbd;
    public float mspeed, jumpforce;

    private float velocity;

    private bool isGround;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    public Animator anim;

    public bool isKeyboard2;
    public float timeBetweenAttacks = 0.25f;
    private float attackCounter;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        GameManager.instance.AddPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {

        if (isKeyboard2)
        {
            velocity = 0f;

            if (Keyboard.current.lKey.isPressed)
            {
                velocity = 1f;
            }
            if (Keyboard.current.jKey.isPressed)
            {
                velocity = -1f;
            }
            if (isGround && Keyboard.current.rightShiftKey.wasPressedThisFrame)
            {
                rbd.velocity = new Vector2(rbd.velocity.x, jumpforce);
            }
            if (!isGround && Keyboard.current.rightShiftKey.wasReleasedThisFrame && rbd.velocity.y > 0f)
            {
                rbd.velocity = new Vector2(rbd.velocity.x, rbd.velocity.y * .5f);
            }
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                anim.SetTrigger("attack");
                attackCounter = timeBetweenAttacks;
            }

        }

        isGround = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

        rbd.velocity = new Vector2(velocity * mspeed, rbd.velocity.y);

        /*   if (Input.GetButtonDown("Jump"))
           {
               rbd.velocity = new Vector2(rbd.velocity.x, jumpforce);
           }
       } */

        anim.SetBool("isGrounded", isGround);
        anim.SetFloat("yspeed", rbd.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(rbd.velocity.x));

        if (rbd.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, -1f);
        }
        else if (rbd.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        if(attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;

            rbd.velocity = new Vector2(0f, rbd.velocity.y);
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        velocity = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && isGround)
        {
            rbd.velocity = new Vector2(rbd.velocity.x, jumpforce);
        }

        if (!isGround && context.canceled && rbd.velocity.y > 0f)
        {
            rbd.velocity = new Vector2(rbd.velocity.x, rbd.velocity.y * .5f);
        }
    }

    public void attack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            anim.SetTrigger("attack");

            attackCounter = timeBetweenAttacks;
        }
    }
}
