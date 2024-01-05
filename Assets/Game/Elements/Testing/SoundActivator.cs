using UnityEngine;

public class SoundActivator : MonoBehaviour
{
    [SerializeField] KeyCode pageTurnKey = KeyCode.Alpha1;
    [SerializeField] KeyCode menuNavigationKey = KeyCode.Alpha2;

    void Update()
    {
        if (Input.GetKeyDown(pageTurnKey))
            GameCoordinator.Instance.RequestSound(SoundIDs.PageTurn);
        else if (Input.GetKeyDown(menuNavigationKey))
            GameCoordinator.Instance.RequestSound(SoundIDs.MenuNavigation);
    }
}