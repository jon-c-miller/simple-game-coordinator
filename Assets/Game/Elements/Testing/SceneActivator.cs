using UnityEngine;

/// <summary> Acts as a proxy for areas of the codebase that would request scene changes through GameCoordinator. </summary>
public class SceneActivator : MonoBehaviour
{
    [SerializeField] KeyCode nextSceneKey = KeyCode.Tab;
    [SerializeField] KeyCode previousSceneKey = KeyCode.Backspace;
    [Space]
    [SerializeField] KeyCode specificSceneKey = KeyCode.Space;
    [SerializeField] SceneIDs specificSceneToLoad;

    void Update()
    {
        if (Input.GetKeyDown(previousSceneKey))
        {
            // Simulates a scenario where the previous scene needs to be loaded
            GameCoordinator.Instance.RequestSceneIncrement(false);
        }
        else if (Input.GetKeyDown(nextSceneKey))
        {
            // Simulates a scenario where the next scene needs to be loaded
            GameCoordinator.Instance.RequestSceneIncrement(true);
        }
        else if (Input.GetKeyDown(specificSceneKey))
        {
            // Simulates a scenario where a specific given scene needs to be loaded
            GameCoordinator.Instance.RequestSpecificScene(specificSceneToLoad);
        }
    }
}