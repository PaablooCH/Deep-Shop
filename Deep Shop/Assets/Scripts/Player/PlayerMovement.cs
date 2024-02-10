using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour, IPersistenceData
{
    [SerializeField] private float _movementSpeed = 5f;

    private Rigidbody2D _rb;

    private Animator _animator;
    
    private Vector2 _movement;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        if (_movement.x != 0 || _movement.y != 0)
        {
            _animator.SetFloat("X", _movement.x);
            _animator.SetFloat("Y", _movement.y);

            _animator.SetBool("IsMoving", true);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }
    }

    private void FixedUpdate()
    {
        if (_movement != Vector2.zero)
        {
            _rb.MovePosition(_rb.position + _movement * _movementSpeed * Time.fixedDeltaTime);
        }
    }

    public void SaveData(ref GameData data)
    {
        PlayerData playerData = new PlayerData();
        playerData.position = transform.position;

        data.playerData = playerData;
    }

    public void LoadData(GameData data)
    {
        transform.position = data.playerData.position;
    }
}
