using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer _interactSprite;

    private bool _playerIsNear;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerIsNear)
        {
            Interact();
        }

        if (_interactSprite)
        {
            if (!_interactSprite.gameObject.activeSelf && _playerIsNear)
            {
                _interactSprite.gameObject.SetActive(true);
            }

            if (_interactSprite.gameObject.activeSelf && !_playerIsNear)
            {
                _interactSprite.gameObject.SetActive(false);
            }
        }
    }

    public abstract void Interact();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerIsNear = false;
        }
    }
}
