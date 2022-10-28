using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform CharacterGoTransformRef = null;
    [Space]
    public float followSpeed = 1.0f;
    public float followMargin = 10;
    public int endWallMargin = 10;
    public float lerpSpeed = 0.1f;

    private Camera _camera;
    private Vector2 _screenSize;

    
    void Start()
    {
        Init();
    }

    private void Init()
    {
        _camera = gameObject.GetComponent<Camera>();
        _screenSize = new Vector2(x:Screen.width, y:Screen.height);

        if (_camera == null) { Debug.LogError("[CameraController] Failed to find camera reference for " + gameObject.name + ""); }

        if (endWallMargin >= followMargin)
        {
            Debug.LogWarning("[CameraController] End Wall Margin is greater than followMargin. Camera will never follow character.");
        }
    }

    void Update()
    {
        if (CharacterGoTransformRef != null)
        {
            FollowCharacter();
        }
    }

    private void FollowCharacter()
    {
        //Vector3 characterScreenPosition = _camera.WorldToScreenPoint(CharacterGoTransformRef.position);
        //float newCamXPos = 0.0f;
        transform.position = Vector3.Lerp (transform.position,CharacterGoTransformRef.transform.position,5*Time.deltaTime);
        //TODO - implement the logic for endWallMargin; we want the camera to speed up as the character gets closer to the screen's edge.
        //followMargin = new Vector3(Screen.width*0.5f, Screen.height*0.5f, 0.0f).x;
        //if (characterScreenPosition.x < followMargin-2)
        //{
            //newCamXPos -= followSpeed * Time.deltaTime;
        //}
        //else if (characterScreenPosition.x > (_screenSize.x - followMargin)+2)
        //{
            //newCamXPos += followSpeed * Time.deltaTime;
        //}

        //transform.position = Vector3.Lerp (transform.position, new Vector3(transform.position.x + newCamXPos, transform.position.y, transform.position.z), 5f*Time.deltaTime);
    }
}
