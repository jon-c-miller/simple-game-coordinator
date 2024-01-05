using UnityEngine;

/// <summary> Acts as a generic particle class to allow any particle system to be used, using effectType to filter for pooling. </summary>
[RequireComponent (typeof(ParticleSystem))]
public class ParticleEffect : MonoBehaviour, IParticleEffect
{
    [SerializeField] ParticleIDs effectType;

    ParticleSystem effect;

	public static event System.Action<IParticleEffect, ParticleIDs> OnReturnEffect;

    public ParticleSystem IEffect => effect;

    public void ISetActive()
    {
        gameObject.SetActive(true);
        effect.Play(true);
    }

    public void ISetInactive()
    {
        // Particle system's Stop Action being set to Disable disables this object when the current particles are done 
        effect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    public void ISetParent(Transform newParent)
    {
        transform.SetParent(newParent);
        transform.localPosition = Vector3.zero;
    }

    public void ISetPosition(Vector3 newPosition) => transform.localPosition = newPosition;

    void Awake() => TryGetComponent(out effect);

    void OnDisable() => OnReturnEffect?.Invoke(this, effectType);
}