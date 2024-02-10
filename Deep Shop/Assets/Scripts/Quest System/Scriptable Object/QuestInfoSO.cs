using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Quest/New Quest Container")]
public class QuestInfoSO : ScriptableObject
{
    [SerializeField, ReadOnly] private string _id = Guid.NewGuid().ToString();
    [SerializeField] private string _idQuest;

    [Header("General")]
    [SerializeField] private string _nameQuest;

    [Header("Dialogues")]
    [SerializeField] private DialogueTextSO _startDialogueQuest;
    [SerializeField] private DialogueTextSO _finishDialogueQuest;

    [Header("Requirements")]
    [SerializeField] private float _karma;
    [SerializeField] private QuestInfoSO[] _questPrerequisites;

    [Header("Steps")]
    [SerializeField] private GameObject[] _segments;

    [Header("Rewards")]
    [SerializeField] private ItemQuantitySerialized _productReward;
    [SerializeField] private int _moneyReward;

    public string Id { get => _id; }
    public string IdQuest { get => _idQuest; }
    public string NameQuest { get => _nameQuest; }
    public DialogueTextSO StartDialogueQuest { get => _startDialogueQuest; }
    public DialogueTextSO FinishDialogueQuest { get => _finishDialogueQuest; }
    public float Karma { get => _karma; }
    public QuestInfoSO[] QuestPrerequisites { get => _questPrerequisites; }
    public GameObject[] Segments { get => _segments; }
    public ItemQuantitySerialized ProductReward { get => _productReward; }
    public int MoneyReward { get => _moneyReward; }

    private void OnValidate()
    {
#if UNITY_EDITOR
        _nameQuest = name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}