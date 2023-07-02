using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoCache : MonoBehaviour
{
    public static List<MonoCache> allUpdate = new List<MonoCache>(10001);
    public static List<MonoCache> allFixed = new List<MonoCache>(10001);

    private void OnEnable()
    {
        allUpdate.Add(this);
        allFixed.Add(this);
    }
        
    private void OnDisable()
    {
        allUpdate.Remove(this);
        allFixed.Remove(this);
    }

    private void OnDestroy()
    {
        allUpdate.Remove(this);
        allFixed.Remove(this);
    }

    public void UpdateTick() => OnUpdateTick();
    public virtual void OnUpdateTick() { }

    public void FixedUpdateTick() => OnFixedUpdateTick();
    public virtual void OnFixedUpdateTick() { }
}
