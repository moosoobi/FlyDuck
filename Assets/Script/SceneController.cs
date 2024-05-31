using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Slider slider;
    public static float myFloatValue;

    public GameObject Mini;
    public GameObject MiniDetail;
    public GameObject player;
    public GameObject setting;
    public AudioSource Portal;
    public string nextScene;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MiniOn(){
        Mini.SetActive(true);
    }
    
    public void MiniOff(){
        Mini.SetActive(false);
    }
    public void MiniDetailOn(){
        MiniOff();
        slider.value=GameObject.FindGameObjectWithTag("Player").GetComponent<JumpKing>().modulate;
        setting.GetComponent<Setting>().aud=Microphone.Start(Microphone.devices[0].ToString(),true,1,44100);
        MiniDetail.SetActive(true);
    }
    public void MiniDetailOff(){
        MiniDetail.SetActive(false);
        player.GetComponent<JumpKing>().aud=Microphone.Start(Microphone.devices[0].ToString(),true,1,44100);
    }

    public void Load1(){
        SceneManager.LoadScene("Setting");
    }
    public void Load2(){
        myFloatValue=slider.value;
        PlayerPrefs.SetFloat("myFloat", myFloatValue);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Farm");
    }
    public void Quit(){
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name=="Player"){
            Portal.Play();
            myFloatValue=slider.value;
            PlayerPrefs.SetFloat("myFloat", myFloatValue);
            PlayerPrefs.Save();
            SceneManager.LoadScene(nextScene);
        }
    }

}
