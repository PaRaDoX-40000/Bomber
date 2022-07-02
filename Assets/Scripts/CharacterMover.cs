using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private Entity _Character;
    [SerializeField] private MapController _mapController;
    [SerializeField] private int _speed = 1;
    private Cell _characterCell;
    private Cell _targetCell;
    private bool _inMotion=false;

    public Cell PlayerCell => _characterCell; 
    public Cell TargetCell => _targetCell;

    public bool InMotion => _inMotion;


    public void Initialize(MapController mapController)
    {
        _mapController = mapController;
    }


    private void Start()
    {
        _Character = GetComponent<Entity>();
        _characterCell = _mapController.Map[(int)transform.localPosition.x, (int)transform.localPosition.z];
        _characterCell.EntityEntered(_Character);
       
    }



    public void TryMove(Vector3 offset)
    {
        if (_inMotion == false)
        {
            Vector3 characterPosition = transform.localPosition;

            if (_mapController.CanWalkCell(characterPosition + offset, out _targetCell))
            {
                _mapController.CanWalkCell(characterPosition, out _characterCell);
                StartCoroutine(Move(_targetCell));
            }

        }
    }
    
    private IEnumerator Move(Cell targetCell)
    {
        _inMotion = true;
        bool changedCell = false;
        Vector3 targetPosition = targetCell.transform.localPosition;
        transform.LookAt(targetPosition);
        while (transform.localPosition != targetPosition)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, _speed * Time.deltaTime);            
            if((transform.localPosition - targetPosition).magnitude< 0.5 && _characterCell!= null && changedCell == false)
            {
                _characterCell.EntityOut(_Character);
                targetCell.EntityEntered(_Character);
                changedCell =true;
            }
            yield return new WaitForEndOfFrame();

        }
        _characterCell = targetCell;
        _inMotion = false;
    }
}
