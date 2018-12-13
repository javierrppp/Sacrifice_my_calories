using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using global;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour {

	private float startProgress = -0.12f;

	private float endProgress = 1.194f;

	public GameObject pointer;
	public Text level;

	private float targetPosX = 0;

	// Update is called once per frame
	void Update () {
		int weight = global.Global.weight;
		float prop = ((float)weight - (float)Config.minWeight) / ((float)Config.maxWeight - (float)Config.minWeight);
		float posX = prop * (endProgress - startProgress) + startProgress;
        targetPosX = posX;
		level.text = "Layer: " + (int)(global.Global.checkpointLayer / 10);
	}

	void FixedUpdate()
    {
        float diff = targetPosX - pointer.transform.localPosition.x;
		if (diff != 0) {
			float posX = pointer.transform.localPosition.x + diff / 20;
			if ((diff < 0 && posX < targetPosX) || (diff > 0 && posX > targetPosX)) {
				posX = targetPosX;
			}
			pointer.transform.localPosition = new Vector2 (posX, pointer.transform.localPosition.y);
		}
	}
}
