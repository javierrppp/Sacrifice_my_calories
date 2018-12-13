using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SumoUIController : MonoBehaviour {

	// Use this for initialization
	public GameObject[] playerButtons; // 0 up, 1 down, 2 left, 3 right
	public GameObject[] enemyButtons;
	public Text myWeight;
	public Text enemyWeight;
	public Text enemyName;
	public SpriteRenderer bossSmallIconNode;
	public SpriteRenderer bossHeadNode;
	public Sprite[] bossSmallIcons= new Sprite[3];
	public Sprite[] bossHeads = new Sprite[3];
	public ParticleSystem playerParticle;
	public ParticleSystem enemyParticle;
	public GameObject victory;
	
	public GameObject defeat;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void enemyParticlePlay(Vector3 pos) {
		enemyParticle.transform.position = pos;
		enemyParticle.Play();
	}
	
	public void playerParticlePlay(Vector3 pos) {
		playerParticle.transform.position = pos;
		playerParticle.Play();
	}

	public void showVictory() {
        victory.GetComponent<Animation>().Play();
		GameController._instance.weight += 50;
		myWeight.text = "WEIGHT: "+ (int)GameController._instance.weight + "KG";
		Invoke("BackToMain", 3.5f);
	}
	public void showDefeat() {
        defeat.GetComponent<Animation>().Play();
		GameController._instance.weight -= 20;
		myWeight.text = "WEIGHT: "+ (int)GameController._instance.weight + "KG";
		Invoke("BackToMain", 3.5f);
	}
	public void ShowEnemyPredict(int enemyDirection) {
		switch(enemyDirection) {
			case 0:
			case 1:
		      enemyButtons[0].GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
		      enemyButtons[1].GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
			  enemyButtons[2].GetComponent<SpriteRenderer>().color = new Color(69f/255,69f/255,69f/255,255f/255);
			  enemyButtons[3].GetComponent<SpriteRenderer>().color = new Color(69f/255,69f/255,69f/255,255f/255);
			  break;
			case 2:
			case 3:
			  enemyButtons[0].GetComponent<SpriteRenderer>().color = new Color(69f/255,69f/255,69f/255,255f/255);
			  enemyButtons[1].GetComponent<SpriteRenderer>().color = new Color(69f/255,69f/255,69f/255,255f/255);
		      enemyButtons[2].GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
		      enemyButtons[3].GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
			  break;
			default:
			break;
		}
	}
	public void HideEnemyPredict() {
		      enemyButtons[0].GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
		      enemyButtons[1].GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
			  enemyButtons[2].GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
			  enemyButtons[3].GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
	}
	public void ShowPlayerChoice(int dir) {
		for(int i = 0; i < 4; ++i)
		    if(i == dir) continue;
		    else playerButtons[i].GetComponent<Image>().color = new Color(69f/255,69f/255,69f/255,255f/255);
	}
	public void ShowEnemyChoice(int dir) {
		for(int i = 0; i < 4; ++i)
		    if(i == dir) continue;
		    else enemyButtons[i].GetComponent<SpriteRenderer>().color = new Color(69f/255,69f/255,69f/255,255f/255);
	}
	public void recoverMyUI() {
		for (int i = 0; i < 4; ++i) playerButtons[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
	}
	public void RefreshBoss(float weightNum) {
		int bossNum = (GameController._instance.currentBoss) % 3;
		bossHeadNode.sprite = bossHeads[bossNum];
		bossSmallIconNode.sprite = bossSmallIcons[bossNum];
		myWeight.text = "WEIGHT: "+ (int)GameController._instance.weight + "KG";
		switch(GameController._instance.currentBoss) {
			case 0:
			enemyName.text = "J.J.";
			enemyWeight.text = "WEIGHT: "+ (int)weightNum + "KG";
			  break;
			case 1:
			enemyName.text = "MOTHER";
			enemyWeight.text = "WEIGHT: "+ (int)weightNum + "KG";
			  break;
			case 2:
			enemyName.text = "CHEN YA";
			enemyWeight.text = "WEIGHT: "+ (int)weightNum + "KG";
			break;
			default:
			break;

	}
	}
	void BackToMain() {
		GameController._instance.backFromBoss = true;
		GameController._instance.currentBoss += 1;
        SceneManager.LoadScene(1);
	}
	
}
