using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using global;
//using UnityEditor;
//using UnityEditorInternal;

public class PlayerPlatformerController : PhysicsObject {

	public float maxSpeed = 7;
//	public float jumpTakeOffSpeed = 7;

	public SpriteRenderer spriteRenderer;
	private Animator animator;

	public GameObject meatPrefab;

	public GameObject shootSpot;

    public enum bodyType{
        fat,
        middle
    }

    public bodyType type = bodyType.fat;

	//public UnityEditor.Animations.AnimatorController fatBody;

	//public UnityEditor.Animations.AnimatorController middleBody;

	private List<GameObject> meatList = new List<GameObject> ();

	private List<GameObject> meatPool = new List<GameObject> ();

	private bool isShooting = false;

	// Use this for initialization
	void Awake () 
	{
		spriteRenderer = GetComponent<SpriteRenderer> (); 
		animator = GetComponent<Animator> ();
	}

	void Update () 
	{
		targetVelocity = Vector2.zero;
        if (!MainGameController._instance.isGameOver)
        {
            ComputeVelocity();
            listenKeyPress();
        }
		checkChangeBody ();
	}

	void FixedUpdate()
	{
		velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
		velocity.x = targetVelocity.x;

		grounded = false;

		Vector2 deltaPosition = velocity * Time.deltaTime;

		Vector2 moveAlongGround = new Vector2 (groundNormal.y, -groundNormal.x);

		Vector2 move = moveAlongGround * deltaPosition.x;
		if (this.GetComponent<MeatController> ()) {
//			Debug.Log (string.Format ("{0},{1}", deltaPosition, moveAlongGround));
		}
		Movement (move, false);

		move = Vector2.up * deltaPosition.y;

		Movement (move, true);
	}

