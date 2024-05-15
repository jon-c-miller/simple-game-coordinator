using UnityEngine;

/// <summary> Processes requests to navigate between scenes. </summary>
public class SceneManager : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image loadingIndicator;

    AsyncOperation loadProgress;

    public void IncrementActiveScene(bool nextScene)
    {
        // Note the current scene index and max scene count to limit incrementing to within scene build array
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings - 1;
        
        // As directed by nextScene bool, go to next if index less than max; or previous if index greater than 0
        if (nextScene && sceneCount > currentSceneIndex)
        {
            loadProgress = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(currentSceneIndex + 1);
        }
        else if (!nextScene && currentSceneIndex > 0)
        {
            loadProgress = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(currentSceneIndex - 1);
        }
    }

    public void NavigateToSpecificScene(SceneIDs scene) => UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene.ToString());

    void Update()
    {
        if (loadProgress != null)
        {
            if (!loadProgress.isDone)
            {
                // Rotate loading indicator image while loading is taking place
                Vector3 rotation = loadingIndicator.rectTransform.localRotation.eulerAngles;
                rotation.z -= Time.deltaTime * 15;
                loadingIndicator.rectTransform.localRotation = Quaternion.Euler(rotation);
            }
        }
    }
}

// Should be identical to the scene filenames for easy conversion to strings for LoadSceneAsync
public enum SceneIDs
{
    GameEntry,
    ParticleTesting,
    SoundTesting,
}