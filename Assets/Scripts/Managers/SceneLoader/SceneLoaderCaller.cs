using UnityEngine;

/// <summary>
/// Load or unload a scene from the Inspector.
/// </summary>
public class SceneLoaderCaller : MonoBehaviour
{
    [SerializeField] private GameSceneEventChannel _sceneEvent;
    [SerializeField] private GameScene _scene;

    public void Invoke() => _sceneEvent.Raise(_scene);
}
