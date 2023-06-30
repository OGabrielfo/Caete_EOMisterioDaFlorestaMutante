using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleWaiting : StateMachineBehaviour
{

    private Patroller _patrol;
    private Rigidbody _rb;
    private float _waitTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Patroll", false);
        animator.SetBool("PlayerChase", false);
        if (animator.GetComponent<Patroller>() != null)
        {
            _patrol = animator.GetComponent<Patroller>();
            _waitTime = _patrol.waitTime;
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
            animator.SetBool("Patroll", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<EnemyController>()._isAttacking = false;
        _rb.velocity = Vector3.zero;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }
}
