using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Inventory Manager singleton already exists.");
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] private GameObject _actualPlayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // TODO switch character
        }
    }

    public PlayerInventory GetPlayerInventory()
    {
        return _actualPlayer.GetComponent<PlayerInventory>();
    }

    public void ChangePlayer(GameObject player)
    {
        if (player.CompareTag("Player"))
        {
            EnableDisablePlayer(false);
            _actualPlayer = player;
            EnableDisablePlayer(true);
        }
        else
        {
            Debug.LogWarning("We want to change the player to an instance who is not a Player. Tag: " + player.tag);
        }
    }

    private void EnableDisablePlayer(bool isNewPlayer)
    {
        _actualPlayer.GetComponent<PlayerMovement>().enabled = isNewPlayer;
        _actualPlayer.GetComponent<Rigidbody2D>().isKinematic = !isNewPlayer;
    }
}
