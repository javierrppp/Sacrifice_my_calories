using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using global;

public class PlatformAbstract : MonoBehaviour {

	public float deadTime = 0;

	//游戏经过的时间
	public float gameTime = 0;

	protected bool isCheckPoint = false;

	public int platformType = 0;	//0为normal， 1为move， 2为checkPoint, 3为meat, 4为道具

	protected float currentTime = 0;

	protected bool isLanded = false;

	public bool isLanding = false;	//用来判断平台的遮挡关系

	public float initPosX = 0;

    public int layer = 0;

	//movePlatform专用，移动比率
	public float moveRate = 1;

	public GameObject box1;
	public GameObject box2;
	public GameObject box3;
	public GameObject box4;
	public GameObject box5;

	public int length = 0;

	public float width = 0.64f;  //写死写死写死

	public List<GameObject> boxList = new List<GameObject>();

	void setLength(int length) {
		this.length = length;
	}

	public virtual void init () {
		int randint = Random.Range (0, 100);
		if (randint < 40) {
			length = 2;
		} else if (randint < 70) {
			length = 3;
		} else if (randint < 95) {
			length = 4;
		} else {
			length = 5;
		}

		Vector2[] pos = { new Vector2(0, 0), new Vector2(-width, 0), new Vector2(width, 0), new Vector2(-2 * width, 0), new Vector2(2 * width, 0)};
		int[] lastBox = {0, 0, 0, 1, 2};
		int[] lastIndex = {0, 0, 0, 0, 0};

		GameObject[] boxes = { box1, box2, box3, box4, box5 };
		for (int i = 0; i < length; i++) {
			randint = Random.Range (0, 100);
			int boxId = Random.Range (0, 100);
			if (randint < 35) {
				boxId = 0;
			} else if (randint < 70) {
				boxId = 1;
			} else if (randint < 85) {
				boxId = 2;
			} else if (randint < 95) {
				boxId = 3;
			} else {
				boxId = 4;
			}
			if ((boxId == 3 && i > 0 && lastIndex [lastBox [i]] == 2) || (boxId == 2 && i > 0 && lastIndex [lastBox [i]] == 3)) {
				boxId = Random.Range (0, 2);
			}
			GameObject box = Instantiate(boxes [boxId]);
			box.transform.parent = this.gameObject.transform;
			box.transform.localPosition = pos [i];
			box.GetComponent<SpriteRenderer> ().flipX = Random.Range (0, 2) == 0 ? true : false;
			if (boxId == 4) {
				//一排最多只能有一个道具
				box.GetComponent<SpriteRenderer> ().flipX = false;
				boxes [4] = boxes [0];
			}
//			if (boxId == 2 && i > 0 && lastIndex == 3) {
//				box.GetComponent<SpriteRenderer> ().flipX = false;
//			}
			lastIndex [i] = boxId;
			boxList.Add(box);
		}
		renderColor ();
	}

	// Update is called once per frame
	void FixedUpdate() {
		this.gameTime += Time.deltaTime;
		this.move ();
	}

	/**
	 *  @param weight 根据weight来决定平台的消失时间
	 */
	public virtual void land() {
		this.deadTime = global.Global.getDeadTime();
		this.isLanded = true;
		Invoke ("changeColor", this.deadTime > 2 ? this.deadTime - 2 : 0);
		Invoke ("destroyObject", this.deadTime);
	}

	public virtual void renderColor() {
		;
	}

	void destroyObject() {
		//Destroy (this.gameObject);
		this.reviveColor();
		this.SendMessageUpwards ("notifyDestroyPlatform", this.gameObject);
	}

	void changeColor() {
		foreach (GameObject box in boxList) {
			Animator boxAnimate = box.GetComponent<Animator> ();
			boxAnimate.SetBool ("change", true);
		}
	}

	void reviveColor() {
		foreach (GameObject box in boxList) {
			Animator boxAnimate = box.GetComponent<Animator> ();
			boxAnimate.SetBool ("change", false);
		}
	}

	public void resetPlatform() {
		CancelInvoke ();
		reviveColor ();
	}

	protected virtual void move() {

	}
}
