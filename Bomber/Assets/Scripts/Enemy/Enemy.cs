using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Entity
{
    [SerializeField] private int _respavnTine = 5;
    [SerializeField] private GameObject _model;

    private EnemyMovementController _enemyMovementController;
    private CharacterMover _enemyMover;
    private EnemyAttack _enemyAttack;

    private Vector3 _spasn;
    public UnityAction respawnEvent;

    public void Initialize(MapController mapController, Player player)
    {
        _enemyMovementController = GetComponent<EnemyMovementController>();
        _enemyMovementController.Initialize(mapController, player);

        _enemyMover = GetComponent<CharacterMover>();
        _enemyMover.Initialize(mapController);

        _enemyAttack = GetComponent<EnemyAttack>();
        _enemyAttack.Initialize(player);

    }



    private void Start()
    {
        _spasn = transform.localPosition;
    }

    public override void Destroy()
    {
        StartCoroutine(Respawn());       
        _model.SetActive(false);
        DiedEvent?.Invoke();
        _enemyMovementController.enabled = false;
        _enemyMover.enabled = false;
        _enemyAttack.enabled = false;


    }
    private IEnumerator Respawn()
    {      
        yield return new WaitForSeconds(_respavnTine);       
        transform.localPosition = _spasn;
        respawnEvent?.Invoke();
        _enemyMovementController.enabled = true;
        _enemyMover.enabled = true;
        _enemyAttack.enabled = true;
        _model.SetActive(true);

    }


}
