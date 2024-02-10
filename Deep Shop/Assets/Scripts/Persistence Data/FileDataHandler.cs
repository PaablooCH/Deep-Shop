using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

public class FileDataHandler
{
    private string _dataDirPath = "";
    private string _dataNameFile = "";
    private bool _useEncryption = false;
    private readonly string _encryptionCodeWord = "cr0CoD1l3";

    public FileDataHandler(string dataDirPath, string dataNameFile, bool useEncryption)
    {
        _dataDirPath = dataDirPath;
        _dataNameFile = dataNameFile;
        _useEncryption = useEncryption;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(_dataDirPath, _dataNameFile);
        GameData gameData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataFromJSON = "";
                // Write data into file, using "using" is a safe method to encapsulate the connection to the file inside the block
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataFromJSON = reader.ReadToEnd();
                    }
                }

                if (_useEncryption)
                {
                    dataFromJSON = EncryptDecrypt(dataFromJSON);
                }

                // Deserialize from JSON
                gameData = JsonConvert.DeserializeObject<GameData>(dataFromJSON);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return gameData;
    }

    public void Save(GameData gameData)
    {
        string fullPath = Path.Combine(_dataDirPath, _dataNameFile);
        try
        {
            // Create directory if not exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // Set up SerializerSettings
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            // Serialize the info into a JSON
            string dataJSON = JsonConvert.SerializeObject(gameData, settings);

            if (_useEncryption)
            {
                dataJSON = EncryptDecrypt(dataJSON);
            }

            // Write data into file, using "using" is a safe method to encapsulate the connection to the file inside the block
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataJSON);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    // Encrypt using a XOR encryption
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";

        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ _encryptionCodeWord[i % _encryptionCodeWord.Length]);
        }

        return modifiedData;
    }
}
