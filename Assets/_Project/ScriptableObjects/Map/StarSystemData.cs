using UnityEngine;

[CreateAssetMenu(fileName = "NewStarSystem", menuName = "Coreforge/Star System Data")]
public class StarSystemData : ScriptableObject
{
    public string systemName = "New System";
    public string description = "A distant star system";
    [Range(1,10)] public int difficulty = 3;
    public Vector2 mapPosition = Vector2.zero;
    public SceneReference sectorScene; // Use SceneReference from Unity or string path
    public Sprite icon;
    public StarSystemData[] connectedSystems;

    [Header("Resources")]
    public int ferriteAmount = 50;
    public int energyCellAmount = 30;
    // Add more as needed
}