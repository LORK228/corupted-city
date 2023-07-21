using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Transform[] _hands;
    private Rigidbody2D _rb;
    private Vector2 _movement;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        if(GetComponentInChildren<Weapon>() != null)
        {
            var Points = GetComponentInChildren<Weapon>().countOfPoints;
            _hands[0].position = Points[0].position;
            _hands[1].position = Points[1].position;
        }
        else
        {
            _hands[0].localPosition = new Vector3(-1.52f, -5.48f, 0);
            _hands[1].localPosition = new Vector3(1.62f, -5.48f, 0);
        }
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
    }
}
