using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CattleArrow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform[] spawnTransform;
    public AudioSource arrowsound;
    void Start()
    {
        InvokeRepeating("SpawnArrow", 0f, 5.0f);
    }
    public void SpawnArrow(){
        arrowsound.Play();
        int i=Random.Range(0, 6);
        GameObject arrow1=Instantiate(arrowPrefab, spawnTransform[i].position, spawnTransform[i].rotation);
        StartCoroutine(InvokeAfter(arrow1));
    }
    
    private IEnumerator InvokeAfter(GameObject arrowObj){
        yield return new WaitForSeconds(8f);
        Destroy(arrowObj);
    }
}
