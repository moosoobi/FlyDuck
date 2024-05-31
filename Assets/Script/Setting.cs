using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{

    private float[] samples;
    private int sampleRate=44100;

    public AudioClip aud;
    public Slider slider;
    public Slider bar;
    public GameObject fill;
    public float rmsValue;
    public float modulate;
    public int maxValue;
    public int resultValue;
    public int cutValue=15;
    public bool inGame=false;

    void Start()
    {
        samples= new float[sampleRate];
        aud=Microphone.Start(Microphone.devices[0].ToString(),true,1,sampleRate);
        if(inGame){modulate = PlayerPrefs.GetFloat("myFloat");}
    }


    void Update()
    {
        aud.GetData(samples,0);
        float sum=0;
        for(int i=0;i<samples.Length;i++){
            sum+=samples[i]*samples[i];
        }
        rmsValue=Mathf.Sqrt(sum/samples.Length);
        rmsValue=rmsValue*modulate;
        rmsValue=Mathf.Clamp(rmsValue,0,100);
        resultValue=Mathf.RoundToInt(rmsValue);
        if(resultValue<cutValue){
            resultValue=0;
        }
        modulate=slider.value;
        GameObject.FindGameObjectWithTag("Player").GetComponent<JumpKing>().modulate=slider.value;
        if(resultValue==0){fill.SetActive(false);}
        else{fill.SetActive(true);}
        bar.value=resultValue;
    }
}
