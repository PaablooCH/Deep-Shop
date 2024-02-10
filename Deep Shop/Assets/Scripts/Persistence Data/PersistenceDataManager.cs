using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PersistenceDataManager : MonoBehaviour
{
    #region Singleton
    public static PersistenceDataManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Persistence Data Manager singleton already exists.");
            return;
        }
        instance = this;
    }
    #endregion
    
    [Header("File Storage Config")]
    [SerializeField] private string _fileName = "";
    [SerializeField] private bool _useEncryption;

    private FileDataHandler _fileDataHandler;
    private GameData _gameData;
    private IPersistenceData[] _dataPersistencesObjects;

    private void Start()
    {
        _fileDataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);
        _dataPersistencesObjects = FindAllIDataPersistences();
        LoadGame();
    }

    public void NewGame()
    {
        _gameData = new GameData();

        // Start values from JSONs
        _gameData.inventoryData.StartItemsJSON();
    }

    public void LoadGame()
    {
        // Load any data from the system
        _gameData = _fileDataHandler.Load();

        if (_gameData == null)
        {
            Debug.Log("No data was found. Start New Game.");
            NewGame();
        }

        // Load the values into the components that need it
        foreach (IPersistenceData dataPerObj in _dataPersistencesObjects)
        {
            dataPerObj.LoadData(_gameData);
        }
    }

    public void SaveGame()
    {
        // Save the data from the components
        foreach (IPersistenceData dataPerObj in _dataPersistencesObjects)
        {
            dataPerObj.SaveData(ref _gameData);
        }

        // Create the file with the new data
        _fileDataHandler.Save(_gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private IPersistenceData[] FindAllIDataPersistences()
    {
        IEnumerable<IPersistenceData> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().
            OfType<IPersistenceData>();

        return dataPersistenceObjects.ToArray();
    }
}
