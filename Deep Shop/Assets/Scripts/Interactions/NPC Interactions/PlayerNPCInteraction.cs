using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNPCInteraction : MonoBehaviour
{
    protected GameObject _npc = null;
    protected bool _isPlayer = false;

    // Update is called once per frame
    void Update()
    {
        if (_isPlayer && _npc && Input.GetKeyDown(KeyCode.E))
        {
            // open dialog
        }
    }

    public virtual void EndInteraction()
    {
        _npc = null;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayer = true;
        }
        else
        {
            NeededByInheritClasses(collision.gameObject);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
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

    protected virtual void NeededByInheritClasses(GameObject npc) { }
}
