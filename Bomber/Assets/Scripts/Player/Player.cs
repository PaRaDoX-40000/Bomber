using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public override void Destroy()
    {
        gameObject.SetActive(false);
        DiedEvent?.Invoke();
    }

}
