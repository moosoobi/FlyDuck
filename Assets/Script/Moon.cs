using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    
    public Transform player; // 플레이어의 Transform
    public float maxDistance = 5.0f; // 거리가 5.0f 이내일 때만 크기 변화
    public float minScale = 1.0f; // 최소 크기
    public float maxScale = 2.0f; // 최대 크기
    public GameObject ending;

    private Vector3 initialScale; // 오브젝트의 초기 크기

    void Start()
    {
        initialScale = transform.localScale;
    }

    
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= maxDistance)
        {
            // 거리 비율 계산 (0.0f ~ 1.0f)
            float scaleRatio = 1.0f - (distance / maxDistance);
            // 크기 조절
            float newScale = Mathf.Lerp(minScale, maxScale, scaleRatio);
            transform.localScale = initialScale * newScale;
        }
        else
        {
            // 거리가 멀어지면 초기 크기로 복원
            transform.localScale = initialScale * minScale;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Ending1();
        }
    }
    public void Ending1(){
        ending.SetActive(true);
    }
}
