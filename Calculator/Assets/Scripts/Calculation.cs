using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculation : MonoBehaviour {

    private float anser; //答え

    [SerializeField]
    private List<string> keydata;  //押されたボタンを格納
    
    //外部から押されたキーを取得
    public void SetKeyData(string key)
    {
        keydata.Add(key);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //イコールが押されたら計算開始
        if(keydata.Contains("="))
        {
            print("計算");
        }

        //DELが押されたら末尾の文字列とdelを削除
        if (keydata.Contains("del"))
        {
            keydata.RemoveRange(keydata.Count - 2, 2);
        }

        //Cが押されたら消去
        if (keydata.Contains("c"))
        {
            keydata.Clear();
        }
    }
}
