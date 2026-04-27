using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class DifficultyTier
    {
        public string name;
        public float activationDistance;
        public List<GameObject> prefabs;
    }

    [SerializeField] private Transform player;
    [SerializeField] private GameObject startSegment;
    [SerializeField] private List<DifficultyTier> tiers;
    [SerializeField] private float segmentWidth = 20f;
    [SerializeField] private float despawnDistance = 20f;

    private List<GameObject> segments = new();

    void Start()
{
    if (startSegment == null)
    {
        Debug.LogError("LevelManager: Start Segment nicht zugewiesen!");
        enabled = false;
        return;
    }

    if (tiers == null || tiers.Count == 0)
    {
        Debug.LogError("LevelManager: Keine Tiers konfiguriert!");
        enabled = false;
        return;
    }

    tiers.Sort((a, b) => a.activationDistance.CompareTo(b.activationDistance));

    float y = startSegment.transform.position.y;
    float startCenterX = startSegment.transform.position.x;

    segments.Add(startSegment);

    for (int i = 1; i < 3; i++)
    {
        GameObject prefab = PickPrefab();
        if (prefab == null) continue;

        GameObject seg = Instantiate(prefab,
            new Vector3(startCenterX + i * segmentWidth, y, 0f),
            Quaternion.identity);
        segments.Add(seg);
    }
}

void Update()
{
    if (segments.Count == 0) return;

    GameObject oldest = segments[0];
    if (oldest == null)
    {
        segments.RemoveAt(0);
        return;
    }

    float oldestRightEdge = oldest.transform.position.x + segmentWidth * 0.5f;

    if (oldestRightEdge < player.position.x - despawnDistance)
    {
        Destroy(oldest);
        segments.RemoveAt(0);

        if (segments.Count == 0) return;

        GameObject front = segments[segments.Count - 1];
        float newCenterX = front.transform.position.x + segmentWidth;
        float y = front.transform.position.y;

        GameObject prefab = PickPrefab();
        if (prefab == null) return;

        GameObject seg = Instantiate(prefab, new Vector3(newCenterX, y, 0f), Quaternion.identity);
        segments.Add(seg);
    }
}

GameObject PickPrefab()
{
    DifficultyTier currentTier = tiers[0];
    foreach (var tier in tiers)
    {
        if (player.position.x >= tier.activationDistance)
            currentTier = tier;
        else
            break;
    }

    if (currentTier.prefabs == null || currentTier.prefabs.Count == 0)
    {
        Debug.LogWarning($"Tier '{currentTier.name}' hat keine Prefabs!");
        return null;
    }

    return currentTier.prefabs[Random.Range(0, currentTier.prefabs.Count)];
}
}