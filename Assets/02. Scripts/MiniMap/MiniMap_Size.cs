using UnityEngine;

public class MiniMap_Size : MonoBehaviour
{
    [Header("카메라")]
    public Camera miniMapCamera;

    [Header("설정")]
    public float zoomStep = 2f;
    public float minSize = 5f;
    public float maxSize = 25f;

    void Start()
    {
        if (miniMapCamera == null)
            miniMapCamera = GetComponent<Camera>();
    }

    public void ZoomIn()
    {
        if (miniMapCamera != null)
        {
            miniMapCamera.orthographicSize = Mathf.Max(minSize, miniMapCamera.orthographicSize - zoomStep);
        }
    }

    public void ZoomOut()
    {
        if (miniMapCamera != null)
        {
            miniMapCamera.orthographicSize = Mathf.Min(maxSize, miniMapCamera.orthographicSize + zoomStep);
        }
    }
}
