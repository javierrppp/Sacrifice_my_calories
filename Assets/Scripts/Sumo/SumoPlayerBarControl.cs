using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumoPlayerBarControl : MonoBehaviour {

	// Use this for initialization
	public GameObject cursor;
	public bool isPress;
	public float moveTime;

	float cursorPositionPer = -1f;
	float movingVelocity = 0f;
	float maxWidth = 2.718f;

	void Start () {
	}

	public void StartCursor() {
		isPress = false;
        StartCoroutine(CursorCoroutine());
	}

	IEnumerator CursorCoroutine() {
		float target = 1f;
		while(isPress == false) {
			cursorPositionPer = Mathf.SmoothDamp(cursorPositionPer, target, ref movingVelocity, moveTime);
			RefreshCursorPos();
			if (Mathf.Abs(target - cursorPositionPer) < 0.1f) target *= (-1f);
		    yield return null;
		}
	}

	void RefreshCursorPos() {
		float xPos = maxWidth * cursorPositionPer;
        cursor.transform.position = new Vector3(xPos, cursor.transform.position.y, cursor.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		/*if(Input.GetKeyUp(KeyCode.Space) && isPress == false)
		    isPress = true;
		else if(Input.GetKeyUp(KeyCode.Space) && isPress == true)  {
				isPress = false;
				StartCursor();
			}*/
	}
	public float StopCursor() {
		isPress = true;
        return 1 - Mathf.Abs(cursorPositionPer);
	}
}
