using UnityEngine;

/// <summary> Holds references to all sound assets for access by SoundManager. </summary> 
[System.Serializable]
public class Sounds
{
    public AudioClip PageTurnClip;
    public AudioSource PageTurn;
    [Space]
    public AudioSource MenuNavigation;
}

public enum SoundIDs
{
    MenuNavigation,
    PageTurn,
}