using System;

public class NPCEvents
{
    public event Action<string> onNPCExit;
    public void NPCExit(string tag)
    {
        if (onNPCExit != null)
        {
            onNPCExit(tag);
        }
    }
}
