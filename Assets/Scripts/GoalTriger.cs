using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTriger : MonoBehaviour
{
    public event Action Goal;
    private void OnTriggerEnter(Collider other)
    {
        Goal?.Invoke();
    }
}
