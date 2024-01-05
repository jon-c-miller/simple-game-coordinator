using System.Collections.Generic;
using UnityEngine;

/// <summary> Processes requests to fetch a particle effect. </summary>
public class ParticleManager : MonoBehaviour
{
    [SerializeField] Particles particles = new();
    [HideInInspector] Transform pool;

    Queue<IParticleEffect> sparks = new();
    Queue<IParticleEffect> explosions = new();

    public IParticleEffect GetParticleEffect(ParticleIDs effect)
    {
        IParticleEffect newEffect = null;

        switch (effect)
        {
            case ParticleIDs.Sparks:
                newEffect = sparks.Dequeue();
                break;

            case ParticleIDs.Explosion:
                newEffect = explosions.Dequeue();
                break;
        }

        return newEffect;
    }

    void ReturnEffect(IParticleEffect effect, ParticleIDs id)
    {
        switch (id)
        {
            case ParticleIDs.Sparks:
                sparks.Enqueue(effect);
                break;

            case ParticleIDs.Explosion:
                explosions.Enqueue(effect);
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