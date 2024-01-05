using UnityEngine;

public class GameCoordinator : MonoBehaviour
{
    // Initialized plain classes and editor refs to primary game systems (sound controller, etc.)
    
    [SerializeField] SoundManager soundManager;


    // Methods for common commands that will be accessed by the rest of the codebase
    
    public void RequestSound(SoundIDs sound)
    {
        if (soundManager != null)
            soundManager.PlaySound(sound);
    }

    
    public static GameCoordinator Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
}