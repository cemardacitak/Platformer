using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    public LayerMask yerLayer;
    public LayerMask duvarLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxcollider;
    private float WallJumpCoolDown;
    private float horizontalinput;


    private void Awake()
     {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider2D>();
    
    }

    private void Update() 
    {
        print(isGrounded());
        horizontalinput = Input.GetAxisRaw("Horizontal");

        ///flip player left or right

        if(horizontalinput>0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalinput<0f)
            transform.localScale = new Vector3(-1,1,1);

        ///set animator parameters
        anim.SetBool("run",horizontalinput != 0);
        anim.SetBool("grounded", isGrounded());
        anim.SetBool("isWallSliding", onWall());
        
        if (isGrounded())
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }

        //duvara cool down ver ki duvardan zıplama çalışsın
        if (WallJumpCoolDown > 0.2f)
        {

            body.velocity = new Vector2(horizontalinput*speed,body.velocity.y);

             //stick to wall
            if(onWall() && !isGrounded())
            {   
                body.gravityScale = 0;
                body.velocity = new Vector2(0,-1.3f);
                
            }
            else
            {

                body.gravityScale = 3;;
            }
            if (Input.GetKeyDown(KeyCode.Space) )
            {
                Jump();
            }
        }
        else 
            WallJumpCoolDown += Time.deltaTime;

    }
    
    private void Jump() 
    {
        if (isGrounded())
        {
            anim.SetTrigger("jump");
            body.velocity = new Vector2(body.velocity.x,jumpPower);
            
        }
        else if (onWall() && !isGrounded())
        {
            
            if (horizontalinput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*10,0);
                transform.localScale= new Vector3(-Mathf.Sign(transform.localScale.x),transform.localScale.y,transform.localScale.z);
    
            }
            else 
            {
                 body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*7,6);

            }
            WallJumpCoolDown = 0;
           

    
        }
        
    }


    // OnCollision with Duvar
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Duvar"))
        {
            anim.SetTrigger("isWallSlideTriggered");
        }
    }
    
    // Check is it touching ground
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcollider.bounds.center,boxcollider.bounds.size,0,Vector2.down,0.03f,yerLayer);
        return raycastHit.collider != null;
    }

    // Check is it touching wall 
     private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcollider.bounds.center,boxcollider.bounds.size,0,new Vector2(transform.localScale.x,0),0.01f,duvarLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalinput == 0 && isGrounded() && !onWall();
    }
}
    


