using UnityEngine;
using UnityEngine.UI;

namespace VrDebugPlugin
{
    public class VrDebugLogCanvas : MonoBehaviour
    {
        public bool LogErrors;
        public bool LogWarnings;
        public bool LogLogs;

        private Text _text;

        private void Start() {
#if UNITY_EDITOR || VRDEBUG_PROD
            transform.GetChild(0).gameObject.SetActive(true);

            VrDebug.logLogs = LogLogs;
            VrDebug.logWarnings = LogWarnings;
            VrDebug.logErrors = LogErrors;

            VrDebug.OnLogMessageReceived += UpdateLogView;
#else
            transform.GetChild(0).gameObject.SetActive(false);
#endif
            _text = GetComponentInChildren<Text>();
        }

        private void UpdateLogView(string log, LogType type) {
            var text = "";
            foreach (var line in VrDebug.log) {
                text += line + "\n \n";
            }

            _text.text = text;
        }

        private void OnDestroy() {
            VrDebug.OnLogMessageReceived -= UpdateLogView;
        }
    }
}