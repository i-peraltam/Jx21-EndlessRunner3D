using System.Collections.Generic;
using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
    [SerializeField] private GameObject trackSegmentPrefab;
    [SerializeField] private Transform player;

    [SerializeField] private float segmentLength = 30f;
    [SerializeField] private int segmentsAhead = 5;
    [SerializeField] private int maxSegments = 7;

    private float nextSpawnZ = 40f;

    private Queue<GameObject> activeSegments = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < segmentsAhead; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        if (player.position.z + (segmentsAhead * segmentLength) > nextSpawnZ)
        {
            SpawnSegment();
        }
    }

    private void SpawnSegment()
    {
        Vector3 position = new Vector3(
            0f,
            -0.5f,
            nextSpawnZ
        );

        GameObject newSegment = Instantiate(
            trackSegmentPrefab,
            position,
            Quaternion.identity
        );

        activeSegments.Enqueue(newSegment);

        nextSpawnZ += segmentLength;

        if (activeSegments.Count > maxSegments)
        {
            GameObject oldSegment = activeSegments.Dequeue();
            Destroy(oldSegment);
        }
    }
}