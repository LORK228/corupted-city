using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private Sprite _dieSprite;
    
    private bool _itHaveGun => GetComponentInChildren<Weapon>() != null;
    private NavMeshAgent _agent;
    private Scaner2D _scanner;
    private bool _iSeeFirstTime;
    private bool _iSee;
    private Weapon weapon;
    private Transform _shootPoint;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _scanner = GetComponent<Scaner2D>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _iSee = false;
        _shootPoint = GetComponentInChildren<ShootPoint>().transform;
        if (_itHaveGun)
        {
            weapon = GetComponentInChildren<Weapon>();
        }
    }

    void Update()
    {
        if (_itHaveGun)
        {
            _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = _mousePos - new Vector2(transform.position.x, transform.position.y);
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            var rotationToMouse = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotationToMouse;
        }

        _iSee = _scanner.Scan();
        if (_scanner.Scan())
            _iSeeFirstTime = true;
        if((_iSeeFirstTime && _itHaveGun == false) || (_itHaveGun == true && _iSee == false && _iSeeFirstTime == true))
            _agent.SetDestination(_player.position);
        if (_iSee && _itHaveGun == true)
            weapon.Shoot(_shootPoint.position,);
    }

    public void Die(Transform positionOfBullet)
    {
        GetComponent<SpriteRenderer>().sprite = _dieSprite;
        GetComponent<Rigidbody2D>().AddForce(positionOfBullet.position,ForceMode2D.Force);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<BoxCollider2D>().isTrigger = true;
        Destroy(GetComponent<EnemyAI>());
    }
    public void Shoot()
    {
        Vector2 lookDir = new Vector2(_player.position.x, _player.position.y) - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        var rotationToPlayer = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotationToPlayer;
        if (Input.GetMouseButton(0) && _canShoot)
        {
            if (shotgun != null)
                StartCoroutine(ShotgunShoot(_shootPoint.position, rotationToMouse));
            else
                StartCoroutine(Shoot(_shootPoint.position, rotationToMouse));
        }
    }
}
