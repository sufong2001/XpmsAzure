namespace Xpms.Core.Message
{
    public interface IComposable<out T>
    {
         T Compose();
    }
}
