
namespace FlappyProject.Interfaces
{
    public interface IManager
    {
        public bool ShouldInitializeAtStart { get; }
        public bool HasInitiated { get; }
        public void Init();
        public void UpdateManager(float deltaTime);
        public void Stop();
    }
}