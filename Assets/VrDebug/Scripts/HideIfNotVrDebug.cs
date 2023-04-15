using UnityEngine;

namespace VrDebugPlugin
{
    //hides this gameobject if VrDebug is not enabled
    public class HideIfNotVrDebug : MonoBehaviour
    {
        void Start() {
#if !UNITY_EDITOR && !VRDEBUG_PROD
      gameObject.SetActive(false);  
#endif
        }
    }
}
