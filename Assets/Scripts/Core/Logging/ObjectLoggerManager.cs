using UnityEngine;

namespace UBT.Logging
{
    /// <summary>
    /// Attach this to a GameObject to enable or disable ObjectLogger log
    /// categories during play mode.
    /// </summary>
    public class ObjectLoggerManager : MonoBehaviour
    {
        [SerializeField] private bool _loggingEnabled;
        [SerializeField] private bool _combatEnabled;
        [SerializeField] private bool _generalEnabled;
        [SerializeField] private bool _networkEnabled;
        [SerializeField] private bool _stateEnabled;



        private void OnValidate()
        {
            ObjectLogger.Enabled = _loggingEnabled;
            Category.CombatEnabled = _combatEnabled;
            Category.GeneralEnabled = _generalEnabled;
            Category.NetworkEnabled = _networkEnabled;
            Category.StateEnabled = _stateEnabled;
        }
    }
}
