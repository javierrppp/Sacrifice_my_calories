using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {
	
	void OnMouseDown()
	{
		SceneManager.LoadScene ("MainGame");
	}
//	void Update() {
//		if (Input.GetMouseButtonDown (1)) {
//			SceneManager.LoadScene ("MainGame");
//		}
//	}
}
