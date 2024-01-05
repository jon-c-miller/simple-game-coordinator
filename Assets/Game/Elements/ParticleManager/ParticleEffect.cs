using UnityEngine;

/// <summary> Acts as a generic particle class to allow any particle system to be used, using effectType to filter for pooling. </summary>
[RequireComponent (typeof(ParticleSystem))]
public class ParticleEffect : MonoBehaviour, IParticleEffect
{
    [SerializeField] ParticleIDs effectType;

    ParticleSystem effect;

	public static event System.Action<IParticleEffect, ParticleIDs> OnReturnEffect;

    public ParticleSystem IEffect => effect;

    public void ISetParent(Transform newParent) => transform.SetParent(newParent);

    public void ISetPosition(Vector3 newPosition) => transform.localPosition = newPosition;

    public void IReturnEffect()
    {
        OnReturnEffect?.Invoke(this, effectType);
        gameObject.SetActive(false);
    }

    void Awake() => TryGetComponent(out effect);
}