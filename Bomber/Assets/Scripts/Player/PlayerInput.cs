using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(CharacterMover), typeof(BombPlanter),typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private BombPlanter _bombPlanter;
    private CharacterMover _playerMover;
    private bool _reportedOffset=false;
    public UnityAction PlayerHasShiftedEvent;



    void Start()
    {
        _bombPlanter = GetComponent<BombPlanter>();
        _playerMover = GetComponent<CharacterMover>();
    }

   
    void Update()
    {
        if (_reportedOffset == false && _playerMover.InMotion == false)
        {
            
            PlayerHasShiftedEvent?.Invoke();
            _reportedOffset = true;
        }
        if (_reportedOffset == true && _playerMover.InMotion == true)
        {
            _reportedOffset = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _bombPlanter.PlaceBomb(_playerMover.PlayerCell, _playerMover.TargetCell);
        }
        if (Input.GetKey(KeyCode.W))
        {
            _playerMover.TryMove(new Vector3(-1,0,0));          
        }
        if (Input.GetKey(KeyCode.S))
        {
            _playerMover.TryMove(new Vector3(1,0,0));           
        }
        if (Input.GetKey(KeyCode.D))
        {
            _playerMover.TryMove(new Vector3(0,0,1));           
        }
        if (Input.GetKey(KeyCode.A))
        {
            _playerMover.TryMove(new Vector3(0,0,-1));
        }

        

    }
    

}
