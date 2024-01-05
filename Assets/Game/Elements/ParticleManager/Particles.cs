using System.Collections.Generic;
using UnityEngine;

/// <summary> Holds references to all particle system assets and pools for access by ParticleManager. </summary> 
[System.Serializable]
public class Particles
{
    [SerializeField] ParticleSystem sparksPF;
    [SerializeField] ParticleSystem explosionPF;

    public Queue<ParticleSystem> Sparks = new();
    public Queue<ParticleSystem> Explosions = new();
}