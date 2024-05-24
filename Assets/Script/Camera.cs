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
        if(stage==1){
             Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            if (player.transform.position.y > -49f)
            {
                newPosition.y = -26f;
            }
            else
            {
                newPosition.y = -75f;
            }

            transform.position = newPosition;
        }if(stage==2){
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            if (player.transform.position.y > 30f)
            {
                newPosition.y = 60f;
            }
            else
            {
                newPosition.y = 0f;
            }

            transform.position = newPosition;
        }
    }
}
