using UnityEngine;

namespace FlappyProject.Interfaces
{
    public interface IManager
    {
        public void Init();
        public void UpdateManager(float deltaTime);
        public void Stop();
    }
}