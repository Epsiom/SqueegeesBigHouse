using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Player data")]
[Serializable]
public class PlayerData : ScriptableObject
{
    public float movementSpeed;
    public float hp;
    public float jumpForce;
    public float vacuumRotationSpeed;
}
