using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    

    public GameObject fishPrefab;
    public Transform[] spawnTransform;
    public AudioSource FishSound;
    public float[] spawnTime;
  


    void Start()
    {
        InvokeRepeating("SpawnFish1", 0f, spawnTime[0]);
        InvokeRepeating("SpawnFish2", 0f, spawnTime[1]);
        InvokeRepeating("SpawnFish3", 0f, spawnTime[2]);
        InvokeRepeating("SpawnFish4", 0f, spawnTime[3]);
    }

    public void SpawnFish1(){
        FishSound.Play();
        GameObject fish1=Instantiate(fishPrefab, spawnTransform[0].position, spawnTransform[0].rotation);
        fish1.GetComponent<Rigidbody2D>().AddForce(spawnTransform[0].right *1800);
        StartCoroutine(InvokeAfter(fish1));
    }
    public void SpawnFish2(){
        FishSound.Play();
        GameObject fish2=Instantiate(fishPrefab, spawnTransform[1].position, spawnTransform[1].rotation);
        fish2.GetComponent<Rigidbody2D>().AddForce(spawnTransform[1].right *1000);
        StartCoroutine(InvokeAfter(fish2));
    }
    public void SpawnFish3(){
        FishSound.Play();
        GameObject fish3=Instantiate(fishPrefab, spawnTransform[2].position, spawnTransform[2].rotation);
        fish3.GetComponent<Rigidbody2D>().AddForce(spawnTransform[2].right *3000);
        StartCoroutine(InvokeAfter(fish3));
    }
    public void SpawnFish4(){
        FishSound.Play();
        GameObject fish4=Instantiate(fishPrefab, spawnTransform[3].position, spawnTransform[3].rotation);
        fish4.GetComponent<Rigidbody2D>().AddForce(spawnTransform[3].right *1000);
        StartCoroutine(InvokeAfter(fish4));
    }
    private IEnumerator InvokeAfter(GameObject fishObj){
        yield return new WaitForSeconds(5f);
        Destroy(fishObj);
    }

}
