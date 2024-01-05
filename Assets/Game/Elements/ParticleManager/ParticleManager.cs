using UnityEngine;

/// <summary> Processes requests to display a particle effect. </summary>
public class ParticleManager : MonoBehaviour
{
    [SerializeField] Particles particles = new();
    
    public ParticleSystem DisplayParticleEffect(ParticleIDs effect, Transform parent)
    {
        ParticleSystem newEffect = null;

        switch (effect)
        {
            case ParticleIDs.Sparks:
                newEffect = particles.Sparks.Dequeue();
                break;

            case ParticleIDs.Explosion:
                newEffect = particles.Explosions.Dequeue();
                break;
        }

        // Set the new parent and return the requested particle system
        if (newEffect != null)
            newEffect.transform.SetParent(parent);
        
        return newEffect;
    }
}

public enum ParticleIDs
{
    Sparks,
    Explosion,
}