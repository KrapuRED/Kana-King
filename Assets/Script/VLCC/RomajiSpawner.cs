using System.Collections.Generic;
using UnityEngine;

public class RomajiSpawner : MonoBehaviour
{

    [SerializeField] private int childCount;

    private void Start()
    {
        childCount = transform.childCount;
    }
    public int RandomizeRomajiSpawn()
    {
        List<int> emptyIndexes = new List<int>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount == 0) // atau cek active
            {
                emptyIndexes.Add(i);
            }
        }

        if (emptyIndexes.Count == 0) return -1;

        return emptyIndexes[Random.Range(0, emptyIndexes.Count)];
    }
}
