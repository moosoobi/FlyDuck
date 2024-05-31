using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugIni : MonoBehaviour
{
    public GameObject bugPrefab;
    public Transform[] spawnTransform;
    public AudioSource BugSound;
    public float[] spawnTime;
    void Start()
    {
        InvokeRepeating("SpawnBug1", 0f, spawnTime[0]);
        InvokeRepeating("SpawnBug2", 0f, spawnTime[1]);
        InvokeRepeating("SpawnBug3", 0f, spawnTime[2]);
        InvokeRepeating("SpawnBug4", 0f, spawnTime[3]);
        InvokeRepeating("SpawnBug5", 0f, spawnTime[4]);
    }

    public void SpawnBug1(){
        BugSound.Play();
        GameObject bug1=Instantiate(bugPrefab, spawnTransform[0].position, spawnTransform[0].rotation);
        bug1.GetComponent<Rigidbody2D>().AddForce(transform.up *-1500);
        StartCoroutine(InvokeAfter(bug1));
    }
    public void SpawnBug2(){
        BugSound.Play();
        GameObject bug2=Instantiate(bugPrefab, spawnTransform[1].position, spawnTransform[1].rotation);
        bug2.GetComponent<Bug>().ifdown=false;
        Vector3 currentRotation = bug2.transform.rotation.eulerAngles;
        currentRotation.x = 0;
        bug2.transform.rotation = Quaternion.Euler(currentRotation);
        bug2.GetComponent<Rigidbody2D>().AddForce(transform.up *1500);
        StartCoroutine(InvokeAfter(bug2));
    }
    public void SpawnBug3(){
        BugSound.Play();
        GameObject bug3=Instantiate(bugPrefab, spawnTransform[2].position, spawnTransform[2].rotation);
        StartCoroutine(InvokeAfter(bug3));
        bug3.GetComponent<Rigidbody2D>().AddForce(transform.up *-1500);
    }
    public void SpawnBug4(){
        BugSound.Play();
        GameObject bug4=Instantiate(bugPrefab, spawnTransform[3].position, spawnTransform[3].rotation);
        bug4.GetComponent<Bug>().ifdown=false;
        Vector3 currentRotation = bug4.transform.rotation.eulerAngles;
        currentRotation.x = 0;
        bug4.transform.rotation = Quaternion.Euler(currentRotation);
        bug4.GetComponent<Rigidbody2D>().AddForce(transform.up *1500);
        StartCoroutine(InvokeAfter(bug4));
    }
    public void SpawnBug5(){
        BugSound.Play();
        GameObject bug5=Instantiate(bugPrefab, spawnTransform[4].position, spawnTransform[4].rotation);
        bug5.GetComponent<Rigidbody2D>().AddForce(transform.up *-1500);
        StartCoroutine(InvokeAfter(bug5));
    }
    private IEnumerator InvokeAfter(GameObject fishObj){
        yield return new WaitForSeconds(5f);
        Destroy(fishObj);
    }
}
