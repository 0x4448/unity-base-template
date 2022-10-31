using UnityEngine;

namespace UBT.Logging
{
    /// <summary>
    /// Logging extensions for UnityEngine.Object. Allows for easy logging with
    /// the <c>this</c> keyword.
    /// <remarks>
    /// <para>
    /// To log a message, call Log, LogWarning, or LogError from any class that
    /// derives from UnityEngine.Object.
    /// </para>
    /// <code>
    /// this.Log("Something happened");
    /// this.Log("Took 5 damage", LC.Combat);
    /// </code>
    /// </remarks>
    /// </summary>
    public static class ObjectLogger
    {
        public static CustomLogger[] Loggers =
        {
            // This must be in the same order as the LC enum.
            new(LC.Combat),
            new(LC.Core),
            new(LC.General),
            new(LC.Player)
        };

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Log(this Object obj, object message, LC category = LC.General)
        {
            Loggers[(int)category].Log(message, obj);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void LogWarning(this Object obj, object message, LC category = LC.General)
        {
            Loggers[(int)category].LogWarning(message, obj);
        }
        public static void LogError(this Object obj, object message, LC category = LC.General)
        {
            Loggers[(int)category].LogError(message, obj);
        }
    }

    /// <summary>
    /// Log Category.
    /// </summary>
    public enum LC
    {
        // This must be in the same order as ObjectLogger.Loggers.
        Combat,
        Core,
        General,
        Player
    }

    public class CustomLogger
    {
        public bool Enabled { get; set; }
        public string Category { get; private set; }

        public CustomLogger(LC category)
        {
            Category = category.ToString();
            Enabled = true;
        }

        public void Log(object message, Object context)
        {
            if (Enabled)
            {
                ConsoleLog(Debug.Log, message, context);
            }
        }

        public void LogWarning(object message, Object context)
        {
            if (Enabled)
            {
                ConsoleLog(Debug.LogWarning, message, context);
            }
        }

        public void LogError(object message, Object context)
        {
            if (Enabled)
            {
                ConsoleLog(Debug.LogError, message, context);
            }
        }

        private void ConsoleLog(System.Action<object, Object> action, object message, Object context)
        {
            action($"[{Category}] {context.name}: {message}", context);
        }
    }
}
