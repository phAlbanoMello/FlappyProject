using UnityEngine;
using FlappyProject.Interfaces;
using FlappyProject.Managers;
using System.Collections.Generic;

[System.Serializable]
public class SystemManager : MonoBehaviour
{ 
    private PlayerManager _playerManager;

    private readonly List<IManager> _managers = new List<IManager>();

    void OnEnable()
    {
        LoadManagersFromChildren();

        _playerManager = GetManagerOfType<PlayerManager>();
        
        InitializeManagers();
    }

    private void InitializeManagers()
    {
        foreach (var manager in _managers)
        {
            if (manager.ShouldInitializeAtStart)
            {
                manager.Init();
            }
        }
    }

    private void LoadManagersFromChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            IManager manager = child.GetComponent<IManager>();
            if (manager != null)
            {
                _managers.Add(manager);
            }
        }
    }

    T GetManagerOfType<T>() where T : class, IManager
    {
        return _managers.Find(obj => obj.GetType() == typeof(T)) as T;
    }

    void Update()
    {
        if (_playerManager.HasInitiated)
        {
            _playerManager.UpdateManager(Time.deltaTime);
        }
    }  
}
