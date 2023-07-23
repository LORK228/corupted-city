using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public class HotBar : MonoBehaviour
{
    public int ActiveSlot;
    float MinOnPick;

    float mw;

    [SerializeField] Transform PlayerTrfm;
    GameObject OnPickPrior;

    [SerializeField] private Sprite Active;
    [SerializeField] private Sprite NonActive;
    [SerializeField] private Animator player;
    [SerializeField] public List<GameObject> Slots;
    [SerializeField] public List<GameObject> ItemSlots;
    [HideInInspector] public List<GameObject> OnPickUpItems;
    [HideInInspector] private Text textofbullets;
    void Start()
    {
        OnPickUpItems = new List<GameObject>();
        ActiveSlot = 0;
        textofbullets = GameObject.Find("CountOFBullet").GetComponent<Text>();
    }

    void Update()
    {   // приоритет и подбор оружия
        mw = Input.GetAxis("Mouse ScrollWheel");
        if (OnPickUpItems.Count != 0)
        {
                MinOnPick = 100000000;
            for (int i = 0; i < OnPickUpItems.Count; i++)
            {

                if (Vector2.Distance(OnPickUpItems[i].transform.position, PlayerTrfm.position) < MinOnPick) {
                    OnPickPrior = OnPickUpItems[i];
                    MinOnPick = Vector2.Distance(OnPickUpItems[i].transform.position, PlayerTrfm.position);
                }
            }
            
            //активировать меню 
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().GetComponent<ItemSlot>().item!=null)
                {
                    var weapon = OnPickPrior.GetComponent<Weapon>();
                    weapon.hotBar = GameObject.Find("Inventory").GetComponent<HotBar>();
                    weapon.textOFbullets = GameObject.Find("CountOFBullet").GetComponent<Text>();
                    OnPickPrior.GetComponent<SpriteRenderer>().enabled = false;
                    Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().item.GetComponent<SpriteRenderer>().enabled = true;
                    Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().item.transform.parent = null;
                    Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().item.GetComponent<Weapon>().textOFbullets.text = $"";
                    ItemSlots[ActiveSlot].GetComponent<SpriteRenderer>().sprite = null;
                    Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().item= null;
                    OnPickPrior.transform.parent = PlayerTrfm;
                    weapon._shootPoint = weapon.GetComponentInParent<Movement>().GetComponentInChildren<ShootPoint>().transform;
                    Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().GetComponent<SpriteRenderer>().sprite = OnPickPrior.GetComponent<SpriteRenderer>().sprite;
                    Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().item = OnPickPrior;
                    OnPickUpItems.Remove(OnPickPrior);
                    ItemSlots[ActiveSlot].transform.localScale = new Vector3(OnPickPrior.GetComponent<Weapon>().SlotSize, OnPickPrior.GetComponent<Weapon>().SlotSize, 1f);
                    ItemSlots[ActiveSlot].transform.localPosition = OnPickPrior.GetComponent<Weapon>().SlotCord;
                    if (weapon.CountOfBullet > 0)
                    {
                        weapon._canShoot = true;
                    }
                    
                }
                else
                {
                    var weapon = OnPickPrior.GetComponent<Weapon>();
                    weapon.hotBar = GameObject.Find("Inventory").GetComponent<HotBar>();
                    weapon.textOFbullets = GameObject.Find("CountOFBullet").GetComponent<Text>();
                    OnPickPrior.transform.parent = PlayerTrfm;
                    print(weapon.GetComponentInParent<Movement>().gameObject.name);
                    weapon._shootPoint = weapon.GetComponentInParent<Movement>().GetComponentInChildren<ShootPoint>().gameObject.transform;
                   
                    Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().GetComponent<SpriteRenderer>().sprite = OnPickPrior.GetComponent<SpriteRenderer>().sprite;
                    Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().item=OnPickPrior;
                    OnPickUpItems.Remove(OnPickPrior);
                    OnPickPrior.GetComponent<SpriteRenderer>().enabled = false;
                    OnPickPrior.transform.localPosition = new Vector2(0f, 0f);
                    OnPickPrior.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                    weapon.textOFbullets.text = $"{weapon.CountOfBullet}/{weapon.maxBullet}";
                    ItemSlots[ActiveSlot].transform.localScale = new Vector3(OnPickPrior.GetComponent<Weapon>().SlotSize, OnPickPrior.GetComponent<Weapon>().SlotSize, 1f);
                    ItemSlots[ActiveSlot].transform.localPosition = OnPickPrior.GetComponent<Weapon>().SlotCord;
                    if (weapon.CountOfBullet > 0)
                    {
                        weapon._canShoot = true;
                    }
                    


                }
            }
        }
        else
        {
            OnPickPrior = null;
        }
        for (int i = 0; i < 3; i++)
        {
            if (ItemSlots[i].GetComponent<ItemSlot>().item != null)
            {

                if (ActiveSlot != i)
                {
                    ItemSlots[i].GetComponent<ItemSlot>().item.gameObject.SetActive(false);
                }
                else
                {
                    ItemSlots[i].GetComponent<ItemSlot>().item.gameObject.SetActive(true);
                }
            }
        }
        // Изменение активного слота колесом мыши
        if (mw > 0.1)
        {
            if (ActiveSlot == 2)
            {
                ActiveSlot = 0;
            }
            else
            {
                ActiveSlot += 1;
            }
        }
        if (mw < -0.1)
        {
            if (ActiveSlot == 0)
            {
                ActiveSlot = 2;
            }
            else
            {
                ActiveSlot -= 1;
            }
        }
        //Изменение активного слота с 1,2,3
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActiveSlot = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActiveSlot = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActiveSlot = 2;
        }
        for (int i = 0; i < 3; i++) {
            Slots[i].GetComponent<SpriteRenderer>().sprite = NonActive;
        }
        Slots[ActiveSlot].GetComponent<SpriteRenderer>().sprite = Active;
        
        if (Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().item == null)
        {
            textofbullets.text = $"";
            player.SetInteger("Weapon", 0);
        }
        else
        {
            Weapon weapon = Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().item.GetComponent<Weapon>();
            player.SetInteger("Weapon", weapon.Number);
            if (weapon.CountOfBullet <= 0)
            {
                textofbullets.text = $"out of ammo";
            }
            else
            {
                textofbullets.text = $"{weapon.CountOfBullet}/{weapon.maxBullet}";
            }
        }    
    }




}
