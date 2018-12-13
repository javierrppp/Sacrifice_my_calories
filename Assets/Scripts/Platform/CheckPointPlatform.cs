using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using global;

public class CheckPointPlatform : PlatformAbstract {

	public bool isTrigger = false;
	//public GameObject guo;

	void Awake() {
		this.isCheckPoint = true;
		this.platformType = 2;
	}

	public override void init () {
		length = 11;
		Vector2[] pos = { new Vector2(0, 0), new Vector2(-width, 0), new Vector2(width, 0), new Vector2(-2 * width, 0), new Vector2(2 * width, 0),
			new Vector2(-3 * width, 0), new Vector2(3 * width, 0), new Vector2(-4 * width, 0), new Vector2(4 * width, 0),
			new Vector2(-5 * width, 0), new Vector2(5 * width, 0)};

		for (int i = 0; i < length; i++) {
			GameObject box = Instantiate(box1);
			box.transform.parent = this.gameObject.transform;
			box.transform.localPosition = pos [i];
			boxList.Add(box);
		}
		//renderColor ();
	}

	public override void renderColor() {
		foreach (GameObject box in boxList) {
			box.GetComponent<BoxController> ().renderColor ();
		}
	}

	public override void land() {
		;
	}

	public void checkPoint() {
		if (!isTrigger) {
		    GameObject pot = FoodFactory._instance.generatePot ();
			pot.GetComponent<Animation>().Play();
		    pot.transform.parent = this.gameObject.transform.parent;
		    pot.transform.position = transform.position + Vector3.up;
            pot.transform.parent = this.gameObject.transform.parent;

            isTrigger = true;
			Global.checkpointLayer = (int)((Global.reachLayer + 1.0f) / 10) * 10;
			Debug.Log (string.Format("check point!{0}", Global.checkpointLayer));
		}
	}
}
