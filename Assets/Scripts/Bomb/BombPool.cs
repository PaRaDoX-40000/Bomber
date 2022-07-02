using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BombPool : MonoBehaviour
{
    [SerializeField] private MapController _mapController;
    [SerializeField] private Transform _container;
    [SerializeField] private int _capacity;
    [SerializeField] private Bomb _bombPrefab;

    private List<GameObject> _pool = new List<GameObject>();
   
    private void Initialize(Bomb prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            Bomb spawned = Instantiate(prefab, _container);
            spawned.Initialize(_mapController);
            spawned.gameObject.SetActive(false);
            _pool.Add(spawned.gameObject);
        }
    }

    public bool TryGetObjectr(out GameObject result)
    {
        result = _pool.First(p => p.activeSelf == false);

        return result != null;
    }

    private void Start()
    {
        Initialize(_bombPrefab);
    }
}
