// Keeps commands that affect all APIs in a single location
[System.Serializable] public partial class GameAPI
{
    public void InitializeAPI()
    {
        // Handle any API initializing here

    }

    void RefreshManagers()
    {
        // Perform any logic that cleans up and resets managers
        particleManager.OnSceneChange();
    }
}