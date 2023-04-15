using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JoinCodeView : MonoBehaviour
{
   public TextMeshPro _joinCodeText;
    
    void Update()
    {
        if (RelayManager.Instance != null)
        {
            _joinCodeText.text = RelayManager.Instance.JoinCode;
        }
    }
}
