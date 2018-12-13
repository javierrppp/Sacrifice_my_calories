using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour {

	public GameObject movePlatform;

	public GameObject normalPlatform;

	public GameObject checkPointPlatform;

	public GameObject meatPlatform;

	public GameObject player;
    public GameObject middlePlayer;

    public GameObject leftWall;

	public GameObject rightWall;

	public GameObject background;

	public GameObject ground;

	public GameObject checkPointGround;

	public GameObject camera;

	private List<GameObject> platforms = new List<GameObject>();

	public List<GameObject> platformsPool = new List<GameObject> ();

	//用于计算层数的
	private int baseLayer = 0;

    private static MainGameController mainGameController;

    public static MainGameController _instance
    {
        get
        {
            if (!mainGameController)
            {
                mainGameController = FindObjectOfType(typeof(MainGameController)) as MainGameController;

                if (!mainGameController)
                {
                    GameObject _foodFactory = new GameObject();
                    mainGameController = _foodFactory.AddComponent<MainGameController>();
                }
                else
                {
                    mainGameController.Init();
                }
            }

            return mainGameController;
        }
    }

    private void Init()
    {

    }

    // Use this for initialization
    void Start () {
		if(GameController._instance.backFromBoss) {
			global.Global.layer = GameController._instance.checkpointLayer;
			global.Global.checkpointLayer = GameController._instance.checkpointLayer;
			global.Global.weight = (int)GameController._instance.weight;
			resurrect();
		}
		//		generatePlatforms (1);
	}

	// Update is called once per frame
	void Update () {
        //Debug.Log(GameController._instance.checkPointLayer);
		this.updatePlatforms ();

    }

	void generatePlatform() {
		int layer = global.Global.layer - baseLayer;
		GameObject platform;
		int cond1 = Random.Range (0, 100);
		int length = 2;
		int offset = layer / 2;
		if (offset > 40) {
			offset = 40;
		}
		if (cond1 < 20 + offset) {
			length = 2;
		}  else if (cond1 < 50 + offset) {
			length = 3;
		}  else if (cond1 < 70 + offset) {
			length = 4;
		}  else {
			length = 5;
		}
		if (layer % global.Config.checkPointPerPlatform == 0) {
			platform = generatePlatform (2, length);
		}  else {
			int cond = Random.Range (0, 100);
			if (cond < 80) {
				platform = generatePlatform (0, length);
			}  else {
				platform = generatePlatform (1, length);
			}
		}
		platform.GetComponent<PlatformAbstract>().initPosX = Random.value * 4 - 2;
		platform.GetComponent<PlatformAbstract>().moveRate = Random.value;
		platform.GetComponent<PlatformAbstract> ().length = length;
		if (platform.GetComponent<PlatformAbstract> ().boxList.Count == 0) {
			platform.GetComponent<PlatformAbstract> ().init ();
		}
		if (platform.GetComponent<PlatformAbstract> ().platformType == 2) {
			platform.transform.position = new Vector2 (0, getTransformPosition (layer));
		}  else {
			platform.transform.position = new Vector2 (platform.GetComponent<PlatformAbstract> ().initPosX, getTransformPosition (layer));
		}
		platform.transform.parent = this.gameObject.transform;
		this.platforms.Add (platform);
		global.Global.layer++;
	}

	GameObject generatePlatform(int type, int length) {
		GameObject platform = null;
		if (this.platformsPool.Count > 0) {
			foreach(GameObject plat in this.platformsPool) {
				if (plat.GetComponent<PlatformAbstract> ().platformType == type && plat.GetComponent<PlatformAbstract> ().length == length) {
					platform = plat;
					break;
				}
			}
			if (platform != null) {
				this.platformsPool.Remove (platform);
			}
		}
		if (platform == null) {
			if (type == 0) {
				platform = Instantiate (normalPlatform);
			}  else if (type == 1) {
				platform = Instantiate (movePlatform);
			}  else {
				platform = Instantiate (checkPointPlatform);
			}
		}
		return platform;
	}

	void generatePlatforms(int num) {
		for (int i = 0; i < num; i++) {
			generatePlatform ();
		}
	}

	void updatePlatforms() {
		int layer = global.Global.layer - baseLayer;
		if (player.transform.position.y >= this.getTransformPosition(layer - 10)) {
			this.generatePlatforms (10);
		}
	}

	float getTransformPosition(int layer) {
		if (layer <= 0) {
			return -100;
		}
		float distantRate = 0.39476f * Mathf.Log (layer * 0.1795f) + 1.7f;
		//Debug.Log (distantRate);
		return (layer * distantRate - 4.7f);
	}

	public Vector2 getAnUpBoxPos (float posY) {
		float offset = 4.0f;
		Vector2 pos = new Vector2 (0, 0); 
		foreach (GameObject platform in platforms) {
			if (platform.transform.position.y > posY && platform.transform.position.y <= posY + offset) {
				pos = platform.transform.position;
			}
		}
		return pos;
	}

	public void resurrect() {
		global.Global.layer = global.Global.checkpointLayer;
		baseLayer = global.Global.checkpointLayer;
		foreach (GameObject platform in platforms) {
			platformsPool.Add (platform); 			platform.GetComponent<PlatformAbstract> ().resetPlatform ();
			platform.transform.position = new Vector3 (10, -10);
		}
		platforms = new List<GameObject> ();
		//清除所有肉平台
		MeatPlatform[] meatPlatforms = this.GetComponentsInChildren<MeatPlatform>();
		for (int i = 0; i < meatPlatforms.Length; i++) {
			Destroy (meatPlatforms [i].gameObject);
		}
		//清除所有食物
		FoodController[] foods = this.GetComponentsInChildren<FoodController> ();
		for (int i = 0; i < foods.Length; i++) { 			Destroy (foods [i].gameObject); 		}
		updatePlatforms ();
		background.GetComponent<Background> ().reset ();
		if (global.Global.checkpointLayer >= global.Config.checkPointPerPlatform) {
			this.checkPointGround.SetActive (true);
			this.ground.SetActive (false);
		} else if (global.Global.checkpointLayer == 0)
        {
            this.checkPointGround.SetActive(false);
            this.ground.SetActive(true);
        }
		player.transform.localPosition = new Vector2 (0, 0);
        middlePlayer.transform.localPosition = new Vector2(0, 0);
        camera.GetComponent<DeadzoneCamera> ().activeCamera = false; 		camera.GetComponent<DeadzoneCamera> ().MoveToSourceNode ();
//		camera.transform.position = new Vector2 (0, 0);
	}

    public void gameOver()
    {
        restart();
    }

    public void restart()
    {
        HealthController._instance.resetHeart();
        global.Global.checkpointLayer = 0;
        resurrect();
    }

    /**
	 * 以下是消息通知
	 */
    void notifyPlatformToLand(GameObject platform) {
		platform.GetComponent<PlatformAbstract> ().land ();
	}

	void notifyDestroyPlatform(GameObject platform) {
		//将其设置到看不见的地方
		if (platform.GetComponent<PlatformAbstract> ().platformType == 3) {
			//肉平台则直接销毁
			if (platforms.Contains(platform)) {
				this.platforms.Remove (platform);
			}
			Destroy (platform);
		}  else {
			platform.transform.position = new Vector3 (10, -10);
			platform.GetComponent<PlatformAbstract> ().resetPlatform ();
			List<GameObject> boxList = platform.GetComponent<PlatformAbstract> ().boxList;
			foreach (GameObject box in boxList) {
				if (box.GetComponent<BoxController> ().type == 1) {
					box.GetComponent<BoxController> ().resetSprite ();
				}
			}
			if (!platformsPool.Contains(platform)) {
				this.platformsPool.Add (platform);
			}
			if (platforms.Contains(platform)) {
				this.platforms.Remove (platform);
			}
		}
	}

	void notifyGenerateMeatPlatform(Vector2 data) {
		int direction = (int)data.x;
		float y = data.y;
		float posX = 0;
		float width = 0.64f;
		if (direction == 0) {
			posX = leftWall.transform.position.x + leftWall.GetComponent<BoxCollider2D> ().bounds.size.x / 2 + width / 2;
		}  else {
			posX = rightWall.transform.position.x - rightWall.GetComponent<BoxCollider2D> ().bounds.size.x / 2 - width / 2;
		}
		GameObject platform = Instantiate (meatPlatform);
		platform.GetComponent<MeatPlatform> ().init ();
		platform.GetComponent<MeatPlatform> ().direction = direction;
		platform.transform.position = new Vector2 (posX, y);

		platform.transform.parent = this.gameObject.transform;
		//		this.platforms.Add (platform);
	}
}

