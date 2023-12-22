using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class VFXParticles : MonoBehaviour
{
    [SerializeField] private string _iD;
    public ParticleSystem ParticleSystem { get; private set; }
    public string ID { get => _iD; }

    internal void Init()
    {
        ParticleSystem = GetComponent<ParticleSystem>();

        if (ParticleSystem == null)
        {
            Debug.LogError("ParticleSystem component not found on the GameObject.");
        }
    }

    public void Play()
    {
        if (ParticleSystem != null && !ParticleSystem.isPlaying)
        {
            ParticleSystem.Play();
        }
    }

    public void Stop()
    {
        if (ParticleSystem != null && ParticleSystem.isPlaying)
        {
            ParticleSystem.Stop();
        }
    }
}
