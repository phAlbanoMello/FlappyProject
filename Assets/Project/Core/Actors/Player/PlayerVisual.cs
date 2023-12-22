using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerVisual
{
    private GameObject _playerGameObject;
    public Transform SoulDrainPosition { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    private bool _isHidden;

    private ParticleSystem _playerTrail;
    private GameObject _vfxParent;
    private VFXSpawner _damageVFXSpawner;
    private VFXSpawner _deathVFXSpawner;
    private VFXSpawner _soulPickUpFXSpawner;
    private VFXSpawner _soulRecoverFXSpawner;

    private List<VFXParticles> _particles = new List<VFXParticles>();

    public PlayerVisual(GameObject playerGameObject, Transform soulDrainPosition, GameObject vfxParent)
    {
        _playerGameObject = playerGameObject;
        _vfxParent = vfxParent;
        SoulDrainPosition = soulDrainPosition;

        GetDependencies();
        InitVFXParticles();
        CreateVFXSpawners();
        SubscribeEvents();
    }

    private void InitVFXParticles()
    {
        foreach (VFXParticles vFXParticles in _particles)
        {
            vFXParticles.Init();
        }
    }

    private void SubscribeEvents()
    {
        EventBus.Subscribe<PlayerCollidedEvent>(HandlePlayerCollided);
        EventBus.Subscribe<PlayerDiedEvent>(HandlePlayerDeath);
    }

    private void HandlePlayerDeath(PlayerDiedEvent @event)
    {
        Hide();
        _deathVFXSpawner.SpawnVFX(_playerGameObject.transform);
        UnsubscribeEvents();
    }

    private void UnsubscribeEvents()
    {
        EventBus.Unsubscribe<PlayerCollidedEvent>(HandlePlayerCollided);
        EventBus.Unsubscribe<PlayerDiedEvent>(HandlePlayerDeath);
    }

    private void GetDependencies()
    {
        _playerTrail = _playerGameObject.GetComponentInChildren<ParticleSystem>(true);
        SpriteRenderer = _playerGameObject.GetComponentInChildren<SpriteRenderer>(true);
        _particles = _vfxParent.GetComponentsInChildren<VFXParticles>(true).ToList();
    }

    private void CreateVFXSpawners()
    {
        //TODO:I'm using strings because I don't want to rely on scriptableObjects
        //I should at least turn them to constants one day.
        _damageVFXSpawner = new VFXSpawner(GetParticleSystemById("Damage"));
        _deathVFXSpawner = new VFXSpawner(GetParticleSystemById("Death"));
        _soulPickUpFXSpawner = new VFXSpawner(GetParticleSystemById("SoulPickUp"));
        _soulRecoverFXSpawner = new VFXSpawner(GetParticleSystemById("SoulRecover"));
    }

    private ParticleSystem GetParticleSystemById(string id)
    {
        foreach (VFXParticles ps in _particles)
        {
            if (ps.ID == id)
            {
                ParticleSystem particleSystem = ps.ParticleSystem;
                if (particleSystem == null)
                {
                    Debug.LogError($"ParticleSystem of Id {ps.ID} has {particleSystem} as ps");
                }
                return particleSystem;
            }
        }
        Debug.LogError("Particle system with Id not found: " + id);
        return null;
    }

    private void HandlePlayerCollided(PlayerCollidedEvent @event)
    {
        switch (@event._collidingObject.tag)
        {
            case "Obstacle":
                _damageVFXSpawner.SpawnVFX(_playerGameObject.transform);
               
            break;
            case "BasicSoul":
                _soulPickUpFXSpawner.SpawnVFX(SoulDrainPosition);
                
                break;
            case "SoulRecovery":
                _soulRecoverFXSpawner.SpawnVFX(SoulDrainPosition);
      
                break;
            default:
                break;
        }
    }

    public void Hide()
    {
        SpriteRenderer.gameObject.SetActive(false);
        _playerTrail.Stop();
        _isHidden = true;
    }

    public void Show()
    {
        SpriteRenderer.gameObject.SetActive(true);
        _playerTrail.Play();
        _isHidden = false;
    }
}
