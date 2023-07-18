using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDeath : StateMachineBehaviour
{
    private SceneController sceneController;
    private float drownSpeed;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        drownSpeed = animator.GetComponent<Rigidbody>().velocity.y;
        sceneController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneController>();
        animator.GetComponent<Rigidbody>().useGravity = false;
        if (animator.GetBool("isGrounded"))
        {
            animator.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("isOverWater"))
        {
            if (drownSpeed < 0)
            {
                drownSpeed += 0.5f;
            }
            animator.GetComponent<Rigidbody>().velocity = new Vector3(0f, drownSpeed, 0f);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetInteger("Life") < 1)
        {
            sceneController.deathScreen.SetActive(true);
            animator.GetComponent<SpriteRenderer>().enabled = false;
        }
        /*else
        {
            sceneController.deathScreen.SetActive(false);
            animator.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            animator.GetComponent <Rigidbody>().useGravity = true;
        }*/
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
