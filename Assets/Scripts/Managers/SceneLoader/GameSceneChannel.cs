using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSceneEvent", menuName = "Event Channels/Game Scene Event")]
public class GameSceneEventChannel : ScriptableObject
{
    public delegate void GameSceneEvent(GameObject sender, GameScene index);
    public event GameSceneEvent Handler;

    /// <summary>
    /// Ask the SceneLoader to load or unload a scene.
    /// </summary>
    /// <param name="sender">The GameObject that raised the event.</param>
    /// <param name="index">Index of the scene in Build Settings.</param>
    public void Raise(GameObject sender, GameScene index) => Handler?.Invoke(sender, index);

    /// <inheritdoc cref="Raise(GameObject, GameScene)"/>
    public void Raise(GameScene index) => Raise(null, index);
}
