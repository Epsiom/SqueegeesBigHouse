using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GhostData", menuName = "Ghost/Ghost data")]
[Serializable]
public class GhostData : ScriptableObject
{
    public float movementSpeed;
    public float durationToCapture;
    public float damage;

}
