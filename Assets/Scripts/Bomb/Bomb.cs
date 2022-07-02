using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bomb : MonoBehaviour
{
    [SerializeField] private ParticleEffect[] _particleEffect;
    [SerializeField] private MapController _mapController;
    [SerializeField] private GameObject _bomb;
    private int _particleEffectCounter;
    public UnityEvent BombExploded;

    private void OnEnable()
    {
        _bomb.SetActive(true);
        StartCoroutine(Eexplosion());
    }

    public void Initialize(MapController mapController)
    {
        _mapController = mapController;
    }


    IEnumerator Eexplosion()
    {
        yield return new WaitForSeconds(2);
        _bomb.SetActive(false);
        _particleEffectCounter = 0;

        CellExplosion(new Vector3(1, 0, 0));
        CellExplosion(new Vector3(-1, 0, 0));
        CellExplosion(new Vector3(0, 0, 1));
        CellExplosion(new Vector3(0, 0, -1));
        EntityExplosion(Vector3.zero, 0, _mapController.Map[(int)transform.localPosition.x, (int)transform.localPosition.z]);

        _mapController.Map[(int)transform.localPosition.x, (int)transform.localPosition.z].hasBomb = false;
        BombExploded?.Invoke();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    private void CellExplosion(Vector3 direction)
    {
        Vector3 BombPosition = transform.localPosition;
        for (int i = 1; i <= 3; i++)
        {
            if(_mapController.CheckingExitFromMap(BombPosition + direction * i))
            {
               
                if(_mapController.CanWalkCell(BombPosition + direction * i,out Cell targetCell))
                {
                    EntityExplosion(direction, i, targetCell);
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

    }

     private void EntityExplosion(Vector3 direction,int i, Cell targetCell)
    {
        _particleEffect[_particleEffectCounter].transform.localPosition = direction * i;
        _particleEffect[_particleEffectCounter].Play();

        _particleEffectCounter++;
       
        if (targetCell.GetEntities().Count>0)
        {
            for(int j= 0;j< targetCell.GetEntities().Count;j++)
            {
                if (targetCell.GetEntities()[j] != null)
                {
                    targetCell.GetEntities()[j].Destroy();
                }
                
            }
            targetCell.ClearEntitys();


        }
    }

}
