using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using global;

public class HealthController : MonoBehaviour {

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    private static HealthController healthController;

    public static HealthController _instance
    {
        get
        {
            if (!healthController)
            {
                healthController = FindObjectOfType(typeof(HealthController)) as HealthController;

                if (!healthController)
                {
                    GameObject _foodFactory = new GameObject();
                    healthController = _foodFactory.AddComponent<HealthController>();
                }
                else
                {
                    healthController.Init();
                }
            }

            return healthController;
        }
    }

    private void Init()
    {

    }

    public bool hurt()
    {
        Global.heartNum--;
        if (Global.heartNum == 2)
        {
            heart3.SetActive(false);
        } else if (Global.heartNum == 1)
        {
            heart2.SetActive(false);
        } else
        {
            heart1.SetActive(false);
            return false;
        }
        return true;
    }

    public void changeHeart()
    {
        if (Global.heartNum == 3)
        {
            heart3.SetActive(true);
            heart2.SetActive(true);
            heart1.SetActive(true);
        }
        else if (Global.heartNum == 2)
        {
            heart3.SetActive(false);
            heart2.SetActive(true);
            heart1.SetActive(true);
        }
        else if (Global.heartNum == 1)
        {
            heart3.SetActive(false);
            heart2.SetActive(false);
            heart1.SetActive(true);
        }
        else
        {
            heart3.SetActive(false);
            heart2.SetActive(false);
            heart1.SetActive(false);
        }
    }

    public bool isDead()
    {
        return Global.heartNum == 0;
    }

    public void resetHeart()
    {
        Global.heartNum = 3;
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
    }
}
