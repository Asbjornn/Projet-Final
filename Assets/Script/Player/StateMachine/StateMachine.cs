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

    [Space(10)]
    [Header("PlayerData")]
    public float currentSpeed;

    [Space(10)]
    [Header("Scripts")]
    public PlayerStats playerStats;

    [Space(10)]
    [Header("Bools")]
    public bool isWalking;
    public bool isShooting;

    [Space(10)]
    [Header("Detection")]
    public float range;

    [Space(10)]
    [Header("UI")]
    public TextMeshProUGUI textCurrentState;

    private void Awake()
    {
        playerStats.InitialiseStats();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _state.Add(nameof(StateIdle), new StateIdle(this));
        _state.Add(nameof(StateMovement), new StateMovement(this));

        ChangeState(nameof(StateIdle));
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
        textCurrentState.text = currentState.ToString();

        Collider2D col = Physics2D.OverlapCircle(transform.position, range);
        if (col.CompareTag("Cross"))
        {

        }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform.position, range);
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
