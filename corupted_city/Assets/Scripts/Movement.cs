using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private Rigidbody2D _rb;
    private Vector2 _movement;
    [SerializeField] private HotBar HotBar;
    public int health = 3;
    private health healthPlayer;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        healthPlayer = GetComponentInChildren<health>();
    }

    void Update()
    {
        if (healthPlayer.healthCount <= 0)
        {
            Dead();
        }
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetMouseButton(1))
        {

            if (HotBar.Slots[HotBar.ActiveSlot].GetComponentInChildren<ItemSlot>().GetComponent<SpriteRenderer>().sprite != null)
            {
                HotBar.Slots[HotBar.ActiveSlot].GetComponentInChildren<ItemSlot>().GetComponent<ItemSlot>().item.GetComponent<Weapon>().Throw();
                HotBar.Slots[HotBar.ActiveSlot].GetComponentInChildren<ItemSlot>().GetComponent<SpriteRenderer>().sprite = null;
                HotBar.Slots[HotBar.ActiveSlot].GetComponentInChildren<ItemSlot>().GetComponent<ItemSlot>().item = null;
            }
        }
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (_rb.IsSleeping())
        {
            GetComponent<Animator>().SetBool("Mooving", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Mooving", true);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.GetComponent<Weapon>() != null)
        {
            if (HotBar.OnPickUpItems.Find(x => x == other.gameObject) == null)
            {
                HotBar.OnPickUpItems.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Weapon>() != null)
        {
            if (HotBar.OnPickUpItems.Find(x => x == other.gameObject) != null)
            {
                HotBar.OnPickUpItems.Remove(other.gameObject);
            }
        }
    }

    private void Dead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
