using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumoPhysics : MonoBehaviour {

	// Use this for initialization
	private Rigidbody2D rb2D;
	private Vector3 velocity = Vector3.zero;

	private float facingDirection;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/*public void Move(Vector2 moveVector, float moveTime) {
		Vector2 start = transform.position;
		Vector2 end =  start + moveVector;
		Debug.Log(moveVector);
		StartCoroutine(SmoothMovement(end, moveTime));
	}

	protected IEnumerator SmoothMovement(Vector3 end, float moveTime) {
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
		while(sqrRemainingDistance > float.Epsilon) {
			transform.position = Vector3.SmoothDamp(transform.position, end, ref velocity, moveTime);
			//transform.position = Vector3.MoveTowards(transform.position, end, 1/moveTime * Time.deltaTime);
			//rb2D.MovePosition(newPosition);
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;
			yield return null;
		}
	}*/

	/*void GetFacingDirection() {
		playerFacingDirection = player.transform.localEulerAngles.z;
		enemyFacingDirection = enemy.transform.localEulerAngles.z;
	}*/
}
