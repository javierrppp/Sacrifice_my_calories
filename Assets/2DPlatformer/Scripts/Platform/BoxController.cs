using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour {

	public int type = 0; //0: normal; 1: prop

	public Sprite initBoxSprite;

	public Sprite propBoxSprite;

	public void resetSprite() {
		this.GetComponent<SpriteRenderer> ().sprite = propBoxSprite;
	}

	public void normalSprite() {
		this.GetComponent<SpriteRenderer> ().sprite = initBoxSprite;
	}

	public void renderColor() {
		this.gameObject.GetComponent<SpriteRenderer> ().color = new Color (215, 142, 0);

		Debug.Log (this.gameObject.GetComponent<SpriteRenderer> ().flipX);
	}
}
