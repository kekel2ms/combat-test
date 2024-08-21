using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;

    [SerializeField]
    private Transform[] _spawnPoints;

    private List<GameObject> _spawns = new List<GameObject>();

    private void Update()
    {
        ClearAllDiedSpawn();

        if (_spawns.Count == 0)
        {
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                _spawns.Add(Instantiate(_enemy, _spawnPoints[i].position, _spawnPoints[i].rotation));
            }
        }
    }

    private void ClearAllDiedSpawn()
    {
        if (_spawns.Count == 0 )
        {
            return;
        }

        for (int i = _spawns.Count - 1; i >= 0; i--)
        {
            if (_spawns[i] == null)
            {
                _spawns.RemoveAt(i);
            }
        }
    }
}
