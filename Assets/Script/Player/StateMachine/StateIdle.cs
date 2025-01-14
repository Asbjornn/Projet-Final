using UnityEngine;

public class StateIdle : State
{
    public StateIdle(StateMachine machine) : base(machine) { }

    public override void OnEnter()
    {
        machine.currentSpeed = 0;
        machine.animator.SetBool("Idle", true);
    }

    public override void OnExit()
    {
        machine.animator.SetBool("Idle", false);
    }

    public override void OnFixedUpdate()
    {
        machine.rb.linearVelocity = machine.direction * machine.currentSpeed;
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        
    }

    public override void OnUpdate()
    {
        if(machine.isWalking)
        {
            machine.ChangeState(nameof(StateMovement));
        }
    }
}
