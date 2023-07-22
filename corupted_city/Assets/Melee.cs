using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(0) && collision.gameObject.layer == 6 && player.GetComponentInChildren<Weapon>() == null)
        {
            collision.GetComponent<EnemyAI>().Die(transform);
        }
    }
}
