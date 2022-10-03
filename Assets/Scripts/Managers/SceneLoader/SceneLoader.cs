using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Events")]
    [SerializeField]
    private GameSceneEventChannel _loadSceneEvent;
    [SerializeField]
    private GameSceneEventChannel _unloadSceneEvent;

    private readonly Dictionary<AsyncOperation, GameScene> _loadOperations = new();

    private void OnEnable()
    {
        _loadSceneEvent.Handler += LoadScene;
        _unloadSceneEvent.Handler += UnloadScene;
    }

    private void OnDisable()
    {
        _loadSceneEvent.Handler -= LoadScene;
        _unloadSceneEvent.Handler -= UnloadScene;
    }

    /// <summary>
    /// Load a scene asynchronously.
    /// </summary>
    /// <inheritdoc cref="GameSceneEventChannel.Raise(GameObject, GameScene)"/>
    private void LoadScene(GameObject sender, GameScene newScene)
    {
        var loadOperation = SceneManager.LoadSceneAsync((int)newScene, LoadSceneMode.Additive);
        _loadOperations.Add(loadOperation, newScene);
        loadOperation.completed += UnloadOldScenes;
    }

    private void UnloadOldScenes(AsyncOperation loadOperation)
    {
        var newScene = _loadOperations[loadOperation];
        foreach (var scene in GetLoadedScenes())
        {
            if (scene != newScene)
            {
                UnloadScene(scene);
            }
        }
        _loadOperations.Remove(loadOperation);
    }

    private GameScene[] GetLoadedScenes()
    {
        var count = SceneManager.sceneCount;
        var scenes = new GameScene[count];
        for (int i = 0; i < count; i++)
        {
            scenes[i] = (GameScene)SceneManager.GetSceneAt(i).buildIndex;
        }
        return scenes;
    }

    /// <summary>
    /// Unload a scene asynchronously.
    /// </summary>
    /// <inheritdoc cref="GameSceneEventChannel.Raise(GameObject, GameScene)"/>
    private void UnloadScene(GameObject sender, GameScene scene)
    {
        if (scene != GameScene.PersistentManagers)
        {
            SceneManager.UnloadSceneAsync((int)scene);
        }
    }

    /// <inheritdoc cref="UnloadScene(GameObject, GameScene)"/>
    private void UnloadScene(GameScene scene) => UnloadScene(null, scene);
}
