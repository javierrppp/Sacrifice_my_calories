using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumoManager : MonoBehaviour {

    public float weight;
	// Use this for initialization
	void Start () {
		weight = GameController._instance.weight;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
