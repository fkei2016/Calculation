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

    private Toast toast = new Toast();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PushNumKey(int number)
	{

        if (num[calculateNums] >= 10000000)
        {
            //Toast.Create("最大桁数(8)を超えました");
#if UNITY_ANDROID && !UNITY_EDITOR
            toast.ToastView();
#endif

            return;
        }

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

		viewDestroy ();
		view ();


	}

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
	}

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