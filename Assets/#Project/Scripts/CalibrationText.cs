using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrationText : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.Instance.currentState == GameStateManager.GameState.Waiting)
        {
            if (gameObject.activeInHierarchy == false)
            {
                gameObject.SetActive(true);
            }
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
