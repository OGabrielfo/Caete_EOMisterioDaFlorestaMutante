using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMula_Idle : StateMachineBehaviour
{
    private BossMulaController _mulaController;
    private Rigidbody _rb;
    private float _waitTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Invulneravel", false);
        if (animator.GetComponent<BossMulaController>() != null)
        {
            _mulaController = animator.GetComponent<BossMulaController>();
            _waitTime = _mulaController.waitTime;
        }
        _rb = animator.GetComponent<Rigidbody>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_waitTime > 0)
        {
            _rb.velocity = new Vector3(0f, _rb.velocity.y, _rb.velocity.z);
            _waitTime -= Time.deltaTime;
        }
        else
        {
            int action = Random.Range(1, 3);
            animator.SetInteger("RandomAction", action);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rb.velocity = Vector3.zero;
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
