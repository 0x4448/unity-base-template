using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSceneEvent", menuName = "Event Channels/Game Scene Event")]
public class GameSceneEventChannel : ScriptableObject
{
    public delegate void GameSceneEvent(GameObject sender, GameScene scene);
    public event GameSceneEvent Handler;

    /// <summary>
    /// Ask the SceneLoader to load or unload a scene.
    /// </summary>
    /// <param name="sender">The GameObject that raised the event.</param>
    /// <param name="scene">Index of the scene in Build Settings.</param>
    public void Raise(GameObject sender, GameScene scene) => Handler?.Invoke(sender, scene);

    /// <inheritdoc cref="Raise(GameObject, GameScene)"/>
    public void Raise(GameScene scene) => Raise(null, scene);
}
