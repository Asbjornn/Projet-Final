using UnityEngine;

public class StateMovement : State
{
    public StateMovement(StateMachine machine) : base(machine) { }

    public override void OnEnter()
    {
        machine.currentSpeed = machine.playerStats.movementSpeed;
        machine.animator.SetBool("Walk", true);
    }

    public override void OnExit()
    {
        machine.animator.SetBool("Walk", false);
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
        machine.Flip();

        if(!machine.isWalking)
        {
            machine.ChangeState(nameof(StateIdle)); 
        }
    }
}
