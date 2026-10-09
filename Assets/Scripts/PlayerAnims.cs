using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnims : MonoBehaviour
{
    public const int Idle = 0;
    public const int Walk = 1;      
    public const int FrontWalk = 2; 
    public const int BackWalk = 3;  

    [SerializeField] private string stateParameter = "State";
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private bool spriteFacesLeft = false;

    private Animator animator;
    private int stateHash;
    private int currentState = -1;

    public int AnimState { get; private set; }

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (playerMovement == null) playerMovement = GetComponent<PlayerMovement>();
        stateHash = Animator.StringToHash(stateParameter);
    }

    void Update()
    {
        Vector2 input = playerMovement != null ? playerMovement.MoveInput : Vector2.zero;

        int newState;
        if (input.sqrMagnitude < 0.01f)
        {
            newState = Idle;
        }
        else if (!Mathf.Approximately(input.x, 0f))
        {
            newState = Walk;
            FlipSprite(input.x);
        }
        else
        {
            newState = input.y < 0f ? FrontWalk : BackWalk;
        }

        AnimState = newState;

        if (newState != currentState)
        {
            currentState = newState;
            animator.SetInteger(stateHash, newState);
        }
    }

    private void FlipSprite(float horizontal)
    {
        bool movingLeft = horizontal < 0f;
        bool flip = spriteFacesLeft ? !movingLeft : movingLeft;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (flip ? -1f : 1f);
        transform.localScale = scale;
    }
}
