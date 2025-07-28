using UnityEngine;

public class CamaraZoom : MonoBehaviour
{
    [Header("Zoom Settings")]
    public float zoomSpeed = 2f;
    public float minSize = 3f;
    public float maxSize = 20f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        if (cam == null)
        {
            Debug.LogError("CamaraZoom 스크립트는 Camera가 붙은 오브젝트에만 사용할 수 있습니다!");
        }
    }

    void Update()
    {
        if (cam == null) return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > 0.01f)
        {
            cam.orthographicSize -= scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minSize, maxSize);
        }
    }
}
