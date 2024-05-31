using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawn : MonoBehaviour
{
    public GameObject MeteorPrefab;
    public GameObject UfoPrefab;
    public Transform[] spawnTransform;
    public Transform ufoTransform;
    void Start()
    {
        InvokeRepeating("SpawnMeteor", 0f, 8.0f);
        InvokeRepeating("SpawnUfo", 0f, 15.0f);
    }
    public void SpawnMeteor(){
        int i=Random.Range(0, 3);
        GameObject Meteor1=Instantiate(MeteorPrefab, spawnTransform[i].position, spawnTransform[i].rotation);
        //if(i==2||i==0){air1.GetComponent<SpriteRenderer>().flipY=true;}
        Meteor1.GetComponent<Rigidbody2D>().AddForce((spawnTransform[i].right*-1+spawnTransform[i].up*-1) *3000);
        StartCoroutine(InvokeAfter(Meteor1));
    }
    public void SpawnUfo(){
        GameObject Ufo1=Instantiate(UfoPrefab, ufoTransform.position, ufoTransform.rotation);
        StartCoroutine(InvokeAfter(Ufo1));
    }
    
    private IEnumerator InvokeAfter(GameObject Obj){
        yield return new WaitForSeconds(7.5f);
        Destroy(Obj);
    }
}
