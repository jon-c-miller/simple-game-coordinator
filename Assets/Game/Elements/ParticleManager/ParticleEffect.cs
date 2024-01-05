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
        Debug.Log($"Setting active...");
        effect.Play(true);
    }

    public void ISetInactive()
    {
        effect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        // effect.Clear();
        Debug.Log($"Setting inactive...");
    }

    public void ISetParent(Transform newParent) => transform.SetParent(newParent);

    public void ISetPosition(Vector3 newPosition) => transform.localPosition = newPosition;

    void Awake() => TryGetComponent(out effect);

    void OnDisable() => OnReturnEffect?.Invoke(this, effectType);
}