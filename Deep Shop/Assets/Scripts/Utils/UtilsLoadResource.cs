using UnityEngine;

public static class UtilsLoadResource
{
    public static Sprite LoadSprite(string pathSprite)
    {
        Texture2D tex = (Texture2D)Resources.Load(pathSprite);
        return Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
}
