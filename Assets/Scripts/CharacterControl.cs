using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [Header("Control Values")]
    [SerializeField] private float _movementSpeed = 1.0f;
    [SerializeField] private float _jumpSpeed = 1.0f;
    
    [Header("Debug")]
    [SerializeField] private bool canJump  = true ;
    [SerializeField] private bool isMoving = false;
    
    private Rigidbody _rigidbodyRef = null;
    private Animator _animatorRef = null;
    private float dashtimer = 0;
    public float dashtime = 0;
    public bool leftright = true;
    public float dashspeed = 5;
    Vector3 DashPause = Vector3.zero;
    private bool inAir = false;
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        DetectInput();
        DashPause = transform.position;
        if(dashtimer >0)
        {  if (canJump == true) 
            dashtimer -=Time.deltaTime;
            
            if (leftright == true)
            {  DashPause.x += dashspeed*Time.deltaTime;
              transform.localRotation = Quaternion.Euler(0, 90, 0);  
            }  
            else { DashPause.x -= dashspeed*Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, -90, 0);
                

            }   
            transform.position = DashPause;
        }
    }

    private void Init()
    {
        _rigidbodyRef = gameObject.GetComponent<Rigidbody>();
        _animatorRef  = gameObject.GetComponentInChildren<Animator>();

        if (_rigidbodyRef == null) { Debug.LogError("[CharacterControl] Failed to find rigidbody reference for " + gameObject.name + ""); }
        if (_animatorRef  == null) { Debug.LogError("[CharacterControl] Failed to find animator reference for "  + gameObject.name + ""); }
    }

    private void DetectInput()
    {
        isMoving = false;
        Vector3 currPosition = transform.position;
        
        //Move Right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            isMoving = true;
            currPosition.x += _movementSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, 90, 0);
            //Dash Right
            leftright = true;
        }
        //Move Left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            isMoving = true;
            currPosition.x -= _movementSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, -90, 0);
            //Dash Left
            leftright = false;
        }
            //Dash
        if (Input.GetKeyDown(KeyCode.K)){
                dashtimer = dashtime;
               _animatorRef.SetTrigger("dashtrigger");
        }
        //Apply Movement
        transform.position = currPosition;

        //Jump
        if (canJump)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Vector3 currentVelocity = _rigidbodyRef.velocity;
                currentVelocity.y += _jumpSpeed;

                _rigidbodyRef.velocity = currentVelocity;
                canJump = false;
                if (inAir == false)
                _animatorRef.SetTrigger("jumptrigger");

                inAir = true;
            }
        }
        
        //Toggle Movement Animation
        if (isMoving)
        {
            //_animatorRef.StopPlayback();
            //_animatorRef.Play("DebugAnimation_CharacterMovement");
        }
        else
        {
            //_animatorRef.StartPlayback();
            //_animatorRef.Play("DebugAnimation_CharacterIdle");
        }

        _animatorRef.SetBool ("IsMoving",isMoving);
        _animatorRef.SetBool ("canJump",canJump);
        _animatorRef.SetBool ("inAir",inAir);
        _animatorRef.SetFloat ("dashtimer",dashtimer);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        canJump = true;
        inAir = false;
    }

    private void OnCollisionExit(Collision other)
    {
        //In case we walk off a ledge
        //canJump = true;
    }
}
