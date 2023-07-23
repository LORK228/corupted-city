using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class THE_END : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Text Tasktext;
    [SerializeField] private GameObject exit;
    [SerializeField] private string[] exitDialog;
    private bool isIn;
    private bool THEEND;

    void Start()
    {
        isIn = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isIn)
        {
            THEEND = true;
            Destroy(exit);
        }
        if (THEEND)
        {
            Tasktext.text = "Go to the exit";
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            isIn = true;

            text.text = "Press E";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        text.text = "";
        isIn = false;
    }
}
