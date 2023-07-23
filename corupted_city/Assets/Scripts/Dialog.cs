using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private string[] lines;
    [SerializeField] private float speedText;
    [SerializeField] private Text dialogText;
    [SerializeField] private Text TaskText;
    [SerializeField] private GameObject dialog;
    [SerializeField] private Transform player;
    private bool questnotdone;
    private bool questdone;
    [SerializeField] private string[] fOfflines;
    [SerializeField] private string[] exitlines;

    private bool isIn;
    private bool isDialog;

    private int _index;
    private Quaternion lookForward;
    private void Start()
    {
        lookForward =  transform.rotation;
        dialogText.text = string.Empty;
        text.text = string.Empty;
        isIn = false;
        isDialog = false;
        questnotdone = false;
        questdone = false;
    }

    private void Update()
    {
        if (isDialog&&isIn)
        {
            dialog.SetActive(true);
        }
        else
        {
            dialog.SetActive(false);
        }
        if (questnotdone&&!questdone)
        {
            lines = fOfflines;
        }
        if (TaskText.text == "Go to the exit")
        {
            questdone = true;
        }
        if (questdone)
        {
            lines = exitlines;
        }
        
        if (isIn)
        {
            var playerPos = player.position;
            Vector2 lookDir = new Vector2(playerPos.x, playerPos.y) - new Vector2(transform.position.x, transform.position.y);
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            var rotationToMouse = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotationToMouse;
        }
        if (Input.GetKeyDown(KeyCode.E) && isIn )
        {
            if (isDialog)
            {
                ScipTextClick();
            }
            else
            {
                dialog.SetActive(true);
                text.text = "";
                StartDialogue();
                isDialog = true;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isIn = true;
            
            text.text = "Press E";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.rotation = lookForward;
        text.text = "";
        dialog.SetActive(false);
        isIn = false;
    }

    private void StartDialogue()
    {
        _index = 0;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        
        foreach (char letter in lines[_index].ToCharArray())
        {
            
                dialogText.text += letter;
                yield return new WaitForSeconds(speedText);
        }        
           
    }

    public void ScipTextClick()
    {
        if(dialogText.text == lines[_index])
        {
            NextLines();
        }
        else
        {
            StopAllCoroutines();
            dialogText.text = lines[_index];
        }
    }

    private void NextLines()
    {
        if(_index < lines.Length - 1)
        {
            _index++;
            dialogText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            isDialog = false;
            dialogText.text = "";
            dialog.SetActive(false);
            questnotdone = true;
            TaskText.text = "Find money";
        }
    }
}


