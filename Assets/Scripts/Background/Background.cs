using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

	public GameObject LeftWall1;

	public GameObject LeftWall2;

	public GameObject RightWall1;

	public GameObject RightWall2;

	public GameObject player;

	public GameObject bg1;

	public GameObject bg2;

	public GameObject bg3;

	private List<GameObject> sortBgList = new List<GameObject>();

	// 先写死吧。。。
	private const float height = 12.8f;

	/**
	 * @param top: 
	 */
	private int curTopWall = 1;

	void Start() {
		
		GameObject[] bgList = {bg1, bg2};
		int index = Random.Range (0, 2);
		bgList[index].transform.position = new Vector2 (0, -height);
		sortBgList.Add (bgList[index]);
		bg3.transform.position = new Vector2 (0, 0);
		sortBgList.Add (bg3);
		index = (index + 1) % 2;
		bgList[index].transform.position = new Vector2 (0, height);
		sortBgList.Add (bgList[index]);
	}

	public void reset() {
		sortBgList[0].transform.position = new Vector2 (0, -height);
		sortBgList[1].transform.position = new Vector2 (0, 0);
		sortBgList[2].transform.position = new Vector2 (0, height);
		LeftWall1.transform.position = new Vector2 (LeftWall1.transform.position.x, 0);
		LeftWall2.transform.position = new Vector2 (LeftWall2.transform.position.x, 19);
		RightWall1.transform.position = new Vector2 (RightWall1.transform.position.x, 0);
		RightWall2.transform.position = new Vector2 (RightWall2.transform.position.x, 19);
	}

	// Update is called once per frame
	void Update () {
		GameObject topWall1, topWall2, downWall1, downWall2;
		if (curTopWall == 0) {
			topWall1 = LeftWall1;
			topWall2 = RightWall1;
			downWall1 = LeftWall2;
			downWall2 = RightWall2;
		} else {
			topWall1 = LeftWall2;
			topWall2 = RightWall2;
			downWall1 = LeftWall1;
			downWall2 = RightWall1;
		}
		if (this.player.transform.position.y > topWall1.transform.position.y) {
			downWall1.transform.position += new Vector3(0, downWall1.GetComponent<BoxCollider2D> ().bounds.size.y, 0);
			downWall2.transform.position += new Vector3(0, downWall2.GetComponent<BoxCollider2D> ().bounds.size.y, 0);
			curTopWall = (curTopWall + 1) % 2;
		} else if (this.player.transform.position.y < downWall1.transform.position.y) {
			//			topWall1.transform.position -= new Vector3(0, topWall1.GetComponent<BoxCollider2D> ().bounds.size.y, 0);
			//			topWall2.transform.position -= new Vector3(0, topWall2.GetComponent<BoxCollider2D> ().bounds.size.y, 0);
			//			top = (top - 1) % 2;
		}
		GameObject topBg, downBg, middleBg;
		topBg = sortBgList [2];
		middleBg = sortBgList [1];
		downBg = sortBgList [0];
		if (this.player.transform.position.y > middleBg.transform.position.y) {
			downBg.transform.position += new Vector3(0, height * 3, 0);
			sortBgList.Remove (downBg);
			sortBgList.Add (downBg);
		}
	}
}
