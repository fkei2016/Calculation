using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// BGMとSEの管理をするマネージャ。シングルトン。
/// </summary>
public class AudioManager : SingletonMonoBehaviour<AudioManager>

{
    private const string SE_VOLUME_KEY = "SE_VOLUME_KEY";

    private const float SE_VOLUME_DEFULT = 1.0f;

    //次流すBGM名、SE名
    private string _nextSEName;

    //BGM用、SE用に分けてオーディオソースを持つ
    public AudioSource AttachSESource;

    //全Audioを保持
    private Dictionary<string, AudioClip> _seDic;
    
    private void Awake()

    {

        if (this != Instance)
        {

            Destroy(this);

            return;

        }
        DontDestroyOnLoad(this.gameObject);

        //リソースフォルダから全SEのファイルを読み込みセット
        _seDic = new Dictionary<string, AudioClip>();

        object[] seList = Resources.LoadAll("SE");

        foreach (AudioClip se in seList)
        {

            _seDic[se.name] = se;

        }

    }

    private void Start()
    {
        AttachSESource.volume = PlayerPrefs.GetFloat(SE_VOLUME_KEY, SE_VOLUME_DEFULT);

    }

    /// <summary>
    /// 指定したファイル名のSEを流す。第二引数のdelayに指定した時間だけ再生までの間隔を空ける
    /// </summary>
    public void PlaySE(string seName, float delay = 0.0f)

    {

        if (!_seDic.ContainsKey(seName))
        {

            Debug.Log(seName + "という名前のSEがありません");

            return;

        }

        _nextSEName = seName;

        Invoke("DelayPlaySE", delay);

    }

    private void DelayPlaySE()
    {
        AttachSESource.PlayOneShot(_seDic[_nextSEName] as AudioClip);
    }

    /// <summary>
    /// SEのボリュームを変更&保存
    /// </summary>
    public void ChangeVolume(float SEVolume)
    {
        AttachSESource.volume = SEVolume;

        PlayerPrefs.SetFloat(SE_VOLUME_KEY, SEVolume);
    }
}