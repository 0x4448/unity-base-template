using UnityEngine;

namespace UBT.Logging
{
    public class LogManager : MonoBehaviour
    {
        [Tooltip("Disable if the object is in a persistent scene.")]
        [SerializeField] private bool _doNotDestroyOnLoad = true;

        // Inspector fields for toggling logs in play mode.
        [Header("Logs")]
        [SerializeField] private bool _combatLog = true;
        [SerializeField] private bool _coreLog = true;
        [SerializeField] private bool _enemyLog = true;
        [SerializeField] private bool _playerLog = true;
        [SerializeField] private bool _stateLog = true;

        // Static fields for exposing log categories.
        public static Logger Combat { get; private set; }
        public static Logger Core { get; private set; }
        public static Logger Enemy { get; private set; }
        public static Logger Player { get; private set; }
        public static Logger State { get; private set; }

        // Bool references for the Logger class.
        private readonly Bool _combatLogRef = new();
        private readonly Bool _coreLogRef = new();
        private readonly Bool _enemyLogRef = new();
        private readonly Bool _playerLogRef = new();
        private readonly Bool _stateLogRef = new();

        private void InitializeCategoryVariables()
        {
            Combat = new(_combatLogRef);
            Core = new(_coreLogRef);
            Enemy = new(_enemyLogRef);
            Player = new(_playerLogRef);
            State = new(_stateLogRef);
        }

        private void UpdateCategoryVariables()
        {
            _combatLogRef.Value = _combatLog;
            _coreLogRef.Value = _coreLog;
            _enemyLogRef.Value = _enemyLog;
            _playerLogRef.Value = _playerLog;
            _stateLogRef.Value = _stateLog;
        }

        /// <summary>
        /// Update Bool references when value in the inspector changes.
        /// </summary>
        private void OnValidate()
        {
            UpdateCategoryVariables();
        }

        private static LogManager _instance = null;

        private void Awake()
        {
            if (_instance is not null && _instance != this)
            {
                Destroy(this);
            }
            else
            {
                _instance = this;
                if (_doNotDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }

            InitializeCategoryVariables();
            UpdateCategoryVariables();
        }

        /// <summary>
        /// Log messages to the Unity console if it is enabled.
        /// Info and warning logs are removed with conditional compilation.
        /// </summary>
        public class Logger
        {
            private readonly Bool _enabled;

            public Logger(Bool enabled) => _enabled = enabled;

            [System.Diagnostics.Conditional("UNITY_EDITOR")]
            public void Log(object message)
            {
                if (_enabled.Value)
                {
                    Debug.Log(message);
                }
            }

            [System.Diagnostics.Conditional("UNITY_EDITOR")]
            public void LogWarning(object message)
            {
                if (_enabled.Value)
                {
                    Debug.LogWarning(message);
                }
            }

            public void LogError(object message)
            {
                if (_enabled.Value)
                {
                    Debug.LogError(message);
                }
            }
        }

        /// <summary>
        /// Reference wrapper for boolean values.
        /// </summary>
        public class Bool
        {
            public bool Value { get; set; }
            public Bool() => Value = true;
        }
    }
}
