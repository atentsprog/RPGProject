using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class TerrainObjectTool : MonoBehaviour
{
    [ContextMenu("���� ����")]
    void SetHeight()
    {
        var position = transform.position;
        float height = Terrain.activeTerrain.SampleHeight(position);
        position.y = height;
        print(height);
        transform.position = position;
    }
}
