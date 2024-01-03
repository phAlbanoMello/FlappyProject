using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulRecoverySpawner : MonoBehaviour
{ 
    [SerializeField] protected GameObject ObjectToSpawn;
    [SerializeField] protected SpawnableObjectData InitialObjectData;

    private SoulsRecovery _soulsRecovery;
    
    public void Init()
    {
        GetSoulsRecoveryComponent();
        SubscribeEvents();
    }

    private void GetSoulsRecoveryComponent()
    {
        if (_soulsRecovery == null)
        {
            _soulsRecovery = Instantiate(ObjectToSpawn, transform).GetComponent<SoulsRecovery>();
        }
        _soulsRecovery.Init(InitialObjectData);
    }

    public void SubscribeEvents()
    {
        EventBus.Subscribe<GameStartedEvent>(HandleGameStarted);
        EventBus.Subscribe<PlayerDiedEvent>(HandlePlayerDeath);
    }
    public void UnsubscribeEvents()
    {
        EventBus.Unsubscribe<GameStartedEvent>(HandleGameStarted);
        EventBus.Unsubscribe<PlayerDiedEvent>(HandlePlayerDeath);
    }
    private void HandlePlayerDeath(PlayerDiedEvent @event)
    {
        if (_soulsRecovery._isEnabled)
        {
            _soulsRecovery._isEnabled = false;
            _soulsRecovery.CancelMovement();
            UnsubscribeEvents();
            return;
        }
        _soulsRecovery.SetScore(@event.Souls);
        UnsubscribeEvents();
    }

    private void HandleGameStarted(GameStartedEvent @event)
    {
        if (_soulsRecovery._isEnabled)
        {
            _soulsRecovery.SetGameStartTime(@event.gameStartTime);
            SetupSpawnRecovery();
        }
    }

    private void SetupSpawnRecovery()
    {
        if (_soulsRecovery != null)
        {
            _soulsRecovery.Init(InitialObjectData);
            _soulsRecovery.Move();
        }
    }
}
