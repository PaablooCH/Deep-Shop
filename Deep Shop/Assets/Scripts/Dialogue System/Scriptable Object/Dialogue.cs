using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public Sprite sprite;

    [TextArea(5, 10)]
    public string text;
}