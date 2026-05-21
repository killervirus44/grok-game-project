using UnityEngine;
using UnityEngine.EventSystems;

public class GalaxyNode : MonoBehaviour, IPointerClickHandler
{
    public StarSystemData data;
    public LineRenderer connectionLinePrefab; // for connections

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(StarSystemData systemData)
    {
        data = systemData;
        transform.position = new Vector3(systemData.mapPosition.x, systemData.mapPosition.y, 0);
        name = systemData.systemName;
        // Set sprite if needed
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GalaxyMapManager.Instance.SelectSystem(this);
    }

    public void DrawConnections()
    {
        if (data.connectedSystems == null) return;
        foreach (var connected in data.connectedSystems)
        {
            // Find node by name or data and draw line (handled in manager)
        }
    }
}