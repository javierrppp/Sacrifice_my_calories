using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : PlatformAbstract {

	void Awake() {
		this.platformType = 1;
	}

	protected override void move() {
		float result = this.initPosX - Mathf.Sin (this.gameTime) * this.moveRate;
		this.gameObject.transform.localPosition = Vector2.MoveTowards(this.gameObject.transform.localPosition,
			new Vector2(result, this.gameObject.transform.localPosition.y), 0.1f);
	}
}
