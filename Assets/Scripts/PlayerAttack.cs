using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]private float attackCoolDown;
    [SerializeField]private Transform FirePoint;
    [SerializeField]private GameObject[] fireballs;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float coolDownTimer = Mathf.Infinity;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
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
        fireballs[0].transform.position = FirePoint.position;
        fireballs[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        coolDownTimer = 0;
        
    }
}
