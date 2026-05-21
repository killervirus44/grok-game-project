using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GalaxyMapManager : MonoBehaviour
{
    public static GalaxyMapManager Instance;

    [Header("References")]
    public Transform nodesParent;
    public Camera mapCamera;

    [Header("Data")]
    public StarSystemData[] allSystems;

    private GalaxyNode selectedNode;
    private List<GalaxyNode> nodes = new List<GalaxyNode>();

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        GenerateMap();
    }

    private void GenerateMap()
    {
        foreach (var system in allSystems)
        {
            GameObject nodeGO = GameObject.CreatePrimitive(PrimitiveType.Quad); // or use prefab
            nodeGO.transform.SetParent(nodesParent);
            GalaxyNode node = nodeGO.AddComponent<GalaxyNode>();
            node.Initialize(system);
            nodes.Add(node);
        }
        DrawAllConnections();
    }

    private void DrawAllConnections()
    {
        // Implement line drawing between connected nodes
    }

    public void SelectSystem(GalaxyNode node)
    {
        selectedNode = node;
        // Show UI panel with info
        Debug.Log($"Selected: {node.data.systemName}");
    }

    public void TravelToSelected()
    {
        if (selectedNode != null && selectedNode.data.sectorScene != null)
        {
            SceneManager.LoadScene(selectedNode.data.sectorScene); // or use string
        }
    }
}