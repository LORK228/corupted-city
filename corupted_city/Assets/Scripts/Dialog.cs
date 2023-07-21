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
    [SerializeField] private GameObject dialog;

    private bool isIn;
    private bool isDialog;

    private int _index;
    private void Start()
    {
        dialogText.text = string.Empty;
        text.text = string.Empty;
        isIn = false;
        isDialog = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isIn )
        {
            if (isDialog)
            {
                ScipTextClick();
            }
            else
            {
                text.text = "";
                StartDialogue();
                isDialog = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isIn = true;
        dialog.SetActive(true);
        text.text = "Press E";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
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
        }
    }
}


