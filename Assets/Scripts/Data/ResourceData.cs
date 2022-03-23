using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecource", menuName ="RecourceData", order = 51)]
public class ResourceData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _cost;

    public string Name => _name;
    public int Cost => _cost;
}
