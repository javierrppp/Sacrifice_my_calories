using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    private int heartNum = 3;

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
        heartNum--;
        if (heartNum == 2)
        {
            heart3.SetActive(false);
        } else if(heartNum == 1)
        {
            heart2.SetActive(false);
        } else
        {
            heart1.SetActive(false);
            return false;
        }
        return true;
    }

    public bool isDead()
    {
        return heartNum == 0;
    }

    public void resetHeart()
    {
        heartNum = 3;
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
    }
}
