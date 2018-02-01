using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toast: MonoBehaviour {
    
	//// Use this for initialization
	//void Start () {

 //       AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
 //       AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
 //       AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");

 //       activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
 //           AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
 //           AndroidJavaObject toast = toastClass.CallStatic<AndroidJavaObject>("makeText",
 //                                                 context,
 //                                                 "最大桁数(8)を超えました。",
 //                                                 toastClass.GetStatic<int>("LENGTH_SHORT")
 //                                                 );
 //           toast.Call("show");
 //       }));
 //   }

    // Update is called once per frame
    void Update() {

    }

    public void ToastView()
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");

        activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            AndroidJavaObject toast = toastClass.CallStatic<AndroidJavaObject>("makeText",
                                                  context,
                                                  "最大桁数(8)を超えました。",
                                                  toastClass.GetStatic<int>("LENGTH_SHORT")
                                                  );
            toast.Call("show");
        }));
    }
}
