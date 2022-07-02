using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] private MapController _mapController;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerInput _playerInput;

    private CharacterMover _enemyMover;
    private Enemy _enemy;
    private bool _playerFound=false;
    private Vector2[,] _wayMap;
    private List<Vector2> _way = new List<Vector2>();
    private List<Vector2> _cellsToWatch = new List<Vector2>();
    private Vector2 _enemyPosition;
    private bool _dead = false;




    public void Initialize(MapController mapController,  Player player)
    {
        _mapController = mapController;
        _playerInput = player.gameObject.GetComponent<PlayerInput>();
        _player = player;
        _wayMap = new Vector2[_mapController.Map.GetLength(0), _mapController.Map.GetLength(1)];       
        _enemyMover = GetComponent<CharacterMover>();
        _enemy= GetComponent<Enemy>();
        ClearEnemy();
    }


    private void OnEnable()
    {
        _playerInput.PlayerHasShiftedEvent += PlayerHasShifted;
        _enemy.DiedEvent += ClearEnemy;
    }

    private void OnDisable()
    {
        _playerInput.PlayerHasShiftedEvent -= PlayerHasShifted;
        _enemy.DiedEvent -= ClearEnemy;    
    }

    private void PlayerHasShifted()
    {
        _playerFound = false;
    }

    private void ClearEnemy()
    {
        ClearMap();
        _cellsToWatch.Clear();      
        _playerFound = false;
    }
   


    private void Update()
    {
        if (_dead == false)
        {
            if ((_way.Count == 0 || _playerFound == false) && _enemyMover.InMotion == false)
            {
                FindWay();
            }
            else
            {

                _enemyMover.TryMove(new Vector3(_way[_way.Count - 1].x, 0, _way[_way.Count - 1].y) - transform.localPosition);
                if (_way[_way.Count - 1] == new Vector2(transform.localPosition.x, transform.localPosition.z))
                {
                    _way.Remove(_way[_way.Count - 1]);
                }
            }
        }
       
    }
    private void FindWay()
    {
        
        _enemyPosition = new Vector2(transform.localPosition.x, transform.localPosition.z);

        _wayMap[(int)_enemyPosition.x, (int)_enemyPosition.y] = _enemyPosition;

        _cellsToWatch.Add(_enemyPosition);       

        while(_playerFound==false && _cellsToWatch.Count!=0)
        {
            
            CheckAdjacentCell(_cellsToWatch[0], new Vector2(1, 0));
            CheckAdjacentCell(_cellsToWatch[0], new Vector2(-1, 0));
            CheckAdjacentCell(_cellsToWatch[0], new Vector2(0, 1));
            CheckAdjacentCell(_cellsToWatch[0], new Vector2(0, -1));
            _cellsToWatch.Remove(_cellsToWatch[0]);
        }
        _cellsToWatch.Clear();
        ClearMap();

    }

    private void CheckAdjacentCell(Vector2 targetPosition,Vector2 ofset)
    {
        Vector2 targetCellCoordinates = targetPosition + ofset;

       
        if (_mapController.CanWalkCell(new Vector3(targetCellCoordinates.x,0, targetCellCoordinates.y),out Cell targetCell))
        {
            
            if (_wayMap[(int)targetCellCoordinates.x, (int)targetCellCoordinates.y] == new Vector2(-1, -1))
            {
                _wayMap[(int)targetCellCoordinates.x, (int)targetCellCoordinates.y] = targetPosition;
                _cellsToWatch.Add(targetCellCoordinates);
               
                if (targetCell.GetEntities().Contains(_player))
                {
                   
                    _playerFound = true;
                    CreateWay(targetCellCoordinates);
                }              
            }
        }
    }

    private void CreateWay(Vector2 payerCellcoordinates)
    {
      
        Vector2 previousPositions= payerCellcoordinates;
        Vector2 currentPosition = payerCellcoordinates;
        _way.Clear();
        while (_enemyPosition != previousPositions)
        {           
            previousPositions = _wayMap[(int)currentPosition.x, (int)currentPosition.y];
            _way.Add(currentPosition);
            currentPosition = previousPositions;
        }             
    }

    private void ClearMap()
    {
        for (int i = 0; i < _wayMap.GetLength(0); i++)
        {
            for (int j = 0; j < _wayMap.GetLength(1); j++)
            {
                _wayMap[i, j] = new Vector2(-1, -1);
            }
        }
    }
    


}
