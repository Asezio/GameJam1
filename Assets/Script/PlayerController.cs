using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    protected Transform trans;
    public Transform transJump;
    public BoxCollider2D attackPoint;
    private bool canHurt = true;
    private bool timeOnce = true;

    protected Animator anim;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpLow;
    [SerializeField]
    private float jumpHigh;
    [SerializeField]
    private float cooldownTime;

    public float raycastOffsetX;
    public static int health;

    private float raycastDistance = 0.01f;
    private float speed1;
    private float timeSpentInvincible;

    static public bool isDead = false;
    private bool CanBeDamaged = true;
    public bool canJump = true;
    public bool faceRight = true;
    public bool isClimbing = false;
    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();
        attackPoint = GetComponentInChildren<BoxCollider2D>();
        attackPoint.enabled = false;
        //transJump = GetComponent<Transform>();
        speed1 = speed;
        health = 3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Move(horizontalInput);

        //IsGrounded
        if (IsGrounded(-raycastOffsetX) || IsGrounded(raycastOffsetX))
        {
            anim.SetBool("isGrounded", true);
            canJump = true;
            transJump.position = trans.position;
            if (Input.GetKey(KeyCode.Z))
            {
                anim.SetTrigger("Jump");
                rb.velocity = new Vector2(rb.velocity.x, jumpLow);
                this.transform.parent = null;
                if (rb.velocity.y < 0)
                {
                    anim.SetBool("Fall", true);
                }
                //if ((m_jumpSfx != null) && (GetComponent<AudioSource>() != null))
                //{
                //    GetComponent<AudioSource>().clip = m_jumpSfx;
                //    GetComponent<AudioSource>().Play();
                //}
            }
        }
        else
        {
            anim.SetBool("isGrounded", false);
            anim.SetBool("Fall", false);
        }


        if (Input.GetKey(KeyCode.X) && canJump == true && trans.position.y - transJump.position.y <= jumpHigh)
        {
            anim.SetTrigger("Jump");
            //Debug.Log(trans.position.y - transJump.position.y);
            rb.velocity = new Vector2(rb.velocity.x, jumpLow);
        }

        if (Input.GetKeyUp(KeyCode.X) || rb.velocity.y < 0)
        {
            canJump = false;
            anim.SetBool("Fall", true);
        }
        else
        {
            anim.SetBool("Fall", false);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("Attack", true);
            if (IsGrounded(-raycastOffsetX) || IsGrounded(raycastOffsetX))
            {
                speed = 0;
                Attack();
            }
            else
            {
                speed = speed1;
                Attack();
            }
        }
        else
        {
            speed = speed1;
            attackPoint.enabled = false;
            anim.SetBool("Attack", false);

        }

        if (isClimbing == true)
        {
            rb.gravityScale = 0;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(rb.velocity.x, 2);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.velocity = new Vector2(rb.velocity.x, -2);
            }
        }
        else
        {
            rb.gravityScale = 3;
        }


        //Hurt
        if (canHurt == false)
        {
            //Debug.Log(1);
            if (timeOnce == true)
            {
                anim.SetTrigger("Hit");
                timeSpentInvincible = 0f;
                timeOnce = false;
            }
            timeSpentInvincible += Time.deltaTime;
            //3			
            if (timeSpentInvincible < 1f)
            {
                float remainder = timeSpentInvincible % 0.3f;
                sr.enabled = remainder > 0.15f;
            }
            else
            {
                sr.enabled = true;
                canHurt = true;
                timeOnce = true;
            }
        }


        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("speedY", rb.velocity.y);
        //Debug.Log(canJump);
        //Debug.Log(speed);
    }

    private void Move(float direction)
    {
        //flip the sprite based on the direction this unit is moving
        if (direction < 0)
        {
            sr.flipX = true;
            faceRight = false;
        }
        if (direction > 0)
        {
            sr.flipX = false;
            faceRight = true;

        }

        //setting the velocity to move our character
        //keep the y-velocity at it original value

        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        //anim.SetFloat("Speed", Mathf.Abs(direction));
    }

    private bool IsGrounded(float offsetX)
    {
        //We start a raycast at beario's feet
        Vector2 raycastStart = trans.position;
        raycastStart.x += offsetX;
        //Draw a debug-ray to show how raycast works
        Debug.DrawRay(raycastStart, Vector2.down * raycastDistance, Color.red);

        //The raycast goes down,
        RaycastHit2D hitInfo = Physics2D.Raycast(raycastStart, Vector2.down, raycastDistance);

        if (hitInfo.collider == null || hitInfo.collider.tag == "Player")
        {
            //Debug.LogError("here");
            return false;
        }
        return true;

    }

    void Attack()
    {
        attackPoint.enabled = true;

    }

    public void JumpLow()
    {
        canJump = true;
        transJump.position = trans.position;
        rb.velocity = new Vector2(rb.velocity.x, jumpLow);
        //this.transform.parent = null;

    }

    private void Die()
    {

    }

    public void Hurt()
    {
        health--;
        if (health <= 0)
        {
            Die();
        }
        canHurt = false;
        //StartCoroutine(CanHurt());

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy" && canHurt == true)
        {
            Hurt();
        }
    }

    IEnumerator CanHurt()
    {
        canHurt = false;
        yield return new WaitForSeconds(cooldownTime);
        canHurt = true;
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(1f);
    }
}

