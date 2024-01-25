using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/New Dialogue Container")]
public class DialogueText : ScriptableObject
{
    [System.Serializable]
    public class Dialogue
    {
        public Sprite sprite;

        [TextArea(5, 10)]
        public string text;
    }

    [SerializeField] private string _speakerName;

    [SerializeField] private Dialogue[] _dialogues;

    public string SpeakerName { get => _speakerName; set => _speakerName = value; }
    public Dialogue[] Dialogues { get => _dialogues; set => _dialogues = value; }
}
