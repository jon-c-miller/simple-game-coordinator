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
            GameCoordinator.Instance.RequestSceneIncrement(false);
        else if (Input.GetKeyDown(nextSceneKey))
            GameCoordinator.Instance.RequestSceneIncrement(true);
    }
}