using System.Collections.Generic;
using UnityEngine;

namespace UBT.Logging
{
    public enum Severity
    {
        Info,
        Warning,
        Error
    }

    /// <summary>
    /// Logging extension for UnityEngine.Object. Allows for easy logging with
    /// the <c>this</c> keyword.
    /// <remarks>
    /// <para>
    /// Conditional compilation is used to remove log calls in production builds.
    /// Log statements can remain in code without affecting performance outside
    /// of the editor.
    /// </para>
    /// <para>
    /// To log a message, call Log, LogWarning, or LogError from any class that
    /// derives from UnityEngine.Object.
    /// </para>
    /// <code>
    /// this.Log("Something happened");
    /// this.Log().Combat("Took 5 damage");
    /// this.LogWarning().State("Entered attacking state");
    /// </code>
    /// </remarks>
    /// </summary>
    public static class ObjectLogger
    {
        public static bool Enabled = true;

        #region Public log methods
        /// <summary>
        /// Log a message to the console if running in the Unity Editor.
        /// </summary>
        /// <param name="message"></param>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Log(this Object context, object message)
        {
            if (Enabled) LogGeneral(message, context, Severity.Info);
        }

        /// <inheritdoc cref="Log(Object, object)"/>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void LogWarning(this Object context, object message)
        {
            if (Enabled) LogGeneral(message, context, Severity.Warning);
        }

        /// <inheritdoc cref="Log(Object, object)"/>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void LogError(this Object context, object message)
        {
            if (Enabled) LogGeneral(message, context, Severity.Error);
        }

        /// <summary>
        /// Log a message with a category to the console if running in the Unity Editor.
        /// </summary>
        /// <returns>Available log categories.</returns>
        public static Category Log(this Object context) => new(context, Severity.Info);
        /// <inheritdoc cref="Log(Object)"/>
        public static Category LogWarning(this Object context) => new(context, Severity.Warning);
        /// <inheritdoc cref="Log(Object)"/>
        public static Category LogError(this Object context) => new(context, Severity.Error);
        #endregion

        private static void LogGeneral(object message, Object context, Severity severity)
        {
            new Category(context, severity).General(message);
        }
    }

    public struct Category
    {
        private static Dictionary<Severity, System.Action<object, Object>> _action;

        private readonly Object _context;
        private readonly Severity _severity;

        public Category(Object obj, Severity severity)
        {
            _action ??= new Dictionary<Severity, System.Action<object, Object>>()
                {
                    { Severity.Info, Debug.Log },
                    { Severity.Warning, Debug.LogWarning },
                    { Severity.Error, Debug.LogError }
                };

            _context = obj;
            _severity = severity;
        }

        // New log categories should be added here.
        #region Log categories
        public static bool CombatEnabled = true;
        public static string CombatCategory = "Combat";
        public void Combat(object message)
        {
            if (CombatEnabled) Log(message, CombatCategory);
        }

        public static bool GeneralEnabled = true;
        public static string GeneralCategory = "General";
        public void General(object message)
        {
            if (GeneralEnabled) Log(message, GeneralCategory);
        }

        public static bool NetworkEnabled = true;
        public static string NetworkCategory = "Network";
        public void Network(object message)
        {
            if (NetworkEnabled) Log(message, NetworkCategory);
        }

        public static bool StateEnabled = true;
        public static string StateCategory = "State";
        public void State(object message)
        {
            if (StateEnabled) Log(message, StateCategory);
        }
        #endregion

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        private void Log(object message, string category)
        {
            _action[_severity].Invoke($"[{category}] {_context.name}: {message}", _context);
        }
    }
}
