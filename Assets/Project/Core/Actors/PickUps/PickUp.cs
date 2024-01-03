using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickUp : BasicObject
{
    private List<ParticleSystem> _particleSystems = new List<ParticleSystem>();
    private bool _isDisabled = false;

    private void Awake()
    {
        _particleSystems = FindChildParticleSystems();
    }

    public override void Init(SpawnableObjectData data)
    {
        base.Init(data);
    }

    private List<ParticleSystem> FindChildParticleSystems()
    {
        foreach (Transform child in transform)
        {
            ParticleSystem ps = child.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                _particleSystems.Add(ps);
            }
        }
        return _particleSystems;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _isDisabled == false)
        {
            DisablePickUp();
        }
    }

    protected virtual void DisablePickUp()
    {
        if (!_isDisabled && _particleSystems.Count > 0)
        {
            foreach (ParticleSystem ps in _particleSystems)
            {
                ps.Stop();
            }

            _isDisabled = true;
        }
    }

    public void EnablePickUp()
    {
        if (_isDisabled && _particleSystems.Count > 0)
        {
            foreach (ParticleSystem ps in _particleSystems)
            {
                ps.Play();
            }

            _isDisabled = false;
        }
    }

    protected override void ResetFX()
    {
        EnablePickUp();
    }
}


