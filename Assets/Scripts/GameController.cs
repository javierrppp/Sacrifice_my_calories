using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
    //第一章
    public float weight = 200f;
    public int currentBoss = 0;
    public int beatedBoss = 0;
    public bool backFromBoss = false;
	public int layer = 1;
    public int checkpointLayer = 10;

    private static GameController gamecontroller;

    public static GameController _instance
    {
        get
        {
            if (!gamecontroller)
            {
                gamecontroller = FindObjectOfType(typeof(GameController)) as GameController;

                if (!gamecontroller)
                {
                    GameObject _gamecontroller = new GameObject();
                    gamecontroller = _gamecontroller.AddComponent<GameController>();
                }
                else
                {
                    gamecontroller.Init();
                }
            }

            return gamecontroller;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Init()
    {

    }
     void Update()
    {
        //weight = global.Global.weight;
        //checkPointLayer = global.Global.layer;
        //Debug.Log(checkPointLayer);
    }
}
