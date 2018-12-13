using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtn : MonoBehaviour {

	public GameObject tutorial;

	void OnMouseDown()
	{
		tutorial.GetComponent<Animator> ().SetInteger ("page", 1);
//		SceneManager.LoadScene ("MainGame");
	}

	void OnMouseEnter() {
		this.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
	}

	void OnMouseExit() {
		this.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
	}
}
