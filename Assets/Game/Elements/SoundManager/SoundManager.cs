using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Sounds sounds;

    public void PlaySound(SoundIDs sound)
    {
        ActivateSoundBasedOnID(sound);
    }

    void ActivateSoundBasedOnID(SoundIDs sound)
    {
        Debug.Log($"Playing sound {sound}...");
        switch (sound)
        {
            case SoundIDs.MenuNavigation:
                sounds.MenuNavigation.Play();
                break;

            case SoundIDs.PageTurn:
                sounds.PageTurn.Play();
                break;
        }
    }
}

public enum SoundIDs
{
    MenuNavigation,
    PageTurn,
}