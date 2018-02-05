using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorKey : MonoBehaviour {

	[SerializeField]
	private char operatorkey;

	[SerializeField]
	private calculator calcu;


    [SerializeField]
    private string seName;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PushButton()
	{

        AudioManager.Instance.PlaySE(seName);

		calcu.PushOperatorKey(operatorkey);
	}
}