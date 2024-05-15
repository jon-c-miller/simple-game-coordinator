/// <summary> Conceals the type of particle system, preventing the need for a class for every type of particle effect. </summary>
public interface IParticleEffect
{
    UnityEngine.ParticleSystem IEffect { get; }
    void ISetActive();
    void ISetInactive();
    void ISetParent(UnityEngine.Transform newParent);
    void ISetPosition(UnityEngine.Vector3 newPosition);
}