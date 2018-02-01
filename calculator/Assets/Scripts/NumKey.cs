using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumKey : MonoBehaviour {

	[SerializeField]
	private int num;

	[SerializeField]
	private calculator calcu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PushButton()
	{
		calcu.PushNumKey(num);
	}
}
