using UnityEngine;

public class VFXSpawner
{
    private ParticleSystem _vfxParticleSystem;

    public VFXSpawner(ParticleSystem particleSystem)
    {
        _vfxParticleSystem = particleSystem;
    }

    public void SpawnVFX(Transform spawnPosition)
    {
        _vfxParticleSystem.transform.position = spawnPosition.position;

        _vfxParticleSystem.Play();

        float duration = _vfxParticleSystem.main.duration;

        LeanTween.delayedCall(duration, () =>
        {
            StopVFX();
        });
    }


    private void StopVFX()
    {
        _vfxParticleSystem.Stop();
    }
}

