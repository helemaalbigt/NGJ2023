using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CameraLocalPlayer : NetworkBehaviour
{
    [SerializeField] private GameObject cam;
    public override void OnNetworkSpawn()
    {
        if (IsOwner) return;
        cam.SetActive(false);
    }
}
