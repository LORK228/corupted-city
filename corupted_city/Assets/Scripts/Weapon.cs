using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _waintSecondsShoot;
    [SerializeField] public float flyDist;
    [SerializeField] public int Number;
    [SerializeField] public int CountOfBullet;
    [HideInInspector] public Text textOFbullets;
    public float SlotSize;
    public Vector2 SlotCord;
    private HotBar hotBar;
    public int maxBullet;
    private int _shotgunAmmunition;
    private Vector2 _minAndMaxRotateShootGun;
    [HideInInspector] public bool Flying;
    private Vector2 startPoint;
    private Vector3 startRotation;
    private Animator player;
    public Transform[] countOfPoints => GetComponentsInChildren<Point>().Select(x => x.GetComponent<Transform>()).ToArray();
    public int flySpeed;
    public int rotationDegree;
    private Vector2 _mousePos;
    [HideInInspector] public Transform _shootPoint;
    public bool _canShoot;
    private bool inMovement => GetComponentInParent<Movement>() != null;
    ShotGun shotgun => GetComponent<ShotGun>();

    private void Start()
    {
        hotBar = GameObject.Find("Inventory").GetComponent<HotBar>();
        textOFbullets = GameObject.Find("CountOFBullet").GetComponent<Text>();
        player = GameObject.Find("Character").GetComponent<Animator>();
        textOFbullets.text = $"";
        maxBullet = CountOfBullet;
        _canShoot = true;
        if(shotgun != null)
        {
            var shotgun = GetComponent<ShotGun>();
            _shotgunAmmunition = shotgun.ShotgunAmmunition;
            _minAndMaxRotateShootGun = shotgun.MinAndMaxRotateShootGun;
        }
    }

    private void Update()
    {
        
        if(inMovement && CountOfBullet >= 0)
        { 
            _shootPoint.localPosition = new Vector3(1.51999998f, -0.51f, 0);
            _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = _mousePos - new Vector2(transform.position.x, transform.position.y);
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            var rotationToMouse = Quaternion.AngleAxis(angle, Vector3.forward);
            if (CountOfBullet <= 0)
                textOFbullets.text = $"out of ammo";
            
            else if (Input.GetMouseButton(0) && _canShoot)
            {
                if (shotgun != null)
                    StartCoroutine(ShotgunShoot(_shootPoint.position, rotationToMouse));
                else
                    StartCoroutine(Shoot(_shootPoint.position, rotationToMouse));
            }
        }
    }
    private void FixedUpdate()
    {
        if (Flying&&Vector2.Distance(startPoint,transform.position)<flyDist)
        {
            transform.position += startRotation * Time.deltaTime*flySpeed;
            transform.Rotate(0f, 0f, rotationDegree);
        }
        else
        {
            Flying = false;
        }
    }

    public IEnumerator Shoot(Vector3 pointToShoot,Quaternion rotation,bool isAi = false)
    {
        player.SetTrigger("Shooting");
        var bullet = Instantiate(_bullet, pointToShoot, rotation);
        CountOfBullet -= 1;
        textOFbullets.text = $"{CountOfBullet}/{maxBullet}";
        bullet.GetComponent<Bullet>().Ai = isAi;
        _canShoot = false;
        yield return new WaitForSeconds(_waintSecondsShoot);
        _canShoot = true;
    }
    public IEnumerator ShotgunShoot(Vector3 pointToShoot, Quaternion rotation, bool isAi = false)
    {
        player.SetTrigger("Shooting");
        var rotate = rotation.eulerAngles;
        CountOfBullet -= 1;
        var rotateIznach = rotate;
        for (int i = 0; i < _shotgunAmmunition; i++)
        {
            rotate = rotateIznach;
            rotate.z += UnityEngine.Random.Range(_minAndMaxRotateShootGun.x, _minAndMaxRotateShootGun.y);
            var bullet =  Instantiate(_bullet, pointToShoot, Quaternion.Euler(rotate));
            textOFbullets.text = $"{CountOfBullet}/{maxBullet}";
            bullet.GetComponent<Bullet>().Ai = isAi;
        }
        _canShoot = false;
        yield return new WaitForSeconds(_waintSecondsShoot);
        _canShoot = true;
    }
    public void Throw()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        textOFbullets.text = $"";
        startPoint = new Vector2(transform.position.x, transform.position.y);
        startRotation = GetComponentInParent<Movement>().transform.right;
        Flying = true;
        transform.parent = null;
        hotBar.ItemSlots[hotBar.ActiveSlot].transform.localScale = new Vector3(1f,1f, 1f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (Flying)
        {
            if (collision.gameObject.GetComponent<Weapon>())
            {

            }
        }   
    }
}
