// Manages singleton instance and initializes managers in the game API
[System.Serializable] public partial class Game
{
    // Internal singleton which all API calls are redirected to
    static Game instance;

    void Awake()
    {
        if (instance != null)
        {
            // Destroy this instance of GameAPI if there is already an Instance singleton
            Destroy(gameObject);
        }
        else
        {
            // Otherwise assign elements to the global Instance, initialize, and prevent destroy on scene load
            instance = this;
            Initialize();
            DontDestroyOnLoad(gameObject);
        }
    }

    void Initialize()
    {
        // Handle any initializing here

        if (VerifyReferences())
            UnityEngine.Debug.LogWarning("All references intact.");
        else UnityEngine.Debug.LogError("Warning: one or more references missing. Some API calls will fail.");
    }

    void RefreshManagers()
    {
        // Perform any logic that cleans up and resets managers
        instance.particleManager.OnSceneChange();
    }
}