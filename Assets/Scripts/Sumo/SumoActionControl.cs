using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumoActionControl : MonoBehaviour {

	// Use this for initialization
	public GameObject player;
	public GameObject enemy;
	public GameObject anchor;
	public Transform center;
	public SumoUIController ui;

	public float racingRadius;
	public float avatarWidth = 0.2f;
	private float playerFacingDirection;   //up is 0 degree, clockwise
	private float enemyFacingDirection;    //down is 180 degree, clockwise
	private Vector3 playerVelocity = Vector3.zero;
	private Vector3 enemyVelocity = Vector3.zero;
	private float rotateVelocity = 0f;

	public bool canStartAction = true;

	public int checkGameover() {  //0 not yet, 1 win, 2 lose.
		float playerDistance = (center.position - player.transform.position).magnitude;
		float enemyDistance = (center.position - enemy.transform.position).magnitude;
		if(enemyDistance > racingRadius) return 1;
		else if (playerDistance > racingRadius) return 2;
		else return 0;
	}
	void Start () {
		//RotateViaAnchor(20);
		//BeingPulledClockwise(3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void PushEnemy(float radius) {
		Debug.Log("PushEnemy");
		SetAnchorToCenter();
		ui.playerParticlePlay(anchor.transform.position);
		MoveViaAnchor(radius, enemy, 0.2f, 1);
	}

	public void BeingPushed(float radius) {
		Debug.Log("BeingPushed");
		SetAnchorToCenter();
		ui.enemyParticlePlay(anchor.transform.position);
		MoveViaAnchor(radius, player, 0.2f, 2);
	}

	public void PullEnemy(float radius) {
		Debug.Log("PullEnemy");
		SetAnchorToPlayer();
		ui.enemyParticlePlay(anchor.transform.position);
		RotateViaAnchor(180, 0.2f);
		MoveViaAnchor(radius, enemy, 0.2f, 1);
	}
	public void PullEnemyAntiClock(float radius) {
		Debug.Log("PullEnemyAntiClock");
		SetAnchorToPlayer();
		ui.enemyParticlePlay(anchor.transform.position);
		RotateViaAnchor(180, 0.2f);
		MoveViaAnchor(radius, enemy, 0.2f, 1);
	}
	public void PullEnemyClockwise(float radius) {
		Debug.Log("PullEnemyClockwise");
		SetAnchorToPlayer();
		ui.playerParticlePlay(anchor.transform.position);
		RotateViaAnchor(-180f, 0.2f);
		MoveViaAnchor(radius, enemy, 0.2f, 1);
	}
	public void BeingPulled(float radius) {
		Debug.Log("BeingPulled");
		SetAnchorToEnemy();
		ui.playerParticlePlay(anchor.transform.position);
		RotateViaAnchor(180, 0.2f);
		MoveViaAnchor(radius, player, 0.2f, 2);
	}
	public void BeingPulledClockwise(float radius) {
		Debug.Log("BeingPulledClockwise");
		SetAnchorToEnemy();
		ui.playerParticlePlay(anchor.transform.position);
		RotateViaAnchor(-180, 0.2f);
		MoveViaAnchor(radius, player, 0.2f, 2);
	}
	public void BeingPulledAntiClock(float radius) {
		Debug.Log("BeingPulledAntiClock");
		SetAnchorToEnemy();
		ui.playerParticlePlay(anchor.transform.position);
		RotateViaAnchor(180, 0.2f);
		MoveViaAnchor(radius, player, 0.2f, 2);
	}
	public void RotateTogetherClock(float angle, float playerRadius, float enemyRadius, int walkTo) {
		Debug.Log("RotateTogetherClock walkTo: " + walkTo);
		SetAnchorToCenter();
		ui.enemyParticlePlay(anchor.transform.position);
		ui.playerParticlePlay(anchor.transform.position);
		RotateViaAnchor(-angle, 0.1f);
		Debug.Log("playerRadius" + playerRadius);
		Debug.Log("enemyRadius" + enemyRadius);
		MoveViaAnchor(playerRadius, player, 0.1f, 0);
		MoveViaAnchor(enemyRadius, enemy, 0.2f, walkTo);
	}
	public void RotateTogetherAntiClock(float angle, float playerRadius, float enemyRadius, int walkTo) {
		Debug.Log("RotateTogetherAntiClock walkTo" + walkTo);
		SetAnchorToCenter();
		ui.enemyParticlePlay(anchor.transform.position);
		ui.playerParticlePlay(anchor.transform.position);
		RotateViaAnchor(angle, 0.1f);
		Debug.Log("playerRadius" + playerRadius);
		Debug.Log("enemyRadius" + enemyRadius);
		MoveViaAnchor(playerRadius, player, 0.1f, 0);
		MoveViaAnchor(enemyRadius, enemy, 0.2f, walkTo);
	}
	public void PushBackTogether(float playerRadius, float enemyRadius) {
		Debug.Log("PushBackTogether");
		SetAnchorToCenter();
		ui.enemyParticlePlay(anchor.transform.position);
		ui.playerParticlePlay(anchor.transform.position);
		MoveViaAnchor(playerRadius, player, 0.2f, 0);
		MoveViaAnchor(enemyRadius, enemy, 0.2f, 3);
	}
	public void PlayerWalkToEnemy() {
		Debug.Log("PlayerWalkToEnemy");
		KeepFacing();
		SetAnchorToEnemy();
		//float distance = (player.transform.position - enemy.transform.position).magnitude - avatarWidth;
		MoveViaAnchor(avatarWidth * 2f, player, 1f, 0);
	}

	public void EnemyWalkToPlayer() {
		Debug.Log("EnemyWalkToPlayer");
		KeepFacing();
		SetAnchorToPlayer();
		MoveViaAnchor(avatarWidth * 2f, enemy, 1f, 0);
	}
	
	public void WalkToCenter() {
		Debug.Log("WalkToCenter");
		KeepFacing();
		MoveViaAnchor(avatarWidth, player, 0.2f, 0);
		MoveViaAnchor(avatarWidth, enemy, 0.2f, 0);
	}
	

    void SetAnchorToPlayer() {
		Debug.Log("SetAnchorToPlayer");
        anchor.transform.position = player.transform.position;
		//Debug.Log(anchor.transform.position);
	}

	void SetAnchorToEnemy() {
		Debug.Log("SetAnchorToEnemy");
		anchor.transform.position = enemy.transform.position;
	}

	void SetAnchorToCenter() {
		Debug.Log("SetAnchorToCenter");
		anchor.transform.position = (player.transform.position + enemy.transform.position) / 2; 
	}

	void MoveViaAnchor(float radius, GameObject target, float moveTime, int walkTo) {  //positive is forward, negative is backward.
	    //walk to: 0 didnot walk ;1 player to enemy; 2 enemy to player
	    playerVelocity = Vector3.zero;
	    enemyVelocity = Vector3.zero;
		Debug.Log("MoveViaAnchor------Radius:" +radius);
		StartCoroutine(MoveCoroutine(radius, target, moveTime, walkTo));
	}
	
	void RotateViaAnchor(float angle, float rotateTime) {  // negative for clockwise
	    rotateVelocity = 0f;
		StartCoroutine(RotateCoroutine(angle, rotateTime));
	}

	IEnumerator RotateCoroutine(float angle, float rotateTime) {
		rotateVelocity = 0f;
		//Debug.Log(anchor.transform.position);
		float remainingAngle = angle;
		float sign = angle / Mathf.Abs(angle);
		float currAngle = 0f;
		while(true) {
		    //KeepFacing();
		    currAngle = Mathf.SmoothDamp(currAngle, remainingAngle, ref rotateVelocity, rotateTime);
			//Debug.Log(remainingAngle);
			//Debug.Log(rotateVelocity);
			//Debug.Log(currAngle - remainingAngle);
            //Debug.Log(currAngle - remainingAngle);
			player.transform.RotateAround(anchor.transform.position, Vector3.forward, rotateVelocity * Time.deltaTime);
		    enemy.transform.RotateAround(anchor.transform.position, Vector3.forward, rotateVelocity * Time.deltaTime);
			if (Mathf.Abs(currAngle - remainingAngle) > 0.0001f)
			    yield return null;
			else break;
		}
        
	}

	//positive is forward, negative is backward.

	IEnumerator MoveCoroutine(float distance, GameObject target, float moveTime, int walkTo = 0) { //distance means the distance between anchor and target(final status)
		float remainingSqrDistance = Mathf.Pow(distance, 2);
		playerVelocity = Vector3.zero;
		enemyVelocity = Vector3.zero;

		while(remainingSqrDistance > 0.001f) {
		    Vector2 radiusDir = target.transform.position - anchor.transform.position; // vector point to anchor
		    radiusDir.Normalize();
   

		    Vector2 radiusVec = distance * radiusDir;
		   // Debug.Log(radiusVec); 
			
			Vector3 end = anchor.transform.position + new Vector3 (radiusVec.x, radiusVec.y, 0);
			
			//Debug.Log("end" + end); 
	        if (target.name == "Player") {
			    target.transform.position = Vector3.SmoothDamp(target.transform.position, end, ref playerVelocity, moveTime);
				
				//Debug.Log("target position ---------" + target.transform.position);    
	     	}
	    	else if(target.name == "Enemy") {
	    	    target.transform.position = Vector3.SmoothDamp(target.transform.position, end, ref enemyVelocity, moveTime);
		    }
	    	else {
                break;
	    	}
			remainingSqrDistance = (target.transform.position - end).sqrMagnitude;
			
			//Debug.Log("end" + end); 
			
			//Debug.Log("remainingSqrDistance" + remainingSqrDistance); 
			
			//Debug.Log("playerVelocity" + playerVelocity); 
			yield return null;
		}
		    Vector2 finalRadiusDir = target.transform.position - anchor.transform.position; // vector point to anchor
		    finalRadiusDir.Normalize();
		    Vector2 finalRadiusVec = distance * finalRadiusDir;
			
			Vector3 finalEnd = anchor.transform.position + new Vector3 (finalRadiusVec.x, finalRadiusVec.y, 0);
			target.transform.position = finalEnd;
		int gameOver = checkGameover();
		switch(gameOver) {
			case 0:
			  break;
			case 1:
			  ui.enemyParticlePlay(enemy.transform.position);
			  enemy.GetComponent<SpriteRenderer>().enabled = false;
			  ui.HideEnemyPredict();
			  ui.showVictory();
			  yield break;
			case 2:
			  ui.playerParticlePlay(player.transform.position);
			  player.GetComponent<SpriteRenderer>().enabled = false;
			  ui.showDefeat();
			  yield break;
			default:
			break;
		}
		switch(walkTo) {
			case 0:
			  break;
			case 1:
			  PlayerWalkToEnemy();
			  canStartAction = true;
			  break;
			case 2:
			  EnemyWalkToPlayer();
			  canStartAction = true;
			  break;
			case 3:
			  WalkToCenter();
			  canStartAction = true;
			  break;
			default:
			  break;
		}

	}

	void KeepFacing() {
		SetAnchorToCenter();
		//player
		Vector2 playerDir = anchor.transform.position - player.transform.position;// vector point to anchor
		playerDir.Normalize();
		
		//float rotateAngle = Mathf.Acos(Vector2.Dot(playerDir,Vector2.up)) / Mathf.PI * 180;
		float rotateAngle = Vector2.SignedAngle(Vector2.up, playerDir);

        enemy.transform.localEulerAngles = new Vector3(0, 0, 180 + rotateAngle);
		player.transform.localEulerAngles = new Vector3(0, 0, rotateAngle);

		//playerFacingDirection = player.transform.localEulerAngles.z;
		//enemyFacingDirection = enemy.transform.localEulerAngles.z;
	}

	void CallBack(System.Action<int> theAction) {
        theAction(1);
    }
}
