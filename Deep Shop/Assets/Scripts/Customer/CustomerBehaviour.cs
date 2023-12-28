using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomerState
{
    PATROLLING,
    EXIT,
    NONE
};

public class CustomerBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 30f;

    private float _timer = 2f;
    private CustomerState _customerState = CustomerState.NONE;
    private Transform _travelPoint;
    
    private Rigidbody2D _rb;
    private Animator _animator;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_travelPoint == null)
        {
            return;
        }
        if (Vector2.Distance(transform.position, _travelPoint.position) < 0.1f) // wait in the position
        {
            _animator.SetBool("IsWalking", false);

            switch (_customerState)
            {
                case CustomerState.PATROLLING: // demand next point
                    _timer -= Time.deltaTime;
                    if (_timer <= 0f)
                    {
                        _timer = 2f;
                        RequestTravelPoint();
                    }
                    break;
                case CustomerState.EXIT:
                    Destroy(gameObject);
                    break;
                case CustomerState.NONE:
                    _travelPoint = null;
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

    public void ExitStore(Transform outside)
    {
        SetTravelPoint(outside);
        _customerState = CustomerState.EXIT;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    public CustomerState GetCustomerState()
    {
        return _customerState;
    }

    public void SetPatrolling()
    {
        _customerState = CustomerState.PATROLLING;
    }

    public void SetTravelPoint(Transform travelPoint)
    {
        this._travelPoint = travelPoint;
    }

    private void RequestTravelPoint()
    {
        _travelPoint = CustomerManager.instance.NextPosition(_travelPoint);
    }
}
