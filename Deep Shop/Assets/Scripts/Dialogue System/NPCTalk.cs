using UnityEngine;

public class NPCTalk : NPC, ITalkable
{
    [SerializeField] private DialogueTextSO _dialogueText;

    public override void Interact()
    {
        Talk(_dialogueText);
    }

    public void Talk(DialogueTextSO dialogueText)
    {
        // Start conversation
        CanvasManager.instance.GetPanel(UIs.DIALOGUE).GetComponent<DialogueUI>().NextDialogue(dialogueText);
    }
}
