public interface IUIReject : IUI
{
    // Any UI that requieres a reject before apply an action
    public abstract void Reject();
}
