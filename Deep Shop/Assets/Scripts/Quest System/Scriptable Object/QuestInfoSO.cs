using UnityEngine;
using System;

public enum QuestType
{
    DELIVER,
    CRAFT
}

[CreateAssetMenu(menuName = "Quest/New Quest Container")]
public class QuestInfoSO : ScriptableObject
{
    [SerializeField, ReadOnly] private string _id = Guid.NewGuid().ToString();
    [SerializeField] private string _idQuest;
    
    public string Id { get => _id; }
    public string IdQuest { get => _idQuest; }

    [Header("General")]
    [SerializeField] private string _nameQuest;
    public string NameQuest { get => _nameQuest; }

    [Header("Dialogues")]
    public DialogueTextSO startDialogueQuest;
    public DialogueTextSO finishDialogueQuest;

    [Header("Requirements")]
    public int karma;
    public QuestInfoSO[] questPrerequisites;

    [Header("Steps")]
    public GameObject[] segments;

    [Header("Rewards")]
    public ProductQuantity _productReward;
    public int _moneyReward;

    private void OnValidate()
    {
#if UNITY_EDITOR
        _nameQuest = name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    //[SerializeField] private int _id;
    //[SerializeField] private string _name;
    //[SerializeField] private string _description;
    //[SerializeField] private int _karmaNeeded;
    //[SerializeField] private QuestInfoSO _nextQuest;
    //[SerializeField] private QuestInfoSO _previousQuest;
    //[SerializeField] private int _productReward; // don't know if GameObject class is necessary
    //[SerializeField] private int _moneyReward; // always positive
    //[SerializeField] private QuestType _questType;

    //// Type Deliver
    //[SerializeField] private ProductQuantity _productToDeliver;
    //[SerializeField] private int _whomToDeliver;

    //// Type Craft
    //[SerializeField] private ProductQuantity _productToCraft;

    //public int Id { get => _id; set => _id = value; }
    //public string Name { get => _name; set => _name = value; }
    //public string Description { get => _description; set => _description = value; }
    //public QuestType QuestType { get => _questType; set => _questType = value; }
    //public QuestInfoSO NextQuest { get => _nextQuest; set => _nextQuest = value; }
    //public QuestInfoSO PreviousQuest { get => _previousQuest; set => _previousQuest = value; }
    //public int ProductReward { get => _productReward; set => _productReward = value; }
    //public int MoneyReward { get => _moneyReward; set => _moneyReward = value; }
    //public int KarmaNeeded { get => _karmaNeeded; set => _karmaNeeded = value; }

    //public ProductQuantity ProductToDeliver { get => _productToDeliver; set => _productToDeliver = value; }
    //public int WhomToDeliver { get => _whomToDeliver; set => _whomToDeliver = value; }

    //public ProductQuantity ProductToCraft { get => _productToCraft; set => _productToCraft = value; }
}

//[CustomEditor(typeof(QuestInfoSO))]
//public class QuestEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        QuestInfoSO quest = (QuestInfoSO)target;

//        // Display main attributes
//        EditorGUILayout.PropertyField(serializedObject.FindProperty("_id"));
//        EditorGUILayout.PropertyField(serializedObject.FindProperty("_name"));
//        EditorGUILayout.PropertyField(serializedObject.FindProperty("_description"));
//        EditorGUILayout.PropertyField(serializedObject.FindProperty("_karmaNeeded"));
//        EditorGUILayout.PropertyField(serializedObject.FindProperty("_nextQuest"));
//        EditorGUILayout.PropertyField(serializedObject.FindProperty("_previousQuest"));
//        EditorGUILayout.PropertyField(serializedObject.FindProperty("_productReward"));
//        EditorGUILayout.PropertyField(serializedObject.FindProperty("_moneyReward"));

//        // Display dropdown
//        quest.QuestType = (QuestType)EditorGUILayout.EnumPopup("Quest Type", quest.QuestType);

//        // Display conditional for DELIVER
//        if (quest.QuestType == QuestType.DELIVER)
//        {
//            EditorGUILayout.PropertyField(serializedObject.FindProperty("_productToDeliver"), true);
//            EditorGUILayout.PropertyField(serializedObject.FindProperty("_whomToDeliver"));
//        }

//        // Display conditional for CRAFT
//        else if (quest.QuestType == QuestType.CRAFT)
//        {
//            EditorGUILayout.PropertyField(serializedObject.FindProperty("_productToCraft"), true);
//        }

//        // Apply modifications
//        serializedObject.ApplyModifiedProperties();
//    }
//}