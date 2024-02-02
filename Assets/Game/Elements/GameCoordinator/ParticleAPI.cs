using UnityEngine;

// Handles requests for particle effects
public partial class GameAPI
{
    [SerializeField] ParticleManager particleManager;

    public IParticleEffect RequestParticleEffect(ParticleIDs effect)
    {
        IParticleEffect newEffect = null;

        if (particleManager != null)
            newEffect = particleManager.GetParticleEffect(effect);

        return newEffect;
    }
}