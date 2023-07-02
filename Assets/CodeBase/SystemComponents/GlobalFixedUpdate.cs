using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFixedUpdate : MonoBehaviour
{
    private void FixedUpdate()
    {
        for (int i = 0; i < MonoCache.allFixed.Count; i++)
        {
            MonoCache.allUpdate[i].FixedUpdateTick();
        } 
    }
}
