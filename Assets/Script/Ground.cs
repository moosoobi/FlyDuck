using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name=="Player"){
            //GameObject.FindGameObjectWithTag("Player").GetComponent<JumpKing>().IsGrunded=true;
        }
        
    }
}
