using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class StartAndJoinAuto : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            RelayManager.Instance.CreateRelay();
            //NetworkManager.Singleton.StartHost();
        }
    }
}
