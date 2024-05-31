using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float pushForce = 10f; // 플레이어를 밀어내는 힘의 크기
    private bool first=true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&&first)
        {
            first=false;
            other.GetComponent<JumpKing>().BackFirst();
            
            first=true;
        }
    }
}
