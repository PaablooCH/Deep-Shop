using UnityEngine;

public abstract class NPC : MonoBehaviour, IInteractable
{
    [SerializeField]
    private SpriteRenderer _interactSprite;
    [SerializeField]
    [Min(0.1f)]
    private float _interactionDistance = 1.1f;

    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerClose())
        {
            Interact();
        }

        if (!_interactSprite.gameObject.activeSelf && IsPlayerClose())
        {
            _interactSprite.gameObject.SetActive(true);
        }

        if (_interactSprite.gameObject.activeSelf && !IsPlayerClose())
        {
            _interactSprite.gameObject.SetActive(false);
        }
    }

    public abstract void Interact();

    private bool IsPlayerClose()
    {
        if (Vector2.Distance(_playerTransform.position, transform.position) <= _interactionDistance)
        {
            return true;
        }
        return false;
    }
}
