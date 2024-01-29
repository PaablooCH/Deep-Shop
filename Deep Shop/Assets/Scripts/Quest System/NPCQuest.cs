using UnityEngine;

public class NPCQuest : NPC, IQuest, ITalkable
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
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;

        GameEventsManager.instance.dialogueEvents.onFinishDialogue += FinishDialogue;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;

        GameEventsManager.instance.dialogueEvents.onFinishDialogue -= FinishDialogue;
    }

    public override void Interact()
    {
       Quest(_questInfo);
    }

    public void Quest(QuestInfoSO questInfo)
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
                GameEventsManager.instance.questEvents.StartQuest(_questId);
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
                GameEventsManager.instance.questEvents.FinishQuest(_questId);
            }
        }
    }

    public void Talk(DialogueTextSO dialogueText)
    {
        CanvasManager.instance.GetPanel(UIs.DIALOGUE).GetComponent<DialogueUI>().NextDialogue(dialogueText);
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
            GameEventsManager.instance.questEvents.StartQuest(_questId);
        }

        else if (CheckIfCanFinish() && dialogueId == _questInfo.FinishDialogueQuest?.Id)
        {
            GameEventsManager.instance.questEvents.FinishQuest(_questId);
        }
    }
}
