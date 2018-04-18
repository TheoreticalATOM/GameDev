using System;
using System.Globalization;
using System.Threading;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog Stamper", menuName = "Dialog/Dialog Stamper", order = 0)]
public class DialogStamper : SerializedScriptableObject
{
    public AudioClip Clip;
    public string[] Dialog;

    private Thread mThread;
    
    [Button()]
    public void Check()
    {
        float[] samples = new float[Clip.samples * Clip.channels];
        Clip.GetData(samples, 0);
        
        Debug.Log("Thread Started! Analyzing: " + Clip.name + " (" + samples.Length + ")");
        mThread = new Thread(() =>
        {
            AnalyzeClip(samples);
        });
        mThread.Start();

//        for (int i = 0; i < samples.Length; i++)
//        {
//            
//        }


//        while (int i = 0; i < samples.Length) 
//        {
//            samples[i] = samples[i] * 0.5F;
//            ++i;
//        }
    }

    [Button()]
    public void ClearThreadAnalyzis()
    {
        if (mThread != null)
            mThread.Join();
    }
    


    private void AnalyzeClip(float[] samples)
    {
        float stamp = 0.0f;
        foreach (float sample in samples)
        {
            if (sample != 0.0f && stamp != 0.0f)
                stamp = DateTime.Now.Millisecond;
            else if (sample == 0.0f)
            {
                stamp = DateTime.Now.Millisecond - stamp;
                break;
            }
        }
        
        Debug.Log(stamp/100.0f);
    }
}