	protected override void ComputeVelocity()
	{
		Vector2 move = Vector2.zero;

		move.x = Input.GetAxis ("Horizontal");

		if (Input.GetButtonDown ("Jump") && grounded) {
			velocity.y = global.Global.getJumpTakeOffSpeed ();
		}
		 
		bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0f));
		if (flipSprite) 
		{
			spriteRenderer.flipX = !spriteRenderer.flipX;
			shootSpot.transform.localPosition = new Vector3(-shootSpot.transform.localPosition.x, shootSpot.transform.localPosition.y, 0);
		}

		animator.SetBool ("grounded", grounded);
		animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);

		targetVelocity = move * maxSpeed;
	}

	protected override void Movement(Vector2 move, bool yMovement)
	{
		if (move.y > 0) {
			this.canLand = false;
		}

		float distance = move.magnitude;
		if (distance > minMoveDistance) 
		{
			int count = rb2d.Cast (move, contactFilter, hitBuffer, distance + shellRadius);
			if (count == 0) {
				this.canLand = true;
			} else {
				hitBufferList.Clear ();
				for (int i = 0; i < count; i++) {
					hitBufferList.Add (hitBuffer [i]);
				}

				for (int i = 0; i < hitBufferList.Count; i++) 
				{
					Vector2 currentNormal = hitBufferList [i].normal;
                    //					Debug.Log(string.Format("position:{0}, {1}", currentNormal.x, currentNormal.y));
                    if (hitBufferList[i].collider.gameObject.tag == "platform")
                    {
                        hitBufferList[i].collider.gameObject.transform.parent.gameObject.GetComponent<PlatformAbstract>().isLanding = false;
                    }
                    //Debug.Log(string.Format("distance:{0},currentNormal.y:{1}", hitBufferList[i].distance, currentNormal.y));
                    if (currentNormal.y > 0.99) 
					{
                        //Debug.Log(string.Format("position:{0}, {1}", hitBufferList[i].transform.position.y + hitBuffer[i].transform.gameObject.GetComponent<BoxCollider2D>().bounds.size.y / 2, this.gameObject.transform.position.y - this.GetComponent<BoxCollider2D>().bounds.size.y / 2 + shellRadius));
                        if (hitBufferList[i].collider.gameObject.tag == "platform" &&
                            hitBufferList[i].transform.position.y + hitBuffer[i].transform.gameObject.GetComponent<BoxCollider2D>().bounds.size.y / 2 < this.gameObject.transform.position.y - this.GetComponent<BoxCollider2D>().bounds.size.y / 2) {
                            this.hitBufferList[i].collider.gameObject.SendMessageUpwards("notifyPlatformToLand", this.hitBufferList[i].collider.gameObject.transform.parent.gameObject, SendMessageOptions.DontRequireReceiver);
                            hitBufferList[i].collider.gameObject.transform.parent.gameObject.GetComponent<PlatformAbstract>().isLanding = true;
                            grounded = true;
                            //加分
                            //Debug.Log(string.Format("{0}, {1}, {2}", hitBufferList[i].collider.gameObject.transform.parent.gameObject.GetComponent<PlatformAbstract>().layer, Global.reachLayer, Global.baseLayer));
                            if (hitBufferList[i].collider.gameObject.transform.parent.gameObject.GetComponent<PlatformAbstract>().layer > Global.reachLayer)
                            {
                                int score = hitBufferList[i].collider.gameObject.transform.parent.gameObject.GetComponent<PlatformAbstract>().layer - Global.reachLayer;
                                Global.addScore(score, 1);
                                Global.reachLayer = hitBufferList[i].collider.gameObject.transform.parent.gameObject.GetComponent<PlatformAbstract>().layer;
                            }
                        }
                        else if (hitBufferList[i].collider.gameObject.tag == "platform" && velocity.y < 1f &&
                            hitBufferList[i].transform.position.y + hitBuffer[i].transform.gameObject.GetComponent<BoxCollider2D>().bounds.size.y / 2 < this.gameObject.transform.position.y - this.GetComponent<BoxCollider2D>().bounds.size.y / 2 + 0.1)
                        {
                            velocity.y = velocity.y + 0.2f;

                        }
                        else if (hitBufferList[i].collider.gameObject.tag == "ground")
                        {
                            grounded = true;
                        }
                        if (yMovement)
                        {
                            groundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }
//					//道具
					if (hitBufferList [i].collider.gameObject.tag == "platform" && hitBufferList [i].collider.gameObject.GetComponent<BoxController>().type == 1) {
						hitBufferList [i].collider.gameObject.GetComponent<BoxController> ().type = 0;
						hitBufferList [i].collider.gameObject.GetComponent<BoxController> ().normalSprite ();
						GameObject food = FoodFactory._instance.generateAnFood ();
						food.transform.parent = this.gameObject.transform.parent;
						Vector2 collidePos = hitBufferList [i].point;
						food.transform.position = collidePos;
						Vector2 pos = MainGameController._instance.getAnUpBoxPos (collidePos.y);
						float dx = pos.x - collidePos.x;
						float dy = pos.y - collidePos.y;
//						Debug.Log(string.Format("dx,dy:{0}, {1}", dx, dy));
//						Debug.Log(string.Format("gravity:{0}", Physics2D.gravity.y));
						float durateTime = food.GetComponent<FoodController> ().durateTime;
						Vector2 velocity = new Vector2 (dx / durateTime, (-Physics2D.gravity.y * durateTime * durateTime + 2.0f * dy) / 2.0f * durateTime);
                        Debug.Log(string.Format("-{0} * {1} ^ 2 + 2.0f * {2}) / 2.0f * {3}", Physics2D.gravity.y, durateTime, durateTime, dy));
                        Debug.Log(string.Format("velocity:{0}", velocity));
                        //						Debug.Log(string.Format("{0}, {1}", velocity.x, velocity.y));
                        food.GetComponent<FoodController> ().setVelocity (velocity);
					}
					if (hitBufferList [i].collider.gameObject.tag == "food" && hitBufferList [i].collider.gameObject.GetComponent<FoodController>().isCanEat()) {
						hitBufferList [i].collider.gameObject.GetComponent<FoodController> ().Eat ();
						continue;
					}
					if ((hitBufferList [i].collider.gameObject.tag == "platform" && hitBufferList [i].collider.gameObject.transform.parent.gameObject.GetComponent<PlatformAbstract> ().isLanding == true)
						|| (hitBufferList [i].collider.gameObject.tag == "wall") || (hitBufferList [i].collider.gameObject.tag == "ground")) {
                        float projection = Vector2.Dot(velocity, currentNormal);
                        if (projection < 0)
                        {
                            velocity = velocity - projection * currentNormal;
                        }

                        float modifiedDistance = hitBufferList[i].distance - shellRadius;
                        distance = modifiedDistance < distance ? modifiedDistance : distance;
                    }
                    if ((hitBufferList [i].collider.gameObject.tag == "platform" && hitBufferList [i].collider.gameObject.transform.parent.gameObject.GetComponent<PlatformAbstract>().platformType == 2)) {
						hitBufferList [i].collider.gameObject.transform.parent.gameObject.GetComponent<CheckPointPlatform>().checkPoint();
					}
				}
			}
		}
		rb2d.position = rb2d.position + move.normalized * distance;
	}

	protected override void listenKeyPress() {
		if (Input.GetKey(KeyCode.J) && !isShooting) {
			this.GetComponent<Animator> ().Play ("PlayerThrow", 0, 0f);
			isShooting = true;
			Invoke ("onShootComplete", 0.5f);
			if (global.Global.weight >= Config.minWeight + 5 && type == bodyType.fat) {
				Invoke("shoot", 0.3f);
			}
		}
		//if (Input.GetKey(KeyCode.K)) {
		//	this.GetComponentInParent<MainGameController> ().resetStart ();
		//}
	}

	protected void checkChangeBody() {
        if (global.Global.weight < 120 && type == bodyType.fat)
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0);
        }
        if (global.Global.weight < 120 && type == bodyType.middle)
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            Debug.Log("变瘦啦。");
        }
        if (global.Global.weight >= 120 && type == bodyType.fat)
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        if (global.Global.weight >= 120 && type == bodyType.middle)
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            Debug.Log("变胖啦。");
        }
    }

	void shoot() {
		animator.SetBool ("isThrowing", true);
		GameObject meat;
		if (meatPool.Count > 0) {
			meat = meatPool [0];
			meatPool.Remove (meat);
		} else {
			meat = Instantiate (meatPrefab);
		}
		Vector2 velocity = spriteRenderer.flipX ? new Vector2 (-4, 4) : new Vector2 (4, 4);
		meat.GetComponent<MeatController> ().setVelocity (velocity);
		meat.transform.position = new Vector3 (shootSpot.transform.position.x, shootSpot.transform.position.y, 0);
		meat.transform.parent = this.gameObject.transform.parent;
		global.Global.throwMeat ();
	}

	public void onShootComplete() {
		isShooting = false;
		animator.SetBool ("isThrowing", false);
	}
}