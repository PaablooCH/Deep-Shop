using UnityEngine;

public class QuestIcons : MonoBehaviour
{
    [SerializeField] private GameObject _requirementsNotMetIcon;
    [SerializeField] private GameObject _startQuestIcon;
    [SerializeField] private GameObject _inProgressIcon;
    [SerializeField] private GameObject _canFinishIcon;

    public void ChooseStateIcon(QuestState state, bool startPoint, bool finishPoint)
    {
        // Disable all
        _requirementsNotMetIcon.SetActive(false);
        _startQuestIcon.SetActive(false);
        _inProgressIcon.SetActive(false);
        _canFinishIcon.SetActive(false);

        // Enable only the correct state
        switch (state)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
                if (startPoint)
                {
                    _requirementsNotMetIcon.SetActive(true);
                }
                break;
            case QuestState.CAN_START:
                if (startPoint)
                {
                    _startQuestIcon.SetActive(true);
                }
                break;
            case QuestState.IN_PROGRESS:
                if (finishPoint)
                {
                    _inProgressIcon.SetActive(true);
                }
                break;
            case QuestState.CAN_FINISH:
                if (finishPoint)
                {
                    _canFinishIcon.SetActive(true);
                }
                break;
            case QuestState.FINISHED:
                break;
            default:
                Debug.LogWarning("Wrong state to display an icon.");
                break;
        }
    }
}
