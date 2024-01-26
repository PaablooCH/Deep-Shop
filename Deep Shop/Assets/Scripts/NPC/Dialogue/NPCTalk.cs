using UnityEngine;

public class NPCTalk : NPC, ITalkable
{
    [SerializeField] private DialogueTextSO _dialogueText;
    [SerializeField] private DialogueUI _dialogueUI;

    public override void Interact()
    {
        Talk(_dialogueText);
    }

    public void Talk(DialogueTextSO dialogueText)
    {
        //start conversation
        _dialogueUI.NextDialogue(_dialogueText);
    }
}
