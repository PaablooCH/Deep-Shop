using UnityEngine;

public class NPCTalk : NPC, ITalkable
{
    [SerializeField]
    private DialogueText _dialogueText;
    [SerializeField]
    private DialogueUI _dialogueUI;

    public override void Interact()
    {
        Talk(_dialogueText);
    }

    public void Talk(DialogueText dialogueText)
    {
        //start conversation
        _dialogueUI.NextDialogue(_dialogueText);
    }
}
