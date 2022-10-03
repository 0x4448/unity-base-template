using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Initialize the game.
/// </summary>
public class Startup : MonoBehaviour
{
    [Header("Events")]
    [SerializeField]
    private GameSceneEventChannel _loadSceneEvent;

    private void Awake()
    {
        var load = SceneManager.LoadSceneAsync((int)GameScene.PersistentManagers);
        load.completed += (_) => _loadSceneEvent.Raise(GameScene.Intro);
    }
}
