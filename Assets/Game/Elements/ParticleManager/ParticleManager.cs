using System.Collections.Generic;
using UnityEngine;

/// <summary> Processes requests to fetch a particle effect. </summary>
public class ParticleManager : MonoBehaviour
{
    [SerializeField] Particles particles = new();
    [SerializeField] int preallocateAmount = 2;

    [HideInInspector] Transform pool;

    readonly Queue<IParticleEffect> sparks = new();
    readonly Queue<IParticleEffect> explosions = new();

    public IParticleEffect GetParticleEffect(ParticleIDs effect) => TryDequeue(effect);

    public void OnSceneRefresh()
    {
        sparks.Clear();
        explosions.Clear();
        PreallocatePools();
    }

    IParticleEffect TryDequeue(ParticleIDs effect)
    {
        IParticleEffect newEffect = null;

        switch (effect)
        {
            case ParticleIDs.Sparks:
                if (sparks.Count < 1)
                    AddEffect(effect);
                newEffect = sparks.Dequeue();
                break;

            case ParticleIDs.Explosion:
                if (explosions.Count < 1)
                    AddEffect(effect);
                newEffect = explosions.Dequeue();
                break;
        }

        // Reset the parent and position before dispatching effect
        newEffect?.ISetParent(pool);
        newEffect?.ISetPosition(Vector3.zero);

        return newEffect;
    }

    void AddEffect(ParticleIDs id)
    {
        ParticleEffect newEffect = null;

        // Add a new effect to the queue, parented under the pool object
        switch (id)
        {
            case ParticleIDs.Sparks:
                newEffect = Instantiate(particles.SparksPF, Vector3.zero, Quaternion.identity, pool);
                break;

            case ParticleIDs.Explosion:
                newEffect = Instantiate(particles.ExplosionPF, Vector3.zero, Quaternion.identity, pool);
                break;
        }

        ReturnEffect(newEffect, id);
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

    void PreallocatePools()
    {
        if (pool != null)
            Destroy(pool.gameObject);

        // Set up a new gameObject to hold pooled effects
        pool = new GameObject().transform;
        pool.SetParent(transform);
        pool.name = "EffectsPool";

        for (int i = 0; i < preallocateAmount; i++)
        {
            AddEffect(ParticleIDs.Sparks);
            AddEffect(ParticleIDs.Explosion);
        }
    }

    void Awake() => PreallocatePools();

    void OnEnable() => ParticleEffect.OnReturnEffect += ReturnEffect;

    void OnDisable() => ParticleEffect.OnReturnEffect -= ReturnEffect;
}