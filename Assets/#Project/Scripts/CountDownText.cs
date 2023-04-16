using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.Instance.currentState == GameStateManager.GameState.GameStarting)
        {
            target.SetActive(true);
            text.text = Mathf.Ceil(GameStateManager.Instance.timer).ToString();
        }
        else
        {
            target.SetActive(false);
        }
        
    }
}
