using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectTextField : MonoBehaviour {
    public TMP_InputField text;
    void Start()
    {
        text.Select();
    }
}
