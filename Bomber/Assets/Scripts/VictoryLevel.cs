using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VictoryLevel : MonoBehaviour
{
    public UnityAction VictoryEvent;


    public void Victory()
    {
        VictoryEvent?.Invoke();
    }
}

