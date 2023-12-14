using UnityEngine;
using FlappyProject.Interfaces;
using FlappyProject.Managers;
using System.Collections.Generic;

[System.Serializable]
public class SystemManager : MonoBehaviour
{ 
    private PlayerManager playerManager;
    private ObstaclesManager obstacleManager;
    private ScoringManager scoringManager;

    private List<IManager> managers = new List<IManager>();

    void OnEnable()
    {
        LoadManagersFromChildren();

        playerManager = GetManagerOfType<PlayerManager>();
        obstacleManager = GetManagerOfType<ObstaclesManager>();
        scoringManager = GetManagerOfType<ScoringManager>();

        playerManager.SubscribeToPlayerCollisionEvent(scoringManager.GetPlayerDamagedHandler());
        
        InitializeManagers();
    }

    private void InitializeManagers()
    {
        foreach (var manager in managers)
        {
            manager.Init();
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
                managers.Add(manager);
            }
        }
    }

    T GetManagerOfType<T>() where T : class, IManager
    {
        return managers.Find(obj => obj.GetType() == typeof(T)) as T;
    }

    void Update()
    {
        playerManager.UpdateManager(Time.deltaTime);
    }

    void OnDisable() { 
    }    
}
