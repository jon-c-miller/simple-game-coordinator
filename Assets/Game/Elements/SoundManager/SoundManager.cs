using UnityEngine;

/// <summary> Processes requests to play a sound. </summary>
public class SoundManager : MonoBehaviour
{
    [SerializeField] Sounds sounds = new();

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
                sounds.PageTurn.PlayOneShot(sounds.PageTurnClip);
                break;
        }
    }
}

public enum SoundIDs
{
    MenuNavigation,
    PageTurn,
}