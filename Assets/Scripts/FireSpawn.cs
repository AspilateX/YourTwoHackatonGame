using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawn : MonoBehaviour
{
    // Скрипт должен висеть на родительском объекте для точек спавна огня
    private List<Transform> positionsList = new List<Transform>();
    [SerializeField] private GameObject firePrefab;
    public uint fireCount;
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            positionsList.Add(transform.GetChild(i));
        }

        GenerateFirePlaces();
    }

    private void GenerateFirePlaces()
    {
        for (int i = 0; i < fireCount; i++)
        {
            if (positionsList.Count > 0)
            {
                int rnd = Random.Range(0, positionsList.Count);
                Vector3 newFirePos = positionsList[rnd].position;

                Instantiate(firePrefab, newFirePos, Quaternion.identity, transform);

                positionsList.RemoveAt(rnd);
            }
            else
                break;
        }

    }
}
