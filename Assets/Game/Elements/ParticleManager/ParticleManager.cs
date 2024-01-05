using System.Collections.Generic;
using UnityEngine;

/// <summary> Processes requests to fetch a particle effect. </summary>
public class ParticleManager : MonoBehaviour
{
    [SerializeField] Particles particles = new();
    [HideInInspector] Transform pool;

    public Queue<IParticleEffect> Sparks = new();
    public Queue<IParticleEffect> Explosions = new();

    public IParticleEffect GetParticleEffect(ParticleIDs effect, Transform parent)
    {
        IParticleEffect newEffect = null;

        switch (effect)
        {
            case ParticleIDs.Sparks:
                newEffect = Sparks.Dequeue();
                break;

            case ParticleIDs.Explosion:
                newEffect = Explosions.Dequeue();
                break;
        }

        return newEffect;
    }

    void ReturnEffect(IParticleEffect effect, ParticleIDs id)
    {
        switch (id)
        {
            case ParticleIDs.Sparks:
                Sparks.Enqueue(effect);
                break;

            case ParticleIDs.Explosion:
                Explosions.Enqueue(effect);
                break;
        }
    }

    void Awake()
    {
        // Set up a gameObject to hold pooled effects
        pool = new GameObject().transform;
        pool.SetParent(transform);
        pool.name = "EffectsPool";
    }

    void OnEnable() => ParticleEffect.OnReturnEffect += ReturnEffect;

    void OnDisable() => ParticleEffect.OnReturnEffect -= ReturnEffect;
}