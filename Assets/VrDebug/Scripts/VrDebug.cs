using System;
using System.Collections.Generic;
using Rowhouse;
using UnityEngine;

namespace VrDebugPlugin
{
    public class VrDebug : MonoBehaviour
    {
        public static event Action<string, LogType> OnLogMessageReceived;

        public static bool logLogs;
        public static bool logWarnings;
        public static bool logErrors = true;

        private static Transform _debugObjectsParent;
        private static ObjectPool _axisPool;
        private static ObjectPool _pointPool;
        private static ObjectPool _linePool;

        private static readonly Dictionary<GameObject, ObjectPool> _activeObjects = new();
        private static readonly Dictionary<GameObject, ObjectPool> _markedForRemoval = new();

        private static VrDebug _instance;

        public static bool duplicateLogInConsole = false;
        public static DropOutQueue<string> log;
        private const int MaxLogs = 30;

        private void Awake() {
            if (log == null) {
                log = new DropOutQueue<string>(MaxLogs);
                Application.logMessageReceived += LogLodMessage;
            }
        }

        private void OnDestroy() {
            Application.logMessageReceived -= LogLodMessage;
        }

        //hacky solution to have the objects draw for one frame only
        private void LateUpdate() {
            foreach (var obj in _markedForRemoval) {
                if ( obj.Value!= null && obj.Key !=null)
                {
                    obj.Value.Return(obj.Key);
                    _activeObjects.Remove(obj.Key);
                }
               
            }

            _markedForRemoval.Clear();

            foreach (var obj in _activeObjects) {
                if (obj.Key !=null && obj.Key.activeInHierarchy) {
                    _markedForRemoval.Add(obj.Key, obj.Value);
                }
            }
        }

        private static void CheckToInit() {
            if (GameObject.Find("VrDebug") == null) {
                var go = new GameObject("VrDebug");
                _debugObjectsParent = go.transform;
                _instance = go.AddComponent<VrDebug>();

                _axisPool = new ObjectPool("VrDebug/Axis", _debugObjectsParent, 3);
                _pointPool = new ObjectPool("VrDebug/Point", _debugObjectsParent, 3);
                _linePool = new ObjectPool("VrDebug/Line", _debugObjectsParent, 3);
            }
        }

        /// <summary>
        ///     Adds an axis to the scene for one frame
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        public static void DrawAxis(Vector3 pos, Quaternion rot, float scale = 0.02f) {
#if UNITY_EDITOR || VRDEBUG_PROD
            CheckToInit();

            var obj = _axisPool.Get();
            obj.transform.position = pos;
            obj.transform.rotation = rot;
            obj.transform.localScale = Vector3.one * scale;

            _activeObjects.Add(obj, _axisPool);
#endif
        }

        /// <summary>
        ///     Adds an axis to the scene
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="follow">the debug axis will follow the transform's position and rotation</param>
        public static void DrawAxis(Transform tran, float scale = 0.02f) {
#if UNITY_EDITOR || VRDEBUG_PROD
            CheckToInit();

            var obj = _axisPool.Get();
            obj.transform.position = tran.position;
            obj.transform.rotation = tran.rotation;
            obj.transform.localScale = Vector3.one * scale;

            _activeObjects.Add(obj, _axisPool);
#endif
        }

        /// <summary>
        ///     Adds a point to the scene
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="color"></param>
        public static void DrawPoint(Vector3 pos, Color color = default, float scale = 0.005f) {
#if UNITY_EDITOR || VRDEBUG_PROD
            CheckToInit();

            var obj = _pointPool.Get();
            obj.transform.position = pos;
            obj.transform.localScale = scale * Vector3.one;

            if (color != default) {
                obj.GetComponent<Renderer>().material.color = color;
            }

            _activeObjects.Add(obj, _pointPool);
#endif
        }

        public static void DrawLine(Vector3 start, Vector3 end, Color color = default, float thickness = 0.008f) {
#if UNITY_EDITOR || VRDEBUG_PROD
            CheckToInit();

            var obj = _linePool.Get();
            obj.transform.position = start;
            obj.transform.forward = end - start;
            obj.transform.localScale = new Vector3(thickness, thickness, Vector3.Distance(start, end));

            if (color != default) {
                obj.GetComponentInChildren<Renderer>().material.color = color;
            }

            _activeObjects.Add(obj, _linePool);
#endif
        }

        private static void LogLodMessage(string condition, string stacktrace, LogType type) {
#if UNITY_EDITOR || VRDEBUG_PROD
            if (log == null) {
                log = new DropOutQueue<string>(MaxLogs);
            }

            if (type == LogType.Error && logErrors ||
                type == LogType.Warning && logWarnings ||
                type == LogType.Log && logLogs) {
                var newLine = "<b>" + stacktrace + "</b> <i>" + condition + "</i>";

                log.Enqueue(newLine);
                OnLogMessageReceived?.Invoke(newLine, type);
            }
#endif
        }

        #region SPAWN_FUNCTIONS

        public static GameObject SpawnAxis(Transform parent, float scale = 0.02f) {
            CheckToInit();

            var obj = _axisPool.Get();
            obj.transform.SetParent(parent);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one * scale;

            return obj;
        }

        public static GameObject SpawnAxis(Vector3 pos, Quaternion rot, float scale = 0.02f) {
            CheckToInit();

            var obj = _axisPool.Get();
            obj.transform.position = pos;
            obj.transform.rotation = rot;
            obj.transform.localScale = Vector3.one * scale;

            return obj;
        }

        public static GameObject SpawnPoint(Vector3 pos, Color color = default, float scale = 0.005f) {
            CheckToInit();

            var obj = _pointPool.Get();
            obj.transform.position = pos;
            obj.transform.localScale = scale * Vector3.one;

            if (color != default) {
                obj.GetComponent<Renderer>().material.color = color;
            }

            return obj;
        }

        public static GameObject SpawnLine(Vector3 start, Vector3 end, Color color = default, float thickness = 0.008f) {
            CheckToInit();

            var obj = _linePool.Get();
            obj.transform.position = start;
            obj.transform.forward = end - start;
            obj.transform.localScale = new Vector3(thickness, thickness, Vector3.Distance(start, end));

            if (color != default) {
                obj.GetComponentInChildren<Renderer>().material.color = color;
            }

            return obj;
        }

        public static void SetDebugColor(GameObject obj, Color color) {
            obj.GetComponentInChildren<Renderer>().material.color = color;
        }

        public static void PositionLine(GameObject line, Vector3 start, Vector3 end) {
            line.transform.position = start;
            line.transform.forward = end - start;
            line.transform.localScale = new Vector3(line.transform.localScale.x, line.transform.localScale.y, Vector3.Distance(start, end));
        }

        #endregion
    }
}