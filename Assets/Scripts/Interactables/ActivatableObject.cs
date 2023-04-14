using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the parent script for something that can be activated by a button.
// 
public class ActivatableObject : MonoBehaviour
{
    [HideInInspector]
    public bool isCurrentlyActive = false;
}
