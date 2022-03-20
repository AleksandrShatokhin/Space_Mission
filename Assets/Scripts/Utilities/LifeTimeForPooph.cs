using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimeForPooph : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 1.0f);
    }
}
