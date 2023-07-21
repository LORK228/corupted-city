using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField] private Transform _crosshair;
    [SerializeField] private Camera _camera;


    void Update()
    {
        var position = _camera.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        _crosshair.position = position;
    }
}
