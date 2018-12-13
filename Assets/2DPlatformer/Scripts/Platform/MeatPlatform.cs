using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatPlatform : PlatformAbstract {

	public GameObject meat1;

	public GameObject meat2;

	public int direction = 0;

	void Awake() {
		length = 0;
		width = 0.64f;
		this.platformType = 3;
	}

	public override void init () {
		addMeat ();
	}

	/**
	 * @param direction: 0为向左边扔，1为向右边扔
	 */ 
	public void addMeat() {
		if (direction != 0 && direction != 1) {
			Debug.Log ("direction参数有误！");
		}
		Vector3[] pos = {new Vector3(length * width, 0, 1), new Vector3(- length * width, 0, 1)};
		int randint = Random.Range (0, 2);
		GameObject[] meatPrefabList = { meat1, meat2 };
		GameObject meat = Instantiate(meatPrefabList [randint]);
		meat.transform.parent = this.gameObject.transform;
		meat.transform.localPosition = pos [direction];
		meat.GetComponent<SpriteRenderer> ().flipX = Random.Range (0, 2) == 0 ? true : false;
		boxList.Add (meat);
		length++;

	}
}
