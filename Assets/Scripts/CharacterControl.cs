using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [Header("Control Values")]
    [SerializeField] private float _movementSpeed = 1.0f;
    [SerializeField] private float _jumpSpeed = 1.0f;
    
    [Header("Debug")]
    [SerializeField] private bool canJump = true;
    private Rigidbody _rigidbodyRef = null;
    
    
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        DetectInput();
    }

    private void Init()
    {
        _rigidbodyRef = gameObject.GetComponent<Rigidbody>();

        if (_rigidbodyRef == null)
        {
            Debug.LogError("[CharacterControl] Failed to find rigidbody reference on " + gameObject.name + "");
        }
    }

    private void DetectInput()
    {
        Vector3 currPosition = transform.position;
        
        //Move Right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            currPosition.x += _movementSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        //Move Left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
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
    }
    
    private void OnCollisionEnter(Collision other)
    {
        canJump = true;
    }
}
