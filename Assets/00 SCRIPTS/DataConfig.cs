using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ConfigData",menuName ="Data/Test")]
public class DataConfig : ScriptableObject
{
    public float speed=10;
    public float speedRotate = 0.3f;
    public float maxTimeSpaw=3;
}
