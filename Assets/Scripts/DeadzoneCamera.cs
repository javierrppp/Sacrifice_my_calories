using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Camera))]
public class DeadzoneCamera : MonoBehaviour 
{
    public Renderer target;
    public Rect deadzone;
    public Vector3 smoothPos;

    public Rect[] limits;

    protected Camera _camera;
    protected Vector3 _currentVelocity;

	public bool activeCamera = false;
	private bool beginMove = false;
	private float curPosY = 0;

    public void Start()
    {
        smoothPos = target.transform.position;
        smoothPos.z = transform.position.z;
        _currentVelocity = Vector3.zero;

        _camera = GetComponent<Camera>();
        if(!_camera.orthographic)
        {
            Debug.LogError("deadzone script require an orthographic camera!");
            Destroy(this);
        }
    }

	public void MoveToSourceNode() {
		if (this.transform.position.y > 12.8f) {
			this.transform.position = new Vector2 (0, 12.8f);
		}
		curPosY = this.transform.position.y;
		beginMove = true;
	}

	public void FixedUpdate() {
		if (beginMove) {
			float posY = this.transform.position.y;
			float diff = curPosY / 200;
			posY = posY - diff;
			if (posY <= 0) {
				posY = 0;
				beginMove = false;
			}
			this.transform.position = new Vector2 (0, posY);
		}
	}

    public void Update()
    {
		if (target.transform.position.y > transform.position.y && !activeCamera) {
			activeCamera = true;
		}
//        float localX = target.transform.position.x - transform.position.x;
        float localY = target.transform.position.y - transform.position.y;

//        if (localX < deadzone.xMin)
//        {
//            smoothPos.x += localX - deadzone.xMin;
//        }
//        else if (localX > deadzone.xMax)
//        {
//            smoothPos.x += localX - deadzone.xMax;
//        }

//        if (localY < deadzone.yMin)
//        {
//            smoothPos.y += localY - deadzone.yMin;
//        }
		if (localY > deadzone.yMax) {
			smoothPos.y += localY - deadzone.yMax;
		} 
		if (localY < 0 && !activeCamera) {
			smoothPos.y = 0;
		}
        Rect camWorldRect = new Rect();
        camWorldRect.min = new Vector2(smoothPos.x - _camera.aspect * _camera.orthographicSize, smoothPos.y - _camera.orthographicSize);
        camWorldRect.max = new Vector2(smoothPos.x + _camera.aspect * _camera.orthographicSize, smoothPos.y + _camera.orthographicSize);

        for (int i = 0; i < limits.Length; ++i)
        {
            if (limits[i].Contains(target.transform.position))
            {
                Vector3 localOffsetMin = limits[i].min + camWorldRect.size * 0.5f;
                Vector3 localOffsetMax = limits[i].max - camWorldRect.size * 0.5f;

                localOffsetMin.z = localOffsetMax.z = smoothPos.z;

                smoothPos = Vector3.Max(smoothPos, localOffsetMin);
                smoothPos = Vector3.Min(smoothPos, localOffsetMax);

                break;
            }
        }

        Vector3 current = transform.position;
        current.x = smoothPos.x; // we don't smooth horizontal movement

		transform.position = Vector3.SmoothDamp(current, smoothPos, ref _currentVelocity, 0.1f);

		//Debug.Log(string.Format("{0}, {1}", localY, deadzone.yMin - 4 * target.GetComponent<CapsuleCollider2D>().bounds.size.y));
		if (localY < deadzone.yMin - 4 * target.GetComponent<BoxCollider2D>().bounds.size.y && activeCamera)
        {
            if (HealthController._instance.hurt())
            {
                MainGameController._instance.resurrect();
                this.activeCamera = false;
            } else
            {
                MainGameController._instance.gameOver();
            }
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(DeadzoneCamera))] 
public class DeadZonEditor : Editor
{
    public void OnSceneGUI()
    {
        DeadzoneCamera cam = target as DeadzoneCamera;

        Vector3[] vert = 
        {
            cam.transform.position + new Vector3(cam.deadzone.xMin, cam.deadzone.yMin, 0),
            cam.transform.position + new Vector3(cam.deadzone.xMax, cam.deadzone.yMin, 0),
            cam.transform.position + new Vector3(cam.deadzone.xMax, cam.deadzone.yMax, 0),
            cam.transform.position + new Vector3(cam.deadzone.xMin, cam.deadzone.yMax, 0)
        };

        Color transp = new Color(0, 0, 0, 0);
        Handles.DrawSolidRectangleWithOutline(vert, transp, Color.red);

        for(int i = 0; i < cam.limits.Length; ++i)
        {
            Vector3[] vertLimit =
           {
                new Vector3(cam.limits[i].xMin, cam.limits[i].yMin, 0),
                new Vector3(cam.limits[i].xMax, cam.limits[i].yMin, 0),
                new Vector3(cam.limits[i].xMax, cam.limits[i].yMax, 0),
                new Vector3(cam.limits[i].xMin, cam.limits[i].yMax, 0)
            };

            Handles.DrawSolidRectangleWithOutline(vertLimit, transp, Color.green);
        }
    }
}
#endif