using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour
{
    public UnityAction DiedEvent;
    public abstract void Destroy();

}
