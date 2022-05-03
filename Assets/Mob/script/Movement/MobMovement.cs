using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    protected StatsEnemyManager stats;
    private Animator animator;

    protected Transform target;
    protected GameObject player;

    protected virtual void Start()
    {
        stats = GetComponent<StatsEnemyManager>();   //vengono inizializzate le statistiche
        animator = GetComponentInChildren<Animator>();
        aggroPlayer();
    }

    public void movement()
    {
        animationMovement();
    }

    public void movementDuringAttack()
    {

    }

    public void animationMovement()
    {
        animator.SetBool("Back", false);
        animator.SetBool("Front", false);
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);

        if (transform.localRotation.eulerAngles.y < 45 || transform.localRotation.eulerAngles.y > 315)
        {
            animator.SetBool("Back", true);
            animator.SetTrigger("goUp");
        }
        else if (transform.localRotation.eulerAngles.y < 135)
        {
            animator.SetBool("Right", true);
            animator.SetTrigger("goLeft");
        }
        else if (transform.localRotation.eulerAngles.y < 225)
        {
            animator.SetBool("Front", true);
            animator.SetTrigger("goDown");
        }

        else if (transform.localRotation.eulerAngles.y < 315)
        {
            animator.SetBool("Left", true);
            animator.SetTrigger("goRight");
        }
    }

    public bool canMove1()
    {
        // TODO inserito per test nuovo gioco
        return true;

/*        if (stats.aggroIstantaneo || 
           Vector2.Distance(transform.position, target.position) < stats.rangeAggro)
        {
            return true;
        }
        return false;
*/
    }

    public bool movementTrigger()
    {
        return Vector2.Distance(transform.position, target.position) < stats.rangeAggro || stats.aggroIstantaneo;
//        return Vector2.Distance(mobPosition, target.position) < stats.rangeAggro || stats.aggroIstantaneo;
    }

    // Filippo: 04/01/2020
    // Mantello del Re cambia il target del mob
    public void setPlayer(GameObject newTarget)
    {
        target = newTarget.transform;
    }

    protected void setTarget(Transform newTarget)
    {
        Debug.Log("Target " + target);
        target = newTarget;
    }

    // multiplayer
    // aggro il player più vicino
    public void aggroPlayer()
    {
        // multiplayer
        /*
        float distance = -1f;
        foreach (GameObject playerInGame in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (distance < 0 || distance > Vector2.Distance(transform.position, playerInGame.transform.position))
            {
                player = playerInGame;
                target = player.transform;
            }
        }
        */
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;

    }
}
