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
    
    
    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        DetectInput();
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
        }
        //Move Left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            isMoving = true;
            currPosition.x -= _movementSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
        
        //Apply Movement
        transform.position = currPosition;
        
        //Jump
        if (canJump)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
            {
                Vector3 currentVelocity = _rigidbodyRef.velocity;
                currentVelocity.y += _jumpSpeed;

                _rigidbodyRef.velocity = currentVelocity;
                canJump = false;
            }
        }
        
        //Toggle Movement Animation
        if (isMoving)
        {
            //_animatorRef.StopPlayback();
            _animatorRef.Play("DebugAnimation_CharacterMovement");
        }
        else
        {
            //_animatorRef.StartPlayback();
            _animatorRef.Play("DebugAnimation_CharacterIdle");
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        canJump = true;
    }

    private void OnCollisionExit(Collision other)
    {
        //In case we walk off a ledge
        canJump = true;
    }
}
