using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Sounds sounds;

    public void PlaySound(SoundIDs sound)
    {

    }
}

public enum SoundIDs
{
    MenuNavigation,
    PageTurn,
}