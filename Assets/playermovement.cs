using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    public LayerMask yerLayer;
    public LayerMask duvarLayer;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
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
        horizontalinput = Input.GetAxisRaw("Horizontal");

        ///flip player left or right

        if(horizontalinput>0.01f)
            transform.localScale = Vector3.one * 6;
        else if (horizontalinput<0f)
            transform.localScale = new Vector3(-6,6,6);

        ///set animator parameters
        anim.SetBool("run",horizontalinput != 0);
        anim.SetBool("grounded", İsgrounded());

        //duvara cool down ver ki duvardan zıplama çalışsın
        if (WallJumpCoolDown > 0.2f)
        {

            body.velocity = new Vector2(horizontalinput*speed,body.velocity.y);

             //stick to wall
            if(onwall() && ! İsgrounded())
            {   
                body.gravityScale = 0;
                body.velocity = new Vector2(0,-1f);
                
            }
            else
            {
                body.gravityScale =3;
            }
            if (Input.GetKeyDown(KeyCode.Space) )
            Jump();
        }
        else 
            WallJumpCoolDown += Time.deltaTime;

    }
    
    private void Jump() 
    {
        if (İsgrounded())
        {
            body.velocity = new Vector2(body.velocity.x,jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onwall() && ! İsgrounded())
        {
            if (horizontalinput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*10,0);
                transform.localScale= new Vector3(-(transform.localScale.x),transform.localScale.y,transform.localScale.z);
    
            }
            else 
            {
                 body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*7,6);

            }
            WallJumpCoolDown = 0;
           

    
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {

    }
    

    private bool İsgrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcollider.bounds.center,boxcollider.bounds.size,0,Vector2.down,0.01f,yerLayer);
        return raycastHit.collider != null;
}
     private bool onwall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcollider.bounds.center,boxcollider.bounds.size,0,new Vector2(transform.localScale.x,0),0.01f,duvarLayer);
        return raycastHit.collider != null;
}
}
    


