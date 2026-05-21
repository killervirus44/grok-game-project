using UnityEngine;

public class GalaxyCameraController : MonoBehaviour
{
    private Camera cam;
    private Vector3 dragOrigin;

    [Header("Settings")]
    public float panSpeed = 20f;
    public float zoomSpeed = 2f;
    public float minZoom = 5f;
    public float maxZoom = 30f;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        // Pan with middle mouse or drag
        if (Input.GetMouseButtonDown(0)) // or 2 for middle
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += difference;
        }

        // Zoom with scroll
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scroll * zoomSpeed, minZoom, maxZoom);
        }
    }
}