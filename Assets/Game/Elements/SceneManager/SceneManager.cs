using UnityEngine;

/// <summary> Processes requests to navigate between scenes. </summary>
public class SceneManager : MonoBehaviour
{
    public void IncrementActiveScene(bool nextScene)
    {
        // Note the current scene index and max scene count to limit incrementing to within scene build array
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCount;
        
        // As directed by nextScene bool, go to next if index less than max; or previous if index greater than 0
        if (nextScene && sceneCount > currentSceneIndex)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(currentSceneIndex + 1);
        }
        else if (!nextScene && currentSceneIndex > 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(currentSceneIndex - 1);
        }
    }
}

public enum SceneIDs
{
    ParticleTesting,
    SoundTesting,
}