using UnityEngine;

namespace UBT.Logging
{
    /// <summary>
    /// Attach this to a GameObject to enable or disable ObjectLogger log methods
    /// during play mode.
    /// </summary>
    public class ObjectLoggerManager : MonoBehaviour
    {
        [SerializeField] private LoggerStatus[] _loggers;

        [System.Serializable]
        public class LoggerStatus
        {
            public string Logger;
            public bool Enabled;

            public LoggerStatus(string logger, bool enabled)
            {
                Logger = logger;
                Enabled = enabled;
            }
        }

        private void Awake()
        {
            _loggers = new LoggerStatus[ObjectLogger.Loggers.Length];
        }

        private void OnEnable()
        {
            var length = Mathf.Min(ObjectLogger.Loggers.Length, _loggers.Length);
            for (var i = 0; i < length; i++)
            {
                var logger = ObjectLogger.Loggers[i];
                _loggers[i] = new(logger.Category, logger.Enabled);
            }
        }

        private void OnValidate()
        {
            var length = Mathf.Min(ObjectLogger.Loggers.Length, _loggers.Length);
            for (var i = 0; i < length; i++)
            {
                ObjectLogger.Loggers[i].Enabled = _loggers[i].Enabled;
            }
        }
    }
}
