using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatController : PhysicsObject {

	public void setVelocity(Vector2 velocity) {
		this.velocity = velocity;
		targetVelocity = velocity;
	}

	void FixedUpdate()
	{
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

		rb2d.position = rb2d.position + move.normalized * distance;
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
					int direction = velocity.x > 0 ? 1 : 0;
					//用一个向量的x存储方向，因为我们只关心原来向量的y值
					Vector2 data = hitBufferList [i].point;
//					data = this.gameObject.transform.TransformVector (data);

					data.x = direction;
					if (hitBufferList [i].collider.gameObject.tag == "wall") {
						this.SendMessageUpwards ("notifyGenerateMeatPlatform", data);
						Destroy (this.gameObject);
						return;
					} else if (hitBufferList [i].collider.gameObject.tag == "platform" && hitBufferList [i].collider.gameObject.transform.parent.GetComponent<PlatformAbstract> ().platformType == 3) {
						hitBufferList [i].collider.gameObject.transform.parent.GetComponent<MeatPlatform> ().addMeat ();
						Destroy (this.gameObject);
						return;
					}
				}
			}
		}
	}

	protected override void ComputeVelocity()
	{
		;
	}
}
