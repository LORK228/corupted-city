using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _waintSecondsShoot;
    public Transform[] countOfPoints => GetComponentsInChildren<Point>().Select(x => x.GetComponent<Transform>()).ToArray();
    private Vector2 _mousePos;
    private Transform _shootPoint;
    private bool _canShoot;
    private bool inMovement => GetComponentInParent<Movement>() != null;


    private void Start()
    {
        _canShoot = true;
        _shootPoint = GetComponentInChildren<ShootPoint>().transform;
    }

    private void Update()
    {
        if(inMovement)
        {
            _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = _mousePos - new Vector2(transform.position.x, transform.position.y);
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            var rotationToMouse = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotationToMouse;
            if (Input.GetMouseButton(0) && _canShoot)
            {
                StartCoroutine(Shoot(_shootPoint.position, rotationToMouse));
            }
        }
    }

    private IEnumerator Shoot(Vector3 pointToShoot,Quaternion rotation)
    {
        Instantiate(_bullet, pointToShoot, rotation);
        _canShoot = false;
        yield return new WaitForSeconds(_waintSecondsShoot);
        _canShoot = true;
    }
}
