using UnityEngine;

public class VendorSpawn : MonoBehaviour
{
    #region Singleton
    public static VendorSpawn instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Vendor Manager singleton already exists."); 
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] private float _spawnTime = 5f;

    [SerializeField] private Transform _positionCorner;
    [SerializeField] private Transform _positionStart;
    [SerializeField] private Transform _positionExit;

    [SerializeField] private GameObject _basePrefabVendor;

    private GameObject _actualVendor;

    private float _spawnCounter = 0f;

    void Update()
    {
        if (_actualVendor == null)
        {
            _spawnCounter += Time.deltaTime;
            if (_spawnTime <= _spawnCounter)
            {
                InstantiateVendor();
                _spawnCounter = 0f;
            }
        }
    }

    private void InstantiateVendor()
    {
        // Instantiate a new Vendor and give him an item
        _actualVendor = Instantiate(_basePrefabVendor, transform);

        NPCBehaviour npcBehaviour = _actualVendor.GetComponent<NPCBehaviour>();
        if (npcBehaviour)
        {
            npcBehaviour.SetPositions(_positionCorner, _positionStart, _positionExit);
        }
        else
        {
            Debug.LogWarning("NPCBehaviour doesn't exist in the gameObject");
        }
    }
}
