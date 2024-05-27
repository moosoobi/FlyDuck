using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    
    public GameObject player;
    
    public int stage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if(stage==1){
            if (player.transform.position.y > -49f)
            {
                newPosition.y = -26f;
            }
            else
            {
                newPosition.y = -75f;
            } 
        }
        if(stage==2){
            if (player.transform.position.y > 30f)
            {
                newPosition.y = 60f;
            }
            else
            {
                newPosition.y = 0f;
            }        
        }
        if(stage==3){
            if (player.transform.position.x > 55f)
            {
                newPosition.x = 107f;
            }
            else
            {
                newPosition.x = 0f;
            }        
        }
        if(stage==4){
            if (player.transform.position.y > -32f)
            {
                newPosition.y = -7f;
            }
            else
            {
                newPosition.y = -64f;
            }        
        }
        if(stage==5){
            if (player.transform.position.y > -65f)
            {
                newPosition.y = -30f;
            }
            else
            {
                newPosition.y = -92f;
            }        
        }
        if(stage==6){
            if (player.transform.position.y > -45f)
            {
                newPosition.y = -12f;
            }
            else
            {
                newPosition.y = -72f;
            }        
        }
        
        transform.position = newPosition;
    }
}
