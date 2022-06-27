using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    //private Vector2 _coordinates;
    [SerializeField] private bool _canPass;    
    private List<Entity> _entitys = new List<Entity>();
    public bool hasBomb = false;
    public bool willExplode = false;
    


    public bool CanPass => _canPass;
    

    public virtual void EntityEntered(Entity entity)
    {
        _entitys.Add(entity);
    }
    public void EntityOut(Entity entity)
    {
        _entitys.Remove(entity);
    }

    public List<Entity> GetEntities()
    {
        return _entitys;
    }
    public void ClearEntitys()
    {
        _entitys.Clear();
       
    }
}
