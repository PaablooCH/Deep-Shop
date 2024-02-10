using UnityEngine;

public enum NPCState
{
    PATROLLING,
    EXIT,
    GO_TO_POINT,
    NONE
};

public class NPCBehaviour : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 30f;

    private float _timer = 2f;
    private NPCState _npcState = NPCState.NONE;

    private Transform _positionExit;
    private Transform _travelPoint;
    
    private Rigidbody2D _rb;
    private Animator _animator;

    public Transform TravelPoint { get => _travelPoint; set => _travelPoint = value; }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_travelPoint == null)
        {
            return;
        }
        if (Vector2.Distance(transform.position, _travelPoint.position) < 0.1f) // wait in the position
        {
            _animator.SetBool("IsWalking", false);

            switch (_npcState)
            {
                case NPCState.PATROLLING: // demand next point
                    _timer -= Time.deltaTime;
                    if (_timer <= 0f)
                    {
                        _timer = 2f;
                        RequestTravelPoint();
                    }
                    break;
                case NPCState.EXIT:
                    Destroy(gameObject);
                    break;
                case NPCState.GO_TO_POINT:
                    _travelPoint = null;
                    _npcState = NPCState.NONE;
                    break;
                default:
                    break;
            }
        }
        else // go to the nextPoint
        {
            Vector2 movement = (_travelPoint.position - transform.position).normalized;
            _rb.MovePosition(_rb.position + movement * _movementSpeed * Time.deltaTime);

            _animator.SetFloat("X", movement.x);
            _animator.SetFloat("Y", movement.y);

            _animator.SetBool("IsWalking", true);
        }
    }

    public void SetPositions(Transform positionCorner, Transform positionStart, Transform positionExit)
    {
        _positionExit = positionExit;
        transform.position = positionStart.position;
        GoToPoint(positionCorner);
    }

    public void ExitStore()
    {
        _travelPoint = _positionExit;
        _npcState = NPCState.EXIT;
        GetComponent<BoxCollider2D>().enabled = false;
        GameEventsMediator.instance.npcEvents.NPCExit(tag);
    }

    public void GoToPoint (Transform point)
    {
        _npcState = NPCState.GO_TO_POINT;
        _travelPoint = point;
    }

    private void RequestTravelPoint()
    {
        _travelPoint = NextPosition(_travelPoint);
    }

    private Transform NextPosition(Transform currentPosition)
    {
        // TODO return a new position different from the current
        return transform;
    }
}
