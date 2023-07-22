using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private Sprite _dieSprite;

    private NavMeshAgent _agent;
    private Scaner2D _scanner;
    private bool _iSee;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _scanner = GetComponent<Scaner2D>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _iSee = false;
    }

    void Update()
    {
        if (_scanner.Scan())
            _iSee = true;
        if(_iSee)
            _agent.SetDestination(_player.position);
    }

    public void Die(Transform positionOfBullet)
    {
        GetComponent<SpriteRenderer>().sprite = _dieSprite;
        GetComponent<Rigidbody2D>().AddForce(positionOfBullet.position,ForceMode2D.Force);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<BoxCollider2D>().isTrigger = true;
        Destroy(GetComponent<EnemyAI>());
    }
}
