using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

	public float minGroundNormalY = .65f;
	public float gravityModifier = 1f;

	protected Vector2 targetVelocity;
	protected bool grounded;
	protected Vector2 groundNormal;
	protected Rigidbody2D rb2d;
	protected Vector2 velocity;
	protected ContactFilter2D contactFilter;
	protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
	protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16);


	protected const float minMoveDistance = 0.001f;
	protected const float shellRadius = 0.01f;

	//解决跳上去的时候无视碰撞体，下来时却出现bug的短暂解决办法
	protected bool canLand = false;

	void OnEnable()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void Start () 
	{
		contactFilter.useTriggers = false;
		contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer));
		contactFilter.useLayerMask = true;
	}

	protected virtual void ComputeVelocity()
	{

	}

	protected virtual void listenKeyPress() {

	}

	protected virtual void Movement(Vector2 move, bool yMovement)
	{
	}

}