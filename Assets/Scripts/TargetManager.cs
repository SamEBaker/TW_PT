 using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TargetManager : MonoBehaviour
{

    public List<GameObject> targets;
    public int HitTargets = 0;
    public int HitsNeeded;
    public bool DoorOpen = false;
    public UnityEvent Solved;

    public void CheckSolved()
    {
        if(HitTargets == HitsNeeded) 
            Solved.Invoke(); 
    }
    public void HitTarget()
    {
        HitTargets++;
        CheckSolved();
    }
    public void ClearAll()
    {
        for(int i = 0; i < targets.Count; i++)
        {
            targets[i].GetComponent<Targets>().Clear();
        }
        HitTargets = 0;
    }
}
