using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour, IPersistenceData
{
    #region Singleton
    public static QuestManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Quest Manager singleton already exists.");
            return;
        }
        instance = this;
        _questMap = CreateQuestMap();
    }
    #endregion

    private Dictionary<string, Quest> _questMap;

    private float _karma;

    private void Start()
    {
        foreach (Quest quest in _questMap.Values)
        {
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }

        _karma = InventoryManager.instance.Karma;
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        // Get all the Scriptable Objects from the quests folder
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");

        // Insert all the quests into a dictionary
        Dictionary<string, Quest> auxDictionary = new();
        foreach (QuestInfoSO info in allQuests)
        {
            if (auxDictionary.ContainsKey(info.IdQuest))
            {
                Debug.LogWarning("The IdQuest: " + info.IdQuest + " has been found repeated, while creating the Quest Map.");
                continue;
            }
            auxDictionary.Add(info.IdQuest, new Quest(info));
        }

        //return the dictionary
        return auxDictionary;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;
        GameEventsManager.instance.questEvents.onQuestSegmentStateChange += UpdateSegmentState;

        GameEventsManager.instance.inventoryEvent.onKarmaChanged += UpdateKarma;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;
        GameEventsManager.instance.questEvents.onQuestSegmentStateChange -= UpdateSegmentState;

        GameEventsManager.instance.inventoryEvent.onKarmaChanged -= UpdateKarma;
    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.State = state;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }

    private void StartQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.InstantiateSegment(transform);
        ChangeQuestState(quest.QuestInfo.IdQuest, QuestState.IN_PROGRESS);
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.MoveToNextSegment();
        // If next segment exists, instatiate it, if not, the quest can be finished
        if (quest.CurrentSegmentExists())
        {
            quest.InstantiateSegment(transform);
        }
        else
        {
            ChangeQuestState(id, QuestState.CAN_FINISH);
        }
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        ClaimReward(quest);
        ChangeQuestState(id, QuestState.FINISHED);
        CheckConditions();
    }

    private void ClaimReward(Quest quest)
    {
        InventoryManager.instance.Money += quest.QuestInfo.MoneyReward;
        InventoryManager.instance.ModifyInventory(quest.QuestInfo.ProductReward.itemInfo.IdItem,
            quest.QuestInfo.ProductReward.quantity);
    }

    private void UpdateKarma(float newKarma)
    {
        _karma = newKarma;
        CheckConditions();
    }

    private void CheckConditions()
    {
        // Check if some Quest can start now that some requirements are updated
        foreach (Quest quest in _questMap.Values)
        {
            if (quest.State.Equals(QuestState.REQUIREMENTS_NOT_MET) && CheckRequirements(quest))
            {
                ChangeQuestState(quest.QuestInfo.IdQuest, QuestState.CAN_START);
            }
        }
    }

    private bool CheckRequirements(Quest quest)
    {
        // Check Karmar (good and evil)
        if (quest.QuestInfo.Karma >= 0)
        {
            if (quest.QuestInfo.Karma > _karma)
            {
                return false;
            }
        }
        else
        {
            if (quest.QuestInfo.Karma < _karma)
            {
                return false;
            }
        }

        // See if the prerequisites quest are finished
        foreach (QuestInfoSO requirement in quest.QuestInfo.QuestPrerequisites)
        {
            Quest requirementQuest = GetQuestById(requirement.IdQuest);
            if (!requirementQuest.State.Equals(QuestState.FINISHED))
            {
                return false;
            }
        }

        return true;
    }

    public Quest GetQuestById(string id)
    {
        Quest requested = _questMap[id];
        if (requested == null)
        {
            Debug.LogError("Quest with IdQuest: " + id + " doesn't exist.");
        }
        return requested;
    }

    private void UpdateSegmentState(string idQuest, int segment, QuestSegmentState state)
    {
        Quest quest = GetQuestById(idQuest);
        quest.UpdateSegmentState(segment, state);
    }

    public void SaveData(ref GameData data)
    {
        Dictionary<string, QuestData> dataMap = new();
        foreach (string idQuest in _questMap.Keys)
        {
            Quest quest = GetQuestById(idQuest);
            QuestData questData = quest.Save();
            dataMap.Add(idQuest, questData);
        }
        data.questsData = dataMap;
    }

    public void LoadData(GameData data)
    {
        foreach (string idQuest in data.questsData.Keys)
        {
            // Save the state
            Quest quest = _questMap[idQuest];
            quest.Load(data.questsData[idQuest]);

            // Propagate Quest State
            GameEventsManager.instance.questEvents.QuestStateChange(quest);

            // If quest is In Progress instantiate segment
            if (quest.State == QuestState.IN_PROGRESS)
            {
                quest.InstantiateSegment(transform);
            }
        }

        // Check if some quests can be initialize after load
        CheckConditions();
    }
}
