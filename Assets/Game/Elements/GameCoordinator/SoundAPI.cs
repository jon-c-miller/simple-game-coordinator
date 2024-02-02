using System;
using UnityEngine;

// Handles requests for audio
public partial class GameAPI
{
    [SerializeField] SoundManager soundManager;

    public void RequestSound(SoundIDs sound)
    {
        if (soundManager != null)
            soundManager.PlaySound(sound);
    }
}