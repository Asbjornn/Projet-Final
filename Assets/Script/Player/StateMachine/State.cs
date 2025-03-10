using UnityEngine;

public abstract class State
{
    protected StateMachine machine;

    public State(StateMachine _machine)
    {
        machine = _machine;
    }

    public abstract void OnUpdate();
    public abstract void OnFixedUpdate();
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void OnTriggerEnter(Collider2D collision);
}
