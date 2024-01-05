using UnityEngine;

/// <summary> Provides a centralized, globally accessible API to request common game functionality. </summary>
public class GameCoordinator : MonoBehaviour
{
    // Initialized plain classes and editor refs to primary game systems (sound controller, etc.)
    
    [SerializeField] SoundManager soundManager;
    [SerializeField] ParticleManager particleManager;
    [SerializeField] SceneManager sceneManager;


    // Methods for common commands that will be accessed by the rest of the codebase
    
    public void RequestSound(SoundIDs sound)
    {
        if (soundManager != null)
            soundManager.PlaySound(sound);
    }

    public IParticleEffect RequestEffect(ParticleIDs effect)
    {
        IParticleEffect newEffect = null;

        if (particleManager != null)
            newEffect = particleManager.GetParticleEffect(effect);

        return newEffect;
    }

    public void RequestSceneIncrement(bool nextScene)
    {
        if (sceneManager != null)
            sceneManager.IncrementActiveScene(nextScene);
    }

    public void RequestSpecificScene(SceneIDs scene)
    {
        if (sceneManager != null)
            sceneManager.NavigateToSpecificScene(scene);
    }

    
    public static GameCoordinator Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);            // Destroy self if singleton already present
        }
        else
        {
            Instance = this;                // Assign self if Instance is null
            DontDestroyOnLoad(gameObject);  // Preserve self on reload
        }
    }
}