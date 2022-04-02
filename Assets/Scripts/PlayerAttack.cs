using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]private float attackCoolDown;
    [SerializeField]private Transform FirePoint;
    [SerializeField]private GameObject[] fireballs;
    private Animator anim;
    private Player_Movement playerMovement;
    private float coolDownTimer = Mathf.Infinity;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<Player_Movement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && coolDownTimer > attackCoolDown && playerMovement.canAttack())
        {
            Attack();    
        }
        coolDownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("fireball");
        fireballs[FindFireball()].transform.position = FirePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        coolDownTimer = 0;
        
    }
    private int FindFireball()
    {
         for (int i = 0; i < fireballs.Length; i++) 
         {
             if(!fireballs[i].activeInHierarchy)
                 return i;

   
         }
         return 0;

    }
}
