public interface IPersistenceData
{
    void SaveData(ref GameData data);

    void LoadData(GameData data);
}
