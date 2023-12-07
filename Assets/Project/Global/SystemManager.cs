using UnityEngine;
using FlappyProject.Interfaces;
using FlappyProject.Managers;
using System.Collections.Generic;

[System.Serializable]
public class SystemManager : MonoBehaviour
{ 
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private ObstaclesManager obstacleManager;


    private List<IManager> managers = new List<IManager>();

    void OnEnable()
    {
        managers.Add(playerManager); 
        managers.Add(obstacleManager);

        foreach (var manager in managers)
        {
            manager.Init();
        }
    }

    void Update()
    {
        playerManager.UpdateManager(Time.deltaTime);
    }

    void OnDisable() { 
    }    
}
