using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JoinOnButton : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    private void Awake()
    {
    #if UNITY_ANDROID
            gameObject.SetActive(false);
    #endif
    }
    public void OnButtonPress()
    {
        RelayManager.Instance.JoinRelay(inputField.text);
    }
}
