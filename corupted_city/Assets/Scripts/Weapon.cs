using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Weapon : MonoBehaviour
{
    public Transform[] countOfPoints => GetComponentsInChildren<Point>().Select(x => x.GetComponent<Transform>()).ToArray();
    private Vector2 _mousePos;

    private void Update()
    {
        if(GetComponentInParent<Movement>() != null)
        {
            _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = _mousePos - new Vector2(transform.position.x, transform.position.y);
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
