using System;
using UnityEngine;

public class PlayerModel : PlayerModule
{
    public Animator Animator;
    public bool FacingRight = true;

    public override void OnUpdate()
    {
        // use an EPSILON damnit!!
        int i = Math.Sign(Input.GetAxisRaw("Horizontal"));
        float v = Mathf.Abs(Player.UseRigidbody.velocity.x);
        if (i != 0)
        {
            FacingRight = i == 1;
            transform.localScale = new Vector3(1, 1, i);
        }
        Animator.SetBool("IsMoving", v > float.Epsilon);
        Animator.SetFloat("SpeedMultiplier", v / Player.Motor.MoveSpeed);
        Animator.SetBool("IsGrounded", Player.Motor.IsGrounded);
    }
}
