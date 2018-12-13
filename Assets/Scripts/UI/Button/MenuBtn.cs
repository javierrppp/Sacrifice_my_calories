using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBtn : MonoBehaviour {

    void OnMouseDown()
    {
        SceneManager.LoadScene(0);
        MainGameController._instance.gameOverDialog.SetActive(false);
    }
}
