using System.Collections.Generic;
using UnityEngine;

public class GameCoordinator : MonoBehaviour
{
    // Initialized plain classes and editor refs to primary game systems (sound controller, etc.)



    // Methods for common commands that will be accessed by the rest of the codebase
    

    
    public static GameCoordinator Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
}