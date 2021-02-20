using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class SetColliderToTerrain : MonoBehaviour
{
    private Vector3 _mapSize, _mapCenter;
    private Terrain _terrain;
    private BoxCollider _boxCollider;
    
    void Start()
    {
        _terrain = GetComponentInParent<Terrain>();
        _boxCollider = GetComponent<BoxCollider>();
        _mapSize = _terrain.terrainData.size;
        _boxCollider.size = _mapSize;
        _boxCollider.center = new Vector3(_mapSize.x/2, 0, _mapSize.z/2);
    }
}
