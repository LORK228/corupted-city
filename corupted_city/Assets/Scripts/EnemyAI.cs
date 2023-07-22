using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private Sprite _dieSprite;
    [SerializeField] private float _distanceToChase;

    private bool _itHaveGun => GetComponentInChildren<Weapon>() != null;
    private bool _itHaveShotGun => GetComponentInChildren<ShotGun>() != null;
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
        if (_itHaveGun)
        {
            _shootPoint = GetComponentInChildren<ShootPoint>().transform;
            weapon = GetComponentInChildren<Weapon>();
        }
    }

    void Update()
    {
        _iSee = Vector3.Distance(transform.position, _player.position) < _distanceToChase;
        if (Vector3.Distance(transform.position, _player.position) < _distanceToChase)
            _iSeeFirstTime = true;
        if((_iSeeFirstTime && _itHaveGun == false) || (_itHaveGun == true && _iSee == false && _iSeeFirstTime == true))
            _agent.SetDestination(_player.position);
        if (_iSee && _itHaveGun == true && weapon._canShoot)
        {
            Vector2 lookDir = new Vector2(_player.position.x, _player.position.y) - new Vector2(transform.position.x, transform.position.y);
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            var rotationToPlayer = Quaternion.AngleAxis(angle, Vector3.forward);
            weapon.transform.rotation = rotationToPlayer;
            if (_itHaveShotGun)
                StartCoroutine(weapon.ShotgunShoot(_shootPoint.position, rotationToPlayer,true));
            else
                StartCoroutine(weapon.Shoot(_shootPoint.position, rotationToPlayer, true));
        }
    }

    public void Die(Transform positionOfBullet)
    {
        GetComponent<SpriteRenderer>().sprite = _dieSprite;
        GetComponent<Rigidbody2D>().AddForce(positionOfBullet.position,ForceMode2D.Force);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponentInChildren<Weapon>().Throw();
        print(1);
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());
        print(1);
        Destroy(GetComponent<EnemyAI>());
    }
}
