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
    private int sensitive=10;
    private bool isSpeaking = false;
    private bool isWalking=false;
    private bool first=true;
    private bool groundBool=true;
    private bool pressed=false;
    private AudioSource audioSource;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public AudioClip aud;
    public Animator Duck;
    public AudioSource Duck1;
    public Slider slider;
    public Slider settingSlider;
    public GameObject player;
    public GameObject Arrow;
    public TextMeshProUGUI Mouse;

    public bool IsGrounded=false;
    public float rmsValue;
    public float modulate;
    public int maxValue;
    public int resultValue;
    public int cutValue;
    public int speed;
    public int movingSpeed;
    public int stage;
    



    void Start()
    {
        samples= new float[sampleRate];
        aud=Microphone.Start(Microphone.devices[0].ToString(),true,1,sampleRate);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        modulate = PlayerPrefs.GetFloat("myFloat");
    }       

    void Update()
    {
        //if(settingSlider.gameObject.activeSelf){modulate=settingSlider.value;Debug.Log(1);}
        
        /*
        //감도조절
        if ((Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadPlus)) && !pressed) // "+" 키를 누르고 있는지 확인
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            sensitive++;
            modulate+=100

            Mouse.text = "마우스감도:" + sensitive.ToString();
            Mouse.gameObject.SetActive(true);
            currentCoroutine = StartCoroutine(MouseText(2.0f));
            pressed = true;
        }
        else if ((Input.GetKeyUp(KeyCode.Equals) || Input.GetKeyUp(KeyCode.KeypadPlus)) && pressed) // "+" 키를 뗐는지 확인
        {
            pressed = false;
        }

        if ((Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus)) && pressed) // "-" 키를 누르고 있는지 확인
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
           
            Mouse.text = "마우스감도:" + sensitive.ToString();
            Mouse.gameObject.SetActive(true);
            currentCoroutine = StartCoroutine(MouseText(2.0f));
            pressed = false;
            
        }
        else if ((Input.GetKeyUp(KeyCode.Minus) || Input.GetKeyUp(KeyCode.KeypadMinus)) && !pressed) // "-" 키를 뗐는지 확인
        {
            pressed = true;
        }
        */
        //공간 제한
        if(groundBool){
            RayCastGrounded();
        }
        if(stage==1){
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            if (player.transform.position.x > 270f)
            {
                newPosition.x = 270f;
            }
            if (player.transform.position.x < 170f)
            {
                newPosition.x = 170f;
            }

            transform.position = newPosition;
        }if(stage==2){
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            if (player.transform.position.x > 50f)
            {
                newPosition.x = 50f;
            }
            if (player.transform.position.x < -50f)
            {
                newPosition.x = -50f;
            }

            transform.position = newPosition;
        }

        //이동
        if(IsGrounded&&!isSpeaking){
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
        }else if(first){
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
    }

    //땅 검사
    public void RayCastGrounded(){
        Vector2 currentPosition = transform.position;

        Vector2 rightPosition = new Vector2(currentPosition.x + 3f, currentPosition.y);

        Vector2 leftPosition = new Vector2(currentPosition.x - 3f, currentPosition.y);

        RaycastHit2D groundedInfo1 = Physics2D.Raycast(rightPosition, Vector2.down, 3f);
        RaycastHit2D groundedInfo2 = Physics2D.Raycast(leftPosition, Vector2.down, 3f);

        Debug.DrawRay(rightPosition, Vector2.down * 1f, Color.red, 3f);
        Debug.DrawRay(leftPosition, Vector2.down * 1f, Color.red, 3f);
        
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


    private IEnumerator MouseText(float delayInSeconds){
	    yield return new WaitForSeconds(delayInSeconds);
	    Mouse.gameObject.SetActive(false);
    }
}
