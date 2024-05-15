using UnityEngine;

/// <summary> Provides a centralized, globally accessible API to handle common system requests. </summary>
public partial class Game : MonoBehaviour
{
    // Manager references

    [SerializeField] ParticleManager particleManager;
    [SerializeField] SceneManager sceneManager;
    [SerializeField] SoundManager soundManager;

    [SerializeField] bool pageSoundOnSceneSwitch;

    bool VerifyReferences()
    {
        return particleManager != null &&
               sceneManager != null &&
               soundManager != null;
    }
}