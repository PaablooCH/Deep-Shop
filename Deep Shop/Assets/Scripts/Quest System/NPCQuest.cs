using UnityEngine;

public class NPCQuest : NPC, IQuestGiver, ITalkable
{
    [Header("Quest")]
    [SerializeField] private QuestInfoSO _questInfo;

    [Header("Config")]
    [SerializeField] private bool _startQuest;
    [SerializeField] private bool _finishQuest;
    
    private QuestIcons _questIcons;

    private string _questId;
    private QuestState _questState;

    private void Awake()
    {
        _questId = _questInfo.IdQuest;
        _questIcons = GetComponentInChildren<QuestIcons>();
    }

    private void OnEnable()
    {
        GameEventsMediator.instance.questEvents.onQuestStateChange += QuestStateChange;

        GameEventsMediator.instance.dialogueEvents.onFinishDialogue += FinishDialogue;
    }

    private void OnDisable()
    {
        GameEventsMediator.instance.questEvents.onQuestStateChange -= QuestStateChange;

        GameEventsMediator.instance.dialogueEvents.onFinishDialogue -= FinishDialogue;
    }

    public override void Interact()
    {
       QuestGiver(_questInfo);
    }

    public void QuestGiver(QuestInfoSO questInfo)
    {
        if (CheckIfCanStart())
        {
            // If the quest starts with a dialog play it
            if (_questInfo.StartDialogueQuest)
            {
                Talk(_questInfo.StartDialogueQuest);
            }
            // If not start it directly
            else
            {
                GameEventsMediator.instance.questEvents.StartQuest(_questId);
            }
        }
        else if (CheckIfCanFinish())
        {
            // If the quest ends with a dialog play it
            if (_questInfo.FinishDialogueQuest)
            {
                Talk(_questInfo.FinishDialogueQuest);
            }
            // If not end it directly
            else
            {
                GameEventsMediator.instance.questEvents.FinishQuest(_questId);
            }
        }
    }

    public void Talk(DialogueTextSO dialogueText)
    {
        UIManager.instance.GetPanel(UIs.DIALOGUE).GetComponent<DialogueUI>().NextDialogue(dialogueText);
    }

    private bool CheckIfCanStart()
    {
        return _questState.Equals(QuestState.CAN_START) && _startQuest;
    }

    private bool CheckIfCanFinish()
    {
        return _questState.Equals(QuestState.CAN_FINISH) && _finishQuest;
    }

    private void QuestStateChange(Quest quest)
    {
        if (quest.QuestInfo.IdQuest == _questId)
        {
            _questState = quest.State;
            _questIcons.ChooseStateIcon(_questState, _startQuest, _finishQuest);
        }
    }

    private void FinishDialogue(string dialogueId)
    {
        if (CheckIfCanStart() && dialogueId == _questInfo.StartDialogueQuest?.Id)
        {
            GameEventsMediator.instance.questEvents.StartQuest(_questId);
        }

        else if (CheckIfCanFinish() && dialogueId == _questInfo.FinishDialogueQuest?.Id)
        {
            GameEventsMediator.instance.questEvents.FinishQuest(_questId);
        }
    }
}
