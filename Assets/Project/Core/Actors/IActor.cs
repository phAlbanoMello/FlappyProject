using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FlappyProject.Interfaces
{
    public interface IActor
    {
        public void Initialize();
        public void Destroy();
        public void Update(float deltaTime);
        public void EnableActions();
        public void DisableActions();
    }
}