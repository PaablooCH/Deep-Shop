using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class PlayerNPCInteraction : MonoBehaviour, IInteractable
{
    protected GameObject _npc = null;
    protected bool _isPlayer = false;

    private void Update()
    {
        if (_isPlayer && _npc && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    protected abstract void CheckNPC(GameObject npc);

    public abstract void Interact();

    public virtual void EndInteraction()
    {
        _npc = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayer = true;
        }
        else
        {
            CheckNPC(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == _npc)
        {
            _npc = null;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayer = false;
        }
    }
}
