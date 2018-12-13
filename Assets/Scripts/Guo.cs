using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using global;
public class Guo : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.tag == "Player")
        {
            if (GameController._instance.beatedBoss <= GameController._instance.currentBoss)
            {
                GameController._instance.backFromBoss = true;
                GameController._instance.weight = Global.weight;
                GameController._instance.checkpointLayer = Global.checkpointLayer;
                GameController._instance.score = Global.score;
                GameController._instance.heartNum = Global.heartNum;
                GameController._instance.reachLayer = Global.reachLayer;
                GameController._instance.baseLayer = Global.baseLayer;
                SceneManager.LoadScene(2);
            }
        }
	}
}
