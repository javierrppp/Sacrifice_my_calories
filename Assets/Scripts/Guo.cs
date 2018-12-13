using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Guo : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(GameController._instance.beatedBoss <= GameController._instance.currentBoss)  {	
			GameController._instance.backFromBoss = true;
            GameController._instance.weight = global.Global.weight;
            GameController._instance.checkpointLayer = global.Global.checkpointLayer;
			SceneManager.LoadScene(2);
		}
	}
}
