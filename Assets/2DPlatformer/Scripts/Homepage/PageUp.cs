using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageUp : MonoBehaviour {

	public int curPage = 0;

	void OnMouseDown()
	{
		this.transform.parent.parent.GetComponent<Animator>().SetInteger("page", curPage - 1);
	}
}
