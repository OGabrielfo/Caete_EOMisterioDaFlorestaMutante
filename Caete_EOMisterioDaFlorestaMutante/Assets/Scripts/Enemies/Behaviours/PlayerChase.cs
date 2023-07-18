using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerChase : StateMachineBehaviour
{
    private GameObject _player;
    private Rigidbody _rb;

    private bool _aereo;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = animator.GetComponent<Rigidbody>();
        if(_rb.useGravity)
        {
            _aereo = false;
        }
        else
        {
            _aereo = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("PlayerChase"))
        {
            if (_aereo)
            {
                Vector3 direction = _player.transform.position - animator.transform.position;
                direction.Normalize();
                _rb.velocity = direction * animator.GetFloat("Speed");
            }
            else
            {
                Vector3 direction = _player.transform.position - animator.transform.position;
                direction.Normalize();
                _rb.velocity = new Vector3(direction.x * animator.GetFloat("Speed"), _rb.velocity.y, 0f);
            }
        }
        else
        {
            /*Vector2 direction = _controller.ceilingPoint.transform.position - animator.transform.position;
            direction.Normalize();
            _rb.velocity = direction * _controller.speed;*/
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
