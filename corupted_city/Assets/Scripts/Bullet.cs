using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    public bool Ai;
    void Update()
    {
        transform.position += transform.right * _speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<EnemyAI>() != null && Ai == false)
            collision.GetComponent<EnemyAI>().Die(transform);

        if (collision.gameObject.layer != 2 && collision.GetComponent<Weapon>() == null && collision.gameObject.layer != 3 && Ai == false)
        Destroy(gameObject);

        if (Ai && collision.gameObject.layer != 2 && collision.GetComponent<Weapon>() == null && collision.gameObject.layer != 6)
        {
            Destroy(gameObject);
        }
    }
}
