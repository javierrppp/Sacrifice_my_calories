using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using global;

public class FoodController : PhysicsObject {

	public Config.food type = Config.food.APPLE;

	private bool transmit = true;

	public float durateTime = 0.4f;
	public void setVelocity(Vector2 velocity) {
		this.velocity = velocity;
		targetVelocity = velocity;
		Invoke ("stop", durateTime);
	}

	void stop() {
		velocity.x = 0;
		targetVelocity.x = 0;
		this.transmit = false;
		this.canEat = true;
	}

	void FixedUpdate()
	{
        if (type == Config.food.POT)
        {
            return;
        }
//		Debug.Log (this.gameObject.transform.position.y);
		velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
		velocity.x = targetVelocity.x;

		grounded = false;

		Vector2 deltaPosition = velocity * Time.deltaTime;

		Vector2 move = deltaPosition;
		Movement (move, false);

		move = Vector2.up * deltaPosition.y;

		Movement (move, true);
	}

	protected override void Movement(Vector2 move, bool yMovement)
	{
		float distance = move.magnitude;
		if (transmit == true) {
			
		}
		else if (distance > minMoveDistance) 
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
//					if ((!this.canLand || !yMovement) && hitBufferList [i].collider.gameObject.tag == "platform") {
//						continue;
//					}
					if (hitBufferList [i].collider.gameObject.tag == "player") {
						continue;
					}
					if (currentNormal.y > minGroundNormalY) 
					{
						//						Debug.Log(string.Format("position:{0}, {1}", hitBufferList [i].point, this.gameObject.transform.position.y - 0.7676489 / 2));
						if (hitBufferList [i].collider.gameObject.tag == "platform" &&
                             hitBufferList[i].transform.position.y + hitBuffer[i].transform.gameObject.GetComponent<BoxCollider2D>().bounds.size.y / 2 < this.gameObject.transform.position.y - this.GetComponent<BoxCollider2D>().bounds.size.y / 2) {

							float projection = Vector2.Dot (velocity, currentNormal);
							if (projection < 0) 
							{
								velocity = velocity - projection * currentNormal;
                            }
                            float modifiedDistance = hitBufferList[i].distance - shellRadius;
                            distance = modifiedDistance < distance ? modifiedDistance : distance;
                        }
						if (yMovement) 
						{
							groundNormal = currentNormal;
							currentNormal.x = 0;
						}
					}
                }
			}
		}
		rb2d.position = rb2d.position + move.normalized * distance;
	}

	private bool canEat = false;

	public void Eat() {
		if (canEat) {
			int weight;
			Config.weights.TryGetValue (type, out weight);
			Global.eatFood (weight);
            Global.addScore(Mathf.Abs(weight), 0);
			Destroy (this.gameObject);
		}
	}

	public bool isCanEat() {
		return this.canEat;
	}

	public void setCanEat(bool canEat) {
		this.canEat = canEat;
	}

}
