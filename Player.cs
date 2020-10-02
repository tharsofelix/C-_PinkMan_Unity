using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;

    public bool isJumping;

    private Rigidbody2D rig;
    private Animator anim;

    
    Joystick Joy;

    public bool isBlowing;

   
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        Joy = FindObjectOfType<Joystick>();

    }
   
    public void Update()
    {
        
        Move();
       
        Jump();
    }
    void Move()
    {
        //Vector3 movement = new Vector3(Joy.Horizontal, 0f, 0f);
        //transform.position += movement * Time.deltaTime * Speed;
        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2((Joy.Horizontal + movement) * Speed, rig.velocity.y);

        if (Joy.Horizontal > 0f)
            {
                anim.SetBool("walk", true);
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }

            if (Joy.Horizontal < 0f)
            {
                anim.SetBool("walk", true);
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }

            if (Joy.Horizontal == 0)
            {
                anim.SetBool("walk", false);
            }


        

    }
    

    void Jump()
    {

        if (Input.GetButtonDown("Jump") && !isJumping)
        {

            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            anim.SetBool("jump", true);

        }
    }

    public void butUP()
    {
        if (!isJumping)
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            anim.SetBool("jump", true);
        }

    }





    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }

        if(collision.gameObject.tag == "Spike")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Saw")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
            

        }
    }
    void OnTriggerStay2D(Collider2D collider)
    {
       if(collider.gameObject.layer == 11)
        {
            isBlowing = true;
        }
        

    }
     void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 11)
        {
            isBlowing = false;
        }


    }
}
