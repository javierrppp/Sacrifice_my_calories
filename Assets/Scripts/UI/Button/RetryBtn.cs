using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryBtn : MonoBehaviour {

    void OnMouseDown()
    {
        MainGameController._instance.restart();
        MainGameController._instance.gameOverDialog.SetActive(false);
    }
}
