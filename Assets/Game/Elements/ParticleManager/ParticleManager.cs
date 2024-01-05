using System.Collections.Generic;
using UnityEngine;

/// <summary> Processes requests to fetch a particle effect. </summary>
public class ParticleManager : MonoBehaviour
{
    [SerializeField] Particles particles = new();
    [HideInInspector] Transform pool;

    Queue<IParticleEffect> sparks = new();
    Queue<IParticleEffect> explosions = new();

    public IParticleEffect GetParticleEffect(ParticleIDs effect) => TryDequeue(effect);

    IParticleEffect TryDequeue(ParticleIDs effect)
    {
        IParticleEffect newEffect = null;

        switch (effect)
        {
            case ParticleIDs.Sparks:
                // Debug.Log($"sparks has {sparks.Count} items in pool");
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
                sparks.Enqueue(newEffect);
                break;

            case ParticleIDs.Explosion:
                newEffect = Instantiate(particles.ExplosionPF, Vector3.zero, Quaternion.identity, pool);
                explosions.Enqueue(newEffect);
                break;
        }

        newEffect.gameObject.SetActive(false);
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