using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugIni : MonoBehaviour
{
    public GameObject bugPrefab;
    public Transform[] spawnTransform;
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
        GameObject bug1=Instantiate(bugPrefab, spawnTransform[0].position, spawnTransform[0].rotation);
        StartCoroutine(InvokeAfter(bug1));
    }
    public void SpawnBug2(){
        GameObject bug2=Instantiate(bugPrefab, spawnTransform[1].position, spawnTransform[1].rotation);
        bug2.GetComponent<Bug>().ifdown=false;
        Vector3 currentRotation = bug2.transform.rotation.eulerAngles;
        currentRotation.x = 0;
        bug2.transform.rotation = Quaternion.Euler(currentRotation);
        StartCoroutine(InvokeAfter(bug2));
    }
    public void SpawnBug3(){
        GameObject bug3=Instantiate(bugPrefab, spawnTransform[2].position, spawnTransform[2].rotation);
        StartCoroutine(InvokeAfter(bug3));
    }
    public void SpawnBug4(){
        GameObject bug4=Instantiate(bugPrefab, spawnTransform[3].position, spawnTransform[3].rotation);
        bug4.GetComponent<Bug>().ifdown=false;
        Vector3 currentRotation = bug4.transform.rotation.eulerAngles;
        currentRotation.x = 0;
        bug4.transform.rotation = Quaternion.Euler(currentRotation);
        StartCoroutine(InvokeAfter(bug4));
    }
    public void SpawnBug5(){
        GameObject bug5=Instantiate(bugPrefab, spawnTransform[4].position, spawnTransform[4].rotation);
        StartCoroutine(InvokeAfter(bug5));
    }
    private IEnumerator InvokeAfter(GameObject fishObj){
        yield return new WaitForSeconds(5f);
        Destroy(fishObj);
    }
}
