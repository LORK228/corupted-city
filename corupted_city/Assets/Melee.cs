using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public GameObject player;
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        { 
            if (player.GetComponentInChildren<Weapon>() == null)
            {
                GetComponentInParent<Animator>().SetTrigger("Shooting");
            }
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {

            if (collision.gameObject.layer == 6 && player.GetComponentInChildren<Weapon>() == null)
            {
                collision.GetComponent<EnemyAI>().Die(transform);
            }
        }
    }
}
