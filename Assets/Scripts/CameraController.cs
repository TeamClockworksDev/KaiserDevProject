using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform CharacterGoTransformRef = null;
    [Space]
    public float followSpeed = 1.0f;
    public int followMargin = 10;
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

    void FixedUpdate()
    {
        if (CharacterGoTransformRef != null)
        {
            FollowCharacter();
        }
    }

    private void FollowCharacter()
    {
        Vector3 characterScreenPosition = _camera.WorldToScreenPoint(CharacterGoTransformRef.position);
        float newCamXPos = 0.0f;
        
        //TODO - implement the logic for endWallMargin; we want the camera to speed up as the character gets closer to the screen's edge.

        if (characterScreenPosition.x <= followMargin)
        {
            newCamXPos -= followSpeed * Time.deltaTime;
        }
        else if (characterScreenPosition.x >= (_screenSize.x - followMargin))
        {
            newCamXPos += followSpeed * Time.deltaTime;
        }

        transform.position = Vector3.Lerp (transform.position, new Vector3(transform.position.x + newCamXPos, transform.position.y, transform.position.z), 0.1f);
    }
}
