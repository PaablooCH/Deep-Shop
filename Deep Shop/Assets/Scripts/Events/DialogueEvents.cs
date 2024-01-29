using System;

public class DialogueEvents
{
    public event Action<string> onFinishDialogue;
    public void FinishDialogue(string id)
    {
        if (onFinishDialogue != null)
        {
            onFinishDialogue(id);
        }
    }
}
