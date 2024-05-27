using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JumpKing : MonoBehaviour
{
    
    private float speakStartTime = 0f;
    private float speakDuration;
    private float[] samples;
    private int sampleRate=44100;
    private bool isSpeaking = false;
    private bool isWalking=false;
    private bool isHitting=false;
    private bool first=true;
    private bool second=true;
    private bool groundBool=true;
    private AudioSource audioSource;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 firstPosition;

    public AudioClip aud;
    public Animator Duck;
    public AudioSource Duck1;
    public Slider slider;
    public Slider settingSlider;
    public GameObject player;
    public GameObject Arrow;

    public bool IsGrounded=false;
    public bool hitbool=false;
    public float rmsValue;
    public float modulate;
    public int maxValue;
    public int resultValue;
    public int cutValue;
    public int speed;
    public int movingSpeed;
    public int fallInt;
    public int leftInt;
    public int rightInt;



    void Start()
    {
        firstPosition=transform.position;
        samples= new float[sampleRate];
        aud=Microphone.Start(Microphone.devices[0].ToString(),true,1,sampleRate);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        modulate = PlayerPrefs.GetFloat("myFloat");
    }       

    void Update()
    {
        //if(settingSlider.gameObject.activeSelf){modulate=settingSlider.value;Debug.Log(1);}
        
        //공간 제한
        if(groundBool){
            RayCastGrounded();
        }
    
        Vector2 newPosition = rb.velocity;
        if (player.transform.position.x > rightInt)
        {
            newPosition.x = 0;
        }
        if (player.transform.position.x < leftInt)
        {
            newPosition.x = 0;
        }

        rb.velocity = newPosition;
        
        if(hitbool){
            hitbool=false;
            isHitting=true;
            StartCoroutine(hitting());
        }
        //이동
        
        if(IsGrounded&&!isSpeaking&&!isHitting){
            if (Input.GetKey(KeyCode.A))
            {
                if(isWalking==false){
                    isWalking=true;
                    Duck.Play("Walk", 0, 0.0f);
                }
                
                first=true;
                rb.velocity = new Vector2(-movingSpeed, rb.velocity.y);
                spriteRenderer.flipX = false;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if(isWalking==false){
                    isWalking=true;
                    Duck.Play("Walk", 0, 0.0f);
                }
                first=true;
                rb.velocity = new Vector2(movingSpeed, rb.velocity.y);
                spriteRenderer.flipX = true;
            }
            else
            {
                isWalking=false;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }else if(first&&!isHitting){
            first=false;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        

        //음성입력
        aud.GetData(samples,0);
        float sum=0;
        for(int i=0;i<samples.Length;i++){
            sum+=samples[i]*samples[i];
        }
        rmsValue=Mathf.Sqrt(sum/samples.Length);
        rmsValue=rmsValue*modulate;
        rmsValue=Mathf.Clamp(rmsValue,0,100);
        resultValue=Mathf.RoundToInt(rmsValue);
        if(resultValue<cutValue){
            resultValue=0;
        }

        //점프구현
        if(IsGrounded&&resultValue!=0){
            groundBool=false;
            IsGrounded=false;
            isSpeaking = true;
            maxValue=resultValue;
            speakStartTime = Time.time;
            
        }
        if(isSpeaking){
            if(maxValue<resultValue){maxValue=resultValue;}
            speakDuration=(Time.time-speakStartTime)/2f;
            
            slider.value = maxValue;
            Duck.Play("Fly", 0, 0.0f);
        }
        
        if (speakDuration>1.0f)
        {
            Vector3 currentRotation = Arrow.transform.rotation.eulerAngles;
            if(currentRotation.z>270&&currentRotation.z<360){spriteRenderer.flipX = false;}
            if(currentRotation.z>180&&currentRotation.z<270){spriteRenderer.flipX = true;}
            resultValue=0;
            speakDuration=0;
            isSpeaking = false;
            Duck1.Play();
            Duck.Play("Fly", 0, 0.0f);
            GetComponent<Rigidbody2D>().AddForce(Arrow.transform.right *speed*maxValue*-1);
            slider.value = 0;
            StartCoroutine(groundBool1());
        }
        if(IsGrounded&&!isWalking){Duck.Play("Idle", 0, 0.0f);}
        if(transform.position.y<fallInt&&second){
            second=false;
            BackFirst();
        }
        
    }
    public void BackFirst(){
        transform.position=firstPosition;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        second=true;
    }
    //땅 검사
    public void RayCastGrounded(){
        Vector2 currentPosition = transform.position;

        Vector2 rightPosition = new Vector2(currentPosition.x + 2f, currentPosition.y);

        Vector2 leftPosition = new Vector2(currentPosition.x - 2f, currentPosition.y);

        RaycastHit2D groundedInfo1 = Physics2D.Raycast(rightPosition, Vector2.down, 5f);
        RaycastHit2D groundedInfo2 = Physics2D.Raycast(leftPosition, Vector2.down, 5f);
        
        if(groundedInfo1.collider != null||groundedInfo2.collider != null){
            IsGrounded=true;
        }
        else{
            IsGrounded=false;
        }
    }
    private IEnumerator groundBool1(){
        yield return new WaitForSeconds(1f);
        groundBool=true;
    }

    public IEnumerator hitting(){
        isHitting=true;
        GetComponent<SpriteRenderer>().color=new Color32(255,60,60,255);
        yield return new WaitForSeconds(1.0f);
        GetComponent<SpriteRenderer>().color=new Color32(255,255,255,255);
        isHitting=false;
    }

    public bool ifflipX(){
        return spriteRenderer.flipX;
    }
}
