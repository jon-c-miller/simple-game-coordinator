using UnityEngine;

/// <summary> Provides a centralized, globally accessible API to handle common system requests. </summary>
public class GameCoordinator : MonoBehaviour
{
    // Initialized plain classes and editor refs to primary game systems
    
    [SerializeField] SoundManager soundManager;
    [SerializeField] ParticleManager particleManager;
    [SerializeField] SceneManager sceneManager;


    // Methods for common commands that will be accessed by the rest of the codebase
    
    public void RequestSound(SoundIDs sound)
    {
        if (soundManager != null)
            soundManager.PlaySound(sound);
    }

    public IParticleEffect RequestParticleEffect(ParticleIDs effect)
    {
        IParticleEffect newEffect = null;

        if (particleManager != null)
            newEffect = particleManager.GetParticleEffect(effect);

        return newEffect;
    }

    public void RequestSceneIncrement(bool nextScene)
    {
        // Move to next scene or return to previous scene
        if (sceneManager != null)
        {
            RefreshManagers();
            RequestSound(SoundIDs.PageTurn);
            sceneManager.IncrementActiveScene(nextScene);
        }
    }

    public void RequestSpecificScene(SceneIDs scene)
    {
        if (sceneManager != null)
        {
            RefreshManagers();
            RequestSound(SoundIDs.PageTurn);
            sceneManager.NavigateToSpecificScene(scene);
        }
    }

    void RefreshManagers()
    {
        // Perform any logic that cleans up and resets managers
        particleManager.OnSceneChange();
    }


    public static GameCoordinator Instance { get; private set; }

    void Awake()
    {
        // Prevent reassigning an existing instance, as well as destruction on scene loading
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}