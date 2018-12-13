using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumoEnemyBarControl : MonoBehaviour {

	// Use this for initialization
	public GameObject cursor;
	public bool isPress;
	public float moveTime;

	float cursorPositionPer = 0f;
	//float movingVelocity = 0f;
	float maxWidth = 2.718f;

	void Start () {
		//StartCursor();
	}

	public void StartCursor() {
		isPress = false;
        StartCoroutine(CursorCoroutine());
	}

	IEnumerator CursorCoroutine() {
		float target = 1f;
		while(isPress == false) {
			cursorPositionPer = Mathf.Lerp(cursorPositionPer, target, 1f / moveTime * Time.deltaTime);
			RefreshCursorPos();
			if (Mathf.Abs(target - cursorPositionPer) < 0.01f) target = (target == 0f)? 1f:0f;
		    yield return null;
		}
	}

	void RefreshCursorPos() {
		float xPos = maxWidth * 2 * cursorPositionPer - maxWidth;
        cursor.transform.position = new Vector3(xPos, cursor.transform.position.y, cursor.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	}
	public float StopCursor() {
		isPress = true;
        return cursorPositionPer;   // percent
	}
}
