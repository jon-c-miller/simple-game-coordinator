using UnityEngine;

// Provides a central location for organizing API methods
public partial class Game
{
    public static IParticleEffect RequestParticleEffect(ParticleIDs effect) => instance.particleManager.GetParticleEffect(effect);
    
    public static void RequestSound(SoundIDs sound) => instance.soundManager.PlaySound(sound);

    public static void RequestSceneIncrement(bool nextScene)
    {
        // Move to next scene or return to previous scene
        instance.RefreshManagers();
        instance.sceneManager.IncrementActiveScene(nextScene);
        if (instance.pageSoundOnSceneSwitch) RequestSound(SoundIDs.PageTurn);
    }

    public static void RequestSpecificScene(SceneIDs scene)
    {
        instance.RefreshManagers();
        instance.sceneManager.NavigateToSpecificScene(scene);
        if (instance.pageSoundOnSceneSwitch) RequestSound(SoundIDs.PageTurn);
    }
}