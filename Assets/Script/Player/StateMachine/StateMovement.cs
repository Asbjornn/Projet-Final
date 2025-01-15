using UnityEngine;

public class StateMovement : State
{
    public StateMovement(StateMachine machine) : base(machine) { }

    public override void OnEnter()
    {
        machine.animator.SetBool("Walk", true);
    }

    public override void OnExit()
    {
        machine.animator.SetBool("Walk", false);
    }

    public override void OnFixedUpdate()
    {
        machine.rb.linearVelocity = machine.direction * machine.playerStats.movementSpeed;
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
