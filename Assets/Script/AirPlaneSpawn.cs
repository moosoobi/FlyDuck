using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlaneSpawn : MonoBehaviour
{
    public GameObject airPrefab;
    public Transform[] spawnTransform;
    public AudioSource AirSound;
    void Start()
    {
        InvokeRepeating("SpawnAir", 0f, 5.0f);
    }
    public void SpawnAir(){
        AirSound.Play();
        int i=Random.Range(0, 4);
        GameObject air1=Instantiate(airPrefab, spawnTransform[i].position, spawnTransform[i].rotation);
        if(i==2||i==0){air1.GetComponent<SpriteRenderer>().flipY=true;}
        air1.GetComponent<Rigidbody2D>().AddForce(spawnTransform[i].right *3000);
        StartCoroutine(InvokeAfter(air1));
    }
    
    private IEnumerator InvokeAfter(GameObject airObj){
        yield return new WaitForSeconds(8f);
        Destroy(airObj);
    }
}
