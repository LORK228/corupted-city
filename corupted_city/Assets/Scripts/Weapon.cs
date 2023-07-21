using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _waintSecondsShoot;
    [SerializeField] public float flyDist;

    private int _shotgunAmmunition;
    private Vector2 _minAndMaxRotateShootGun;
    private bool Flying;
    private Vector2 startPoint;
    public Transform[] countOfPoints => GetComponentsInChildren<Point>().Select(x => x.GetComponent<Transform>()).ToArray();
    private Vector2 _mousePos;
    private Transform _shootPoint;
    private bool _canShoot;
    private bool inMovement => GetComponentInParent<Movement>() != null;
    ShotGun shotgun => GetComponent<ShotGun>();

    private void Start()
    {
        _canShoot = true;
        _shootPoint = GetComponentInChildren<ShootPoint>().transform;
        if(shotgun != null)
        {
            var shotgun = GetComponent<ShotGun>();
            _shotgunAmmunition = shotgun.ShotgunAmmunition;
            _minAndMaxRotateShootGun = shotgun.MinAndMaxRotateShootGun;
        }
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
                if(shotgun != null)
                    StartCoroutine(ShotgunShoot(_shootPoint.position, rotationToMouse));
                else
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
    private IEnumerator ShotgunShoot(Vector3 pointToShoot, Quaternion rotation)
    {
        var rotate = rotation.eulerAngles;
        var rotateIznach = rotate;
        for (int i = 0; i < _shotgunAmmunition; i++)
        {
            rotate = rotateIznach;
            rotate.z += UnityEngine.Random.Range(_minAndMaxRotateShootGun.x, _minAndMaxRotateShootGun.y);
            Instantiate(_bullet, pointToShoot, Quaternion.Euler(rotate));
        }
        _canShoot = false;
        yield return new WaitForSeconds(_waintSecondsShoot);
        _canShoot = true;
    }
    public void Throw()
    {
        startPoint = transform.position;

        gameObject.transform.parent = null;
        while (Vector2.Distance(startPoint, transform.position) < flyDist) 
        {
            print(1);
            transform.position = new Vector2(transform.position.x, transform.position.y + 1);
        }
    }
}
