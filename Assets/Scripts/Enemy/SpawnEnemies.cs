using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private MapController _mapController;
    [SerializeField] private Enemy _enemiePrafab;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform[] _spawnPoints;
       

    private void Start()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            Enemy spawned = Instantiate(_enemiePrafab, _container);
            spawned.Initialize(_mapController, _player);
            spawned.transform.localPosition = _spawnPoints[i].localPosition;
            spawned.gameObject.SetActive(true);
        }
    }
}
