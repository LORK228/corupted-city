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
            print(OnPickPrior.name);
            //активировать меню 
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                if (Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().GetComponent<ItemSlot>().item!=null)
                {
                    //положить оружие
                }
                else
                {
                    OnPickPrior.transform.parent = PlayerTrfm;
                    Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().GetComponent<SpriteRenderer>().sprite = OnPickPrior.GetComponent<SpriteRenderer>().sprite;
                    Slots[ActiveSlot].GetComponentInChildren<ItemSlot>().GetComponent<ItemSlot>().item=OnPickPrior;
                    OnPickPrior.transform.localPosition = OnPickPrior.GetComponent<Weapon>().PickUpOffSet;
                    OnPickPrior.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
        else
        {
            OnPickPrior = null;
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
