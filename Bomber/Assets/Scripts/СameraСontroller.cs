using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class СameraСontroller : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private void Update()
    {
        transform.position += new Vector3(0, 0, (_player.transform.position.z - transform.position.z) * 2f)* Time.deltaTime;
    }
}
