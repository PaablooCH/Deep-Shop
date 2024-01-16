using UnityEngine;

[System.Serializable]
public class MyColor
{
    [Range(0, 255)]
    public int r = 255;
    [Range(0, 255)]
    public int g = 255;
    [Range(0, 255)]
    public int b = 255;
    [Range(0, 255)]
    public int a = 255;
}