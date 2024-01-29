using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/New Dialogue Container")]
public class DialogueTextSO : ScriptableObject
{
    [System.Serializable]
    public class Dialogue
    {
        public Sprite sprite;

        [TextArea(5, 10)]
        public string text;
    }

    [SerializeField, ReadOnly] private string _id = Guid.NewGuid().ToString();
    [SerializeField] private string _idDialogue;

    public string Id { get => _id; }
    public string IdDialogue { get => _idDialogue; }

    [Header("General")]
    [SerializeField] private string _speakerName;

    [SerializeField] private Dialogue[] _dialogues;

    public string SpeakerName { get => _speakerName; set => _speakerName = value; }
    public Dialogue[] Dialogues { get => _dialogues; set => _dialogues = value; }
}
