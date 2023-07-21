using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBar : MonoBehaviour
{
    int ActiveSlot;
    int MinOnPick;

    float mw;

    Transform PlayerTrfm;
    GameObject OnPickPrior;
    
    [HideInInspector] public List<GameObject> Items;
    [HideInInspector] public List<GameObject> OnPickUpItems;

    void Start()
    {
        ActiveSlot = 0;
        PlayerTrfm = gameObject.transform;
        for (int i =0; i <3; i++)
        {
            Items.Add(null);
        }
        
    }

    void Update()
    {
        mw = Input.GetAxis("Mouse ScrollWheel");
        if (OnPickUpItems.Count != 0)
        {
            for (int i=0; i < OnPickUpItems.Count; i++)
            {
                MinOnPick = 100000000;
                if (Vector2.Distance(OnPickUpItems[i].transform.position, PlayerTrfm.position)<MinOnPick){
                    OnPickPrior = OnPickUpItems[i];
                }
            }
            //������������ ���� 
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Items[ActiveSlot] != null)
                {
                    //�������� ������
                }
                else
                {
                    Items[ActiveSlot] = OnPickPrior;
                }
            }
        }
        else
        {
            OnPickPrior = null;
        }
        // ��������� ��������� ����� ������� ����
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
        //��������� ��������� ����� � 1,2,3
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HotBar>() != null){
            OnPickUpItems.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HotBar>() != null)
        {
            OnPickUpItems.Remove(collision.gameObject);
        }
    }
}
