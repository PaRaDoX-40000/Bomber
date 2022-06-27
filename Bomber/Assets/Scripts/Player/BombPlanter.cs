using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlanter : MonoBehaviour
{
    [SerializeField] private BombPool _bombPool;
    [SerializeField] private MapController _MapController;
    [SerializeField] private float _calldown=0.5f;   
    private bool _canPlaceBomb=true;


    public void PlaceBomb(Cell _playerCell, Cell targetCell)
    {
        if (_canPlaceBomb == true)
        {
            if ((_playerCell.transform.localPosition - transform.localPosition).magnitude > (targetCell.transform.localPosition - transform.localPosition).magnitude)
                Place(_playerCell);
            else
                Place(targetCell);
        }
       
         
    }

    private void Place(Cell nearestCell)
    {
       
        if ((nearestCell.transform.localPosition - transform.localPosition).magnitude<0.4f)
        {
            if (nearestCell.hasBomb == false)
            {
                if (_bombPool.TryGetObjectr(out GameObject Bomb))
                {
                    Bomb.transform.localPosition = nearestCell.transform.localPosition;
                    nearestCell.hasBomb = true;
                    Bomb.SetActive(true);
                    StartCoroutine(Calldown());
                }
            }
            
        }
    }
    private IEnumerator Calldown()
    {
        _canPlaceBomb = false;
        yield return new WaitForSeconds(_calldown);
        _canPlaceBomb = true;
    }
}
