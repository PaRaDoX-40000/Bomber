using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Player _player;

    public void Initialize(Player player)
    {
        _player = player;
    }

    private void Update()
    {
        if((_player.transform.localPosition- transform.localPosition).magnitude < 0.45f)
        {
            _player.Destroy();
        }
    }
}
