using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    public float speed = 2.0f; // 이동 속도
    public bool ifdown=true;




    void Update()
    {
        


        Vector3 curtrans=transform.position;
        if(ifdown){curtrans.y-=0.03f;}
        else{curtrans.y+=0.03f;}
        
        transform.position = curtrans;
        
    }
}
