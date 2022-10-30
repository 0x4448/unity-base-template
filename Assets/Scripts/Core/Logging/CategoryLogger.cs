using System;
using System.Diagnostics;
using UnityEngine;

namespace UBT.Logging
{
    /// <summary>
    /// Extends Unity's logger to include categories and additional levels.
    /// </summary>
    [CreateAssetMenu(fileName = "NewCategoryLogger", menuName = "Logger")]
    public class CategoryLogger : ScriptableObject
    {
        [SerializeField] private bool _enabled = true;
        [SerializeField] private LogLevel _logLevel = LogLevel.Info;

        /// <summary>
        /// .NET log severity levels.
        /// </summary>
        public enum LogLevel
        {
            Trace,
            Debug,
            Info,
            Warning,
            Error,
            Critical,
            Exception
        }

        [Conditional("UNITY_EDITOR")]
        public void Trace(string message)
        {
            if (_enabled && _logLevel == LogLevel.Trace)
            {
                UnityEngine.Debug.Log(Format(message));
            }
        }

        [Conditional("UNITY_EDITOR")]
        public void Debug(string message)
        {
            if (_enabled && (int)_logLevel <= 1)
            {
                UnityEngine.Debug.Log(Format(message));
            }
        }

        [Conditional("UNITY_EDITOR")]
        public void Info(string message)
        {
            if (_enabled && (int)_logLevel <= 2)
            {
                UnityEngine.Debug.Log(Format(message));
            }
        }

        [Conditional("UNITY_EDITOR")]
        public void Warning(string message)
        {
            if (_enabled && (int)_logLevel <= 3)
            {
                UnityEngine.Debug.LogWarning(Format(message));
            }
        }

        public void Error(string message)
        {
            if (_enabled && (int)_logLevel <= 4)
            {
                UnityEngine.Debug.LogError(Format(message));
            }
        }

        public void Critical(string message)
        {
            if (_enabled && (int)_logLevel <= 5)
            {
                UnityEngine.Debug.LogError(Format(message));
            }
        }

        public void Exception(Exception exception)
        {
            if (_enabled)
            {
                UnityEngine.Debug.LogException(exception);
            }
        }

        private string Format(string message)
        {
            return $"[{name}] {message}";
        }
    }
}
