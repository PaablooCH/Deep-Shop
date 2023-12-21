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
    private float movementSpeed = 25f;

    private float timer = 2f;
    private CustomerState customerState = CustomerState.NONE;
    private Transform travelPoint;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (travelPoint == null)
        {
            return;
        }
        if (Vector2.Distance(transform.position, travelPoint.position) < 0.5f) // wait in the position
        {
            if (rb.velocity != Vector2.zero)
            {
                rb.velocity = Vector2.zero;
            }
            switch (customerState)
            {
                case CustomerState.PATROLLING: // demand next point
                    timer -= Time.deltaTime;
                    if (timer <= 0f)
                    {
                        timer = 2f;
                        RequestTravelPoint();
                    }
                    break;
                case CustomerState.EXIT:
                    Destroy(gameObject);
                    break;
                case CustomerState.NONE:
                    travelPoint = null;
                    break;
                default:
                    break;
            }
        }
        else // go to the nextPoint
        {
            Vector2 movement = (travelPoint.position - transform.position).normalized;
            rb.MovePosition(rb.position + movement * movementSpeed * Time.deltaTime);
        }
    }

    public void ExitStore(Transform outside)
    {
        SetTravelPoint(outside);
        customerState = CustomerState.EXIT;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    public CustomerState GetCustomerState()
    {
        return customerState;
    }

    public void SetPatrolling()
    {
        customerState = CustomerState.PATROLLING;
    }

    public void SetTravelPoint(Transform travelPoint)
    {
        this.travelPoint = travelPoint;
    }

    private void RequestTravelPoint()
    {
        travelPoint = CustomerManager.instance.NextPosition(travelPoint);
    }
}
