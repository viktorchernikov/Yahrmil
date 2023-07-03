using UnityEngine;

public class PlayerMotor : PlayerModule
{
    public float MaxSlopeAngle = 45f;
    public float MoveSpeed = 2;
    public float AccelerateRate = 100;
    public bool IsGrounded { get; private set; }
    public float GroundCheckDistance = 0.125f;
    public Vector2 GroundNormal = Vector2.zero;

    public override void OnFixedUpdate()
    {
        CheckGround();
        if (true)
        {
            float inp = GameInput.Move.x;
            Vector2 mv = transform.forward * inp;
            if (Mathf.Abs(Vector2.Dot(Player.UseRigidbody.velocity, mv)) < MoveSpeed)
            {
                Player.UseRigidbody.AddForce(mv * AccelerateRate, ForceMode.Force);
            }
        }
    }
    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, GroundCheckDistance);
        bool flag = false;
        Vector2 vector = Vector2.right;
        if (hit != default)
        {
            if (!hit.transform.CompareTag("Tape"))
            {
                flag = true;
                vector = hit.normal;
            }
        }
        IsGrounded = flag;
        GroundNormal = vector;
    }
}
