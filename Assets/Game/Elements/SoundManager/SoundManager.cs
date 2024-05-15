using UnityEngine;

/// <summary> Processes requests to play a sound. </summary>
public class SoundManager : MonoBehaviour
{
    [SerializeField] Sounds soundAssets = new();

    public void PlaySound(SoundIDs sound)
    {
        Debug.Log($"Playing sound {sound}...");
        switch (sound)
        {
            case SoundIDs.MenuNavigation:
                // Play a sound that can only have one playback at a time (playback reset if activated again before done)
                soundAssets.MenuNavigation.Play();
                break;

            case SoundIDs.PageTurn:
                // Play a sound that can have multiple concurrent playbacks
                soundAssets.PageTurn.PlayOneShot(soundAssets.PageTurnClip);
                break;
        }
    }
}