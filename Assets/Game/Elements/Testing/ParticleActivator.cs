using UnityEngine;

/// <summary> Acts as a proxy for areas of the codebase that would request particle effects through GameCoordinator. </summary>
public class ParticleActivator : MonoBehaviour
{
    [SerializeField] KeyCode explosionKey = KeyCode.Alpha1;
    [SerializeField] KeyCode sparksEnableKey = KeyCode.Alpha2;
    [SerializeField] KeyCode sparksDisableKey = KeyCode.Alpha3;
    [SerializeField] KeyCode sparksReparentKey = KeyCode.Alpha4;
    [Space]
    [SerializeField] Transform sparksParent;
    
    IParticleEffect retrievedSparks;

    void Update()
    {
        if (Input.GetKeyDown(explosionKey))
        {
            GameCoordinator.Instance.RequestEffect(ParticleIDs.Explosion);
        }
        else if (Input.GetKeyDown(sparksEnableKey))
        {
            retrievedSparks = GameCoordinator.Instance.RequestEffect(ParticleIDs.Sparks);
            retrievedSparks.ISetActive();
        }
        else if (Input.GetKeyDown(sparksDisableKey))
        {
            retrievedSparks?.ISetInactive();
        }
        else if (Input.GetKeyDown(sparksReparentKey))
        {
            retrievedSparks?.ISetParent(sparksParent);
        }
    }
}