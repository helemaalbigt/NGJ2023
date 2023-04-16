using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.Instance.currentState == GameStateManager.GameState.GameStarting)
        {
            if (gameObject.activeInHierarchy == false)
            {
                gameObject.SetActive(true);
            }
            text.text = Mathf.Ceil(GameStateManager.Instance.timer).ToString();
        }
        else
        {
            if (gameObject.activeInHierarchy == true)
            {
                gameObject.SetActive(false);
            }
        }

    }
}
