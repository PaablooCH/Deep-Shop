using UnityEngine;

public interface IUIConfirmation : IUI
{
    // Any UI that requieres a confirmation before apply an action
    public abstract void Confirm();
}
