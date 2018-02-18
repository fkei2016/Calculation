using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class calculator : MonoBehaviour {

	[SerializeField]
	private List<string> num = new List<string>();    //計算用の数字

	[SerializeField]
	private List<char> operatorkey = new List<char> (); //演算子記憶用

	[SerializeField]
	private int calculateNums = 0;  //演算子の押された回数

	[SerializeField]
	private Sprite[] nomalNumSpriteTexture;　//表示用数字のテクスチャ

	[SerializeField]
	private Sprite[] anserNumSpriteTexture; //答え表示用テクスチャ

    private int maxNum = 8; //桁数の最大値

    private Toast toast = new Toast();　//8桁表示用

	private bool isfastNumKey = false; //１文字目を入力したかどうかのフラグ

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
	public void PushNumKey(char number)
	{

        //8桁用の制限処理
		if (num[calculateNums].Length >= maxNum)
        {
            //Toast.Create("最大桁数(8)を超えました");
#if UNITY_ANDROID && !UNITY_EDITOR
            toast.ToastView();
#endif

            return;
        }

		//1文字目を追加の場合
		if (!isfastNumKey) 
		{
			num [calculateNums] = number.ToString ();

			isfastNumKey = true;
		}

		//2文字目を追加の場合
		else
			num [calculateNums] += number;
        
        //前に描画されているものを消す
		viewDestroy ();

        //新しく描画しなおす
		view ();
	}

    /// <summary>
    ///特殊キーが押されたときの処理
    /// </summary>
    /// <param name="keyname"></param>
	public void PushOperatorKey(char keyname)
	{
		
        //ACキーが押されたら
        if (keyname == 'A')
        {
			num.Clear();
            calculateNums = 0;
            operatorkey.Clear();
            viewDestroy();
        }

        //Cキーが押されたら
        else if (keyname == 'C')
		{
			num.Clear();
            calculateNums = 0;
            operatorkey.Clear();
            viewDestroy();
        }

        //イコールが押されたら
        else if (keyname == '=')
        {
			Calculate ();
        }

        //四則演算子が押されたら
        else
        {
			calculateNums++;
			num.Add ("0");
			operatorkey.Add(keyname);
            viewDestroy();
            view();

        }

		//オペレータキーがおされたら１文字目のフラグを消去
		isfastNumKey=false;

	}
    
    //数値や演算子などの描画
	void view()
	{
        List<int> number = new List<int>();

        for (int i = calculateNums; i >= 0; i--) 
		{
			
			//数字の表示用数値を確保
			var numbers = num[i];
			//文字を分割
			for (int j = numbers.Length - 1 ; j >= 0; j--) 
			{
				//小数点があるかの確認
				if (numbers[j] == '.')
					number.Add (14);
				//普通の数字を入れる
				else
					number.Add (Int32.Parse(numbers[j].ToString()));
			}

			//演算子表示用
			if (operatorkey.Count > 0 && i > 0)
			{
				
				var a = operatorkey [i - 1];

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


	//計算開始
	void Calculate()
	{
		float anser = float.Parse (num [0]);

		for (int i = 0; i < calculateNums; i++) 
		{
			switch (operatorkey [i]) 
			{
			case '+':
				anser += float.Parse (num [i+1]);
				break;
			case '-':
				anser -= float.Parse (num [i+1]);
				break;
			case '*':
				anser *= float.Parse (num [i+1]);
				break;
			case '/':
				anser /= float.Parse (num [i+1]);
				break;
			}
		}

		AnserView (anser);

	}

	//答えの表示
	void AnserView(float anser)
	{
		List<int> number = new List<int>();

		//数字の表示用数値を確保
		var numbers = anser.ToString();
		//文字を分割
		for (int j = numbers.Length - 1 ; j >= 0; j--) 
		{
			//小数点があるかの確認
			if (numbers[j] == '.')
				number.Add (10);
			//普通の数字を入れる
			else
				number.Add (Int32.Parse(numbers[j].ToString()));
		}
		//イメージを生成して順番に表示
		GameObject.Find("AnserImage").GetComponent<Image>().sprite = anserNumSpriteTexture [number[0]];
		for (int i = 1; i < number.Count; i++) 
		{
			RectTransform scoreimage = (RectTransform)Instantiate (GameObject.Find ("AnserImage")).transform;
			scoreimage.SetParent (this.transform, false);
			scoreimage.localPosition =new Vector2(
				scoreimage.localPosition.x - scoreimage.sizeDelta.x * i,
				scoreimage.localPosition.y);
			scoreimage.GetComponent<Image> ().sprite = anserNumSpriteTexture [number [i]];
		}

	}


}