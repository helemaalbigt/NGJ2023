using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class StartAndJoinAuto : NetworkBehaviour {

    void Start() {
#if UNITY_ANDROID
        StartCoroutine(WaitAndInit());
#endif
    }

    private IEnumerator WaitAndInit() {
        yield return new WaitForSeconds(2f);
        RelayManager.Instance.CreateRelay();
    }

}
