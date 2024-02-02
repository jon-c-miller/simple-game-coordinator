using UnityEngine;

// Handles requests dealing with scene updates
public partial class GameAPI
{
    [SerializeField] SceneManager sceneManager;
    
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
}