using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCell : Cell
{
    [SerializeField] private VictoryLevel _victoryLevel;

    public override void EntityEntered(Entity entity)
    {
        base.EntityEntered(entity);
        if (entity.gameObject.TryGetComponent<Player>(out Player player))
        {
            _victoryLevel.Victory();
        }
    }
}
