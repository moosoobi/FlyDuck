using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public float rotateSpeed = 50f; // 회전 속도
    void Update()
    {

  
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // Z 회전이 0보다 크다면 Z를 0으로 조정합니다.
        if (currentRotation.z > 359)
        {
            // X와 Y는 원래 값으로 유지하고, Z만 0으로 설정합니다.
            transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 359);
        }
        if (currentRotation.z < 180)
        {
            // X와 Y는 원래 값으로 유지하고, Z만 0으로 설정합니다.
            transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 180);
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }

        // 오른쪽 방향키 입력 감지
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
        }
    }
}
