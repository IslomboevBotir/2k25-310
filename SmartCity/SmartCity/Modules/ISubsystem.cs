namespace Modules
{
    public interface ISubsystem
    {
        string Status { get; }
        void Start();
        void Stop();
        void Toggle();
    }
}
