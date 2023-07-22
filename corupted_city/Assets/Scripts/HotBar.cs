using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBar : MonoBehaviour
{
    public int ActiveSlot;
    float MinOnPick;

    float mw;

    [SerializeField] Transform PlayerTrfm;
    GameObject OnPickPrior;

    [SerializeField] private Sprite Active;
    [SerializeField] private Sprite NonActive;
    [SerializeField] public List<GameObject> Slots;
    [SerializeField] public List<GameObject> ItemSlots;
    [HideInInspector] public List<GameObject> OnPickUpItems;

    void Start()
    {
        OnPickUpItems = new List<GameObject>();
        ActiveSlot = 0;
        
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
                    Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().GetComponent<ItemSlot>().item.transform.parent = null;
                    Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().GetComponent<ItemSlot>().item.GetComponent<Weapon>().textOFbullets.text = $"";
                    ItemSlots[ActiveSlot].GetComponent<SpriteRenderer>().sprite = null;
                    Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().GetComponent<ItemSlot>().item= null;
                    OnPickPrior.transform.parent = PlayerTrfm;
                    Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().GetComponent<SpriteRenderer>().sprite = OnPickPrior.GetComponent<SpriteRenderer>().sprite;
                    Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().GetComponent<ItemSlot>().item = OnPickPrior;
                    OnPickPrior.transform.localPosition = OnPickPrior.GetComponent<Weapon>().PickUpOffSet;
                    OnPickUpItems.Remove(OnPickPrior);
                }
                else
                {
                    
                    OnPickPrior.transform.parent = PlayerTrfm;
                    Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().GetComponent<SpriteRenderer>().sprite = OnPickPrior.GetComponent<SpriteRenderer>().sprite;
                    Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().GetComponent<ItemSlot>().item=OnPickPrior;
                    OnPickPrior.transform.localPosition = OnPickPrior.GetComponent<Weapon>().PickUpOffSet;
                    OnPickUpItems.Remove(OnPickPrior);
                    OnPickPrior.GetComponent<SpriteRenderer>().enabled = false;
                    var weapon = OnPickPrior.GetComponent<Weapon>();
                    weapon.textOFbullets.text = $"{weapon.CountOfBullet}/{weapon.maxBullet}";
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
    }



}
