using UnityEngine;

/// <summary> Acts as a proxy for areas of the codebase that would request particle effects through GameCoordinator. </summary>
public class ParticleActivator : MonoBehaviour
{
    [SerializeField] KeyCode explosionKey = KeyCode.Alpha1;
    [SerializeField] KeyCode sparksEnableKey = KeyCode.Alpha2;
    [SerializeField] KeyCode sparksDisableKey = KeyCode.Alpha3;
    [SerializeField] KeyCode sparksReparentKey = KeyCode.Alpha4;
    [Space]
    [SerializeField] Transform otherObject;
    
    IParticleEffect retrievedSparks;

    void Update()
    {
        if (Input.GetKeyDown(explosionKey))
        {
            // Simulates any scenario which requires an explosion at an object's position
            IParticleEffect effect = GameCoordinator.Instance.RequestEffect(ParticleIDs.Explosion);
            effect.ISetPosition(otherObject.localPosition);
            effect.ISetActive();
        }
        else if (Input.GetKeyDown(sparksEnableKey))
        {
            // Simulates a scenario where an object (maybe a building in an RTS) keeps track of the effect for later dismissal
            retrievedSparks = GameCoordinator.Instance.RequestEffect(ParticleIDs.Sparks);
            retrievedSparks.ISetActive();
        }
        else if (Input.GetKeyDown(sparksDisableKey))
        {
            // Simulates a scenario where an object has been repaired, destroyed, etc., and no longer needs the effect
            retrievedSparks?.ISetInactive();
        }
        else if (Input.GetKeyDown(sparksReparentKey))
        {
            // Simulates a scenario where a moving object needs the effect to follow its motion
            retrievedSparks?.ISetParent(otherObject);
        }
    }
}