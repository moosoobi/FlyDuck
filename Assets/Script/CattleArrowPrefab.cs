using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CattleArrowPrefab : MonoBehaviour
{
    public float pushForce = 10f;
    private bool first=true;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&&first)
        {
            first=false;
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            playerRigidbody.velocity = new Vector2(0,0);
            if (playerRigidbody != null)
            {
                Vector2 pushDirection;
                if(other.GetComponent<JumpKing>().ifflipX()){
                    pushDirection = new Vector2(-1, 1).normalized; 
                }else{
                    pushDirection = new Vector2(1, 1).normalized; 
                }
                other.GetComponent<JumpKing>().hitbool=true;
                playerRigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }
            first=true;
        }
    }
}
