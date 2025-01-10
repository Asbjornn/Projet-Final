using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateMachine : MonoBehaviour
{
    public Dictionary<string, State> _state = new();
    public State currentState;

    [Header("PlayerComponent")]
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Vector2 direction;

    [Header("PlayerData")]
    public float currentSpeed;

    [Header("Scripts")]
    public PlayerStats playerStats;

    [Header("Bools")]
    public bool isWalking;
    public bool isShooting;

    public TextMeshProUGUI textCurrentState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats.InitialiseStats();

        _state.Add(nameof(StateIdle), new StateIdle(this));
        _state.Add(nameof(StateMovement), new StateMovement(this));

        ChangeState(nameof(StateIdle));
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
        textCurrentState.text = currentState.ToString();
    }

    private void FixedUpdate()
    {
        currentState.OnFixedUpdate();
    }

    public void ChangeState(string stateName)
    {
        currentState?.OnExit();
        currentState = _state[stateName];
        currentState.OnEnter();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTriggerEnter(other);
    }

    public void Flip()
    {
        if (direction.x != 0)
        {
            if (direction.x < 0.01)
            {
                spriteRenderer.flipX = true;
            }
            else if (direction.x > 0.01)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

#region inputAction
    public void Movement(InputAction.CallbackContext context)
    {
        switch(context.phase)
        {
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Waiting:
                break;
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                direction = context.ReadValue<Vector2>();
                isWalking = true;
                break;
            case InputActionPhase.Canceled:
                direction = Vector2.zero;
                isWalking = false;
                break;
            default:
                break;
        }
    }
#endregion
}
