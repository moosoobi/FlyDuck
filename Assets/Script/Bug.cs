using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    public float speed = 2.0f; // 이동 속도
    public float minX = -50f;  // 최소 x 값
    public float maxX = 50f;   // 최대 x 값

    private SpriteRenderer spriteRenderer;
    private float startX;
    private float preX;
    private float curX;
    void Start()
    {
        spriteRenderer=GetComponent<SpriteRenderer>(); 
        startX = transform.position.x;
    }

    void Update()
    {
        
        curX=Mathf.PingPong(Time.time * speed, maxX - minX);
        if(curX>preX){spriteRenderer.flipX = true;}
        else{spriteRenderer.flipX = false;}

        float x = Mathf.PingPong(Time.time * speed, maxX - minX) + minX;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        preX=Mathf.PingPong(Time.time * speed, maxX - minX);
    }
}
