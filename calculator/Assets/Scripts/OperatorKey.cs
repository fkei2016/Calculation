using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorKey : MonoBehaviour {

	[SerializeField]
	private char operatorkey;

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
		calcu.PushOperatorKey(operatorkey);
	}
}