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
        transform.position = Vector3.Lerp (transform.position,CharacterGoTransformRef.transform.position,5*Time.deltaTime);
    }
}
