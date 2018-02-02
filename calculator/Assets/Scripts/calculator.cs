using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class calculator : MonoBehaviour {

	[SerializeField]
	private float[] num;    //計算用の数字

	[SerializeField]
	private List<char> operatorkey = new List<char> (); //演算子記憶用

	[SerializeField]
	private int calculateNums = 0;  //演算子の押された回数

	[SerializeField]
	private Sprite[] nomalNumSpriteTexture;　//表示用数字のテクスチャ

    private int maxNum = 8;

    private int maxNumCount = 0;

    private Toast toast = new Toast();　//8桁表示用

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 数字キーを押されたときの処理
    /// </summary>
    /// <param name="number"></param>
	public void PushNumKey(int number)
	{

        //8桁用の制限処理
        if (maxNumCount >= maxNum)
        {
            //Toast.Create("最大桁数(8)を超えました");
#if UNITY_ANDROID && !UNITY_EDITOR
            toast.ToastView();
#endif

            return;
        }

        //小数点が押されたときの処理
		if (number == -10)
		{

			return;
		}
		
        //00が押されたとき	
		//if (number == 10) 
		//{
		//	if (num [calculateNums] >= 0) 
		//	{
		//		num [calculateNums] *= 100;
		//	}
        //          viewDestroy();
        //          view();
        //          return;
		//}


        //二桁目からの処理
		if (num [calculateNums] >= 0)
		{
			num [calculateNums] *= 10;
			num [calculateNums] += number;
		}
        //一桁目の処理
		else 
		{
			num [calculateNums] += number;
		}

        //前に描画されているものを消す
		viewDestroy ();
        //新しく描画しなおす
		view ();

        //桁数のカウントアップ
        maxNumCount++;

	}

    /// <summary>
    ///特殊キーが押されたときの処理
    /// </summary>
    /// <param name="keyname"></param>
	public void PushOperatorKey(char keyname)
	{

        //Deleteキーが押されたら
        //if (keyname == 'D') 
        //{
        //	num [calculateNums] /= 10;
        //	num [calculateNums] = (int)num [calculateNums];
        //  viewDestroy();
        //  view();
        //} 

        //ACキーが押されたら
        if (keyname == 'A')
        {
            Array.Clear(num, 0, num.Length);
            calculateNums = 0;
            operatorkey.Clear();
            viewDestroy();
        }
        //Cキーが押されたら
        else if (keyname == 'C')
        {
            Array.Clear(num, 0, num.Length);
            calculateNums = 0;
            operatorkey.Clear();
            viewDestroy();
        }
        //イコールが押されたら
        else if (keyname == '=')
        {

        }
        //四則演算子が押されたら
        else
        {
            Array.Resize(ref num, num.Length + 1);
            calculateNums++;
            operatorkey.Add(keyname);
            viewDestroy();
            view();
        }

        //特殊キーが押されたら桁数を初期化
        maxNumCount = 0;

	}
    
    //数値や演算子などの描画
	void view()
	{

        List<int> number = new List<int>();
        for (int i = calculateNums; i >= 0; i--) 
		{

            var numbers = (int)num[i];
            var digit = numbers;

            while (digit != 0)
            {
                numbers = digit % 10;
                digit /= 10;
                number.Add(numbers);
            }

            if (operatorkey.Count > 0 && i > 0)
            {
                var a = operatorkey[i - 1];
                if (a == '*')
                    number.Add(10);
                if (a == '/')
                    number.Add(11);
                if (a == '+')
                    number.Add(12);
                if (a == '-')
                    number.Add(13);
            }
        }


        //イメージを生成して順番に表示
        GameObject.Find("ScoreImage").GetComponent<Image>().sprite = nomalNumSpriteTexture [number[0]];
		for (int i = 1; i < number.Count; i++) 
		{
			RectTransform scoreimage = (RectTransform)Instantiate (GameObject.Find ("ScoreImage")).transform;
			scoreimage.SetParent (this.transform, false);
            scoreimage.localPosition =new Vector2(
                scoreimage.localPosition.x - scoreimage.sizeDelta.x * i,
                scoreimage.localPosition.y);
            scoreimage.GetComponent<Image> ().sprite = nomalNumSpriteTexture [number [i]];
		}

	}

    //数字や演算子などをすべて消す
	void viewDestroy()
	{
		var objs = GameObject.FindGameObjectsWithTag ("Score");
		foreach (var obj in objs) 
		{
			if (0 <= obj.name.LastIndexOf ("Clone")) 
			{
				Destroy (obj);
			}
		}
        GameObject.Find("ScoreImage").GetComponent<Image>().sprite = nomalNumSpriteTexture[0];
    }
}