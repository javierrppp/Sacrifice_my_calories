using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumoGame : MonoBehaviour {

	// Use this for initialization
	public SumoEnemyBarControl enemyBar;
	public SumoPlayerBarControl playerBar;
	public SumoActionControl actionControl;
	public SumoUIController ui;

	public float powerRatio = 100f;
	public int[,] bossWeight = new int[2,3]; 
	float playerPowerPercent;
	float enemyPowerPercent;
	int enemyDirection;  // 0 up, 1 down, 2 left, 3 right
	int playerDirection;
	bool canPressDirection = false;
	bool cursorStarted = false;
	float enemyWeight = 100f;
	void Start () {
		
		//actionControl.PullEnemy(2f);
		//actionControl.PlayerWalkToEnemy();
		actionControl.WalkToCenter();
		int currBoss = GameController._instance.currentBoss;
		enemyWeight = Random.Range((currBoss + 1) * 50, (currBoss + 2) * 50);
		ui.RefreshBoss(enemyWeight);
		//enemyWeight = Random.Range(100, 150);
		//enemyWeight = Random.Range(150, 200);
		StartRound();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("lllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllll"+(Input.GetKeyDown(KeyCode.Space) && cursorStarted == true && actionControl.canStartAction == true));
		if(Input.GetKeyDown(KeyCode.Space) && cursorStarted == true && actionControl.canStartAction == true) {
			cursorStarted = false;
			actionControl.canStartAction = false;
			enemyPowerPercent = enemyBar.StopCursor();
			playerPowerPercent = playerBar.StopCursor();
			ui.ShowEnemyChoice(enemyDirection);
			JudgeAction();
			Invoke("StartRound", 3.5f);
		}
	}
	void JudgeAction() {
          float playerPower = GameController._instance.weight * playerPowerPercent *  powerRatio / 100;
		  float enemyPower = enemyWeight * enemyPowerPercent *  powerRatio / 100;
		  Debug.Log("playerPower: "+ playerPower);
		  Debug.Log("enemyPower: "+ enemyPower);

          Vector2 playerVec, enemyVec;
			  switch(playerDirection) {
				  case 0:
				    playerVec = Vector2.up;
				    break;
				  case 1:
				    playerVec = Vector2.down;
				    break;
				  case 2:
				    playerVec = Vector2.left;
				    break;
				  case 3:
				    playerVec = Vector2.right;
				    break;
				  default:
				    playerVec = Vector2.zero;
				    break;
			  }
			  switch(enemyDirection) {
				  case 0:
				    enemyVec = Vector2.down;
				    break;
				  case 1:
				    enemyVec = Vector2.up;
				    break;
				  case 2:
				    enemyVec = Vector2.right;
				    break;
				  case 3:
				    enemyVec = Vector2.left;
				    break;
				  default:
				    enemyVec = Vector2.zero;
				    break;
			  }
			  Vector2 combineDirection = enemyVec * enemyPower + playerVec * playerPower;
			  Debug.Log("combineDirection " + combineDirection);
			  Vector2 normalCombine = combineDirection.normalized;

		  if (enemyDirection == 0 && playerDirection == 0) {
              // same push
		      float distance = Mathf.Abs(playerPower - enemyPower) / 100 * 2;
		      if(playerPower > enemyPower) actionControl.PushEnemy(distance);
			  else if (playerPower < enemyPower) actionControl.BeingPulled(distance);

		  }
		  else if((enemyDirection == 1 && playerDirection == 1) ||
			      (enemyDirection == 3 && playerDirection == 2)) {
              // same push
		      float distance = Mathf.Abs(playerPower - enemyPower) / 100 * 2;
		      if(playerPower > enemyPower) actionControl.PullEnemyAntiClock(distance);
			  else if (playerPower < enemyPower) actionControl.BeingPulledClockwise(distance);

		  }
		  else if(enemyDirection == 2 && playerDirection == 3) {
              // same push
		      float distance = Mathf.Abs(playerPower - enemyPower) / 100 * 2;
		      if(playerPower > enemyPower) actionControl.PullEnemyClockwise(distance);
			  else if (playerPower < enemyPower) actionControl.BeingPulledAntiClock(distance);

		  }
		  else if(enemyDirection == 0 && playerDirection == 1) {
              // same push
		      float distance = Mathf.Abs(playerPower + enemyPower) / 100 * 2;
		      if(playerPower > enemyPower) actionControl.PullEnemyClockwise(distance);
			  else if (playerPower < enemyPower) actionControl.BeingPushed(distance);

		  }
		  else if(enemyDirection == 1 && playerDirection == 0) {
              // same push
		      float distance = Mathf.Abs(playerPower + enemyPower) / 100 * 2;
		      if(playerPower > enemyPower) actionControl.PushEnemy(distance);
			  else if (playerPower < enemyPower) actionControl.BeingPulledClockwise(distance);

		  }
		  else if(enemyDirection == 3 && playerDirection == 3) {
              // same push
		      float distance = Mathf.Abs(playerPower + enemyPower) / 100 * 2;
		      if(playerPower > enemyPower) actionControl.PullEnemyClockwise(distance);
			  else if (playerPower < enemyPower) actionControl.BeingPulledClockwise(distance);

		  }
		  else if(enemyDirection == 2 && playerDirection == 2) {
              // same push
		      float distance = Mathf.Abs(playerPower + enemyPower) / 100 * 2;
		      if(playerPower > enemyPower) actionControl.PullEnemyAntiClock(distance);
			  else if (playerPower < enemyPower) actionControl.BeingPulledAntiClock(distance);

		  }
		  else if (playerDirection == 3 || enemyDirection == 3) {   //合力向右旋转90的方向与竖向量夹角
		      Debug.Log("Different axis clock");
		      int walkTo = 0;
		      if (playerPower > enemyPower) walkTo = 1;
			  else walkTo = 2;
			  Vector2 rotatedCombine = Vector2.Perpendicular(normalCombine);
			  Vector2 projection = (new Vector2(0, normalCombine.y)).normalized;
		      Debug.Log("normalCombine " + normalCombine.ToString("f5"));
		      Debug.Log("rotatedCombine " + rotatedCombine.ToString("f5"));
		      Debug.Log("projection " + projection.ToString("f5"));
			  float angle = Mathf.Acos(Vector2.Dot(rotatedCombine, projection)) / Mathf.PI * 180;
		      Debug.Log("angle " + angle.ToString("f5"));
		      actionControl.RotateTogetherClock(angle, playerPower / 100 * 2, enemyPower / 100 * 2, walkTo);

		  }
		  else if (enemyDirection == 2 || playerDirection == 2) {
		      Debug.Log("Different axis anticlock");
		      int walkTo = 0;
		      if (playerPower > enemyPower) walkTo = 1;
			  else walkTo = 2;
			  Vector2 rotatedCombine = -Vector2.Perpendicular(normalCombine);
			  Vector2 projection = (new Vector2(0, normalCombine.y)).normalized;
			  float angle = Mathf.Acos(Vector2.Dot(rotatedCombine, projection)) / Mathf.PI * 180;
		      Debug.Log("normalCombine " + normalCombine.ToString("f5"));
		      Debug.Log("rotatedCombine " + rotatedCombine.ToString("f5"));
		      Debug.Log("projection " + projection.ToString("f5"));
			  Debug.Log("angle: " + angle.ToString("f5"));
		      actionControl.RotateTogetherAntiClock(angle, playerPower / 100 * 2, enemyPower / 100 * 2, walkTo);

		  }
		  else {
              Debug.Log("No Action error.");
		  }
	}
	void StartRound() {
		ui.recoverMyUI();
		DetermineEnemyDirection();
		canPressDirection = true;
	}
	void DetermineEnemyDirection() {
        enemyDirection = Random.Range(0, 4);
        ui.ShowEnemyPredict(enemyDirection);
	}
	void DeterminePower() {
		enemyBar.StartCursor();
		playerBar.StartCursor();
		//Debug.Log("________________________________________determinePower");
		cursorStarted = true;
	}
	public void OnDirectionPress(int dir) {
		//Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
		if(canPressDirection == false) return;
		canPressDirection = false;
		ui.ShowPlayerChoice(dir);
        playerDirection = dir;
		DeterminePower();
	}
}
