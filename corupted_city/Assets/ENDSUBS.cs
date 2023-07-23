using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ENDSUBS : MonoBehaviour
{
    private string BadEnd;
    private string GoodEnd;
    public bool Ending;
    // Start is called before the first frame update
    void Start()
    {
        Ending = false;
        BadEnd = "You escaped \nYou was not infected\nand lived a long and happy life\nGOOD ENDING\npress e to restart";
        GoodEnd = "You escaped \nYou was infected\nYou brougt doom upon humanity\nYou are a tumor of humanity\nBAD ENDING\npress e to restart";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Character").GetComponent<Movement>() == null)
        {
            if (!Ending)
            {
                gameObject.GetComponent<Text>().text = BadEnd; 
            }
            else
            {
                gameObject.GetComponent<Text>().text = GoodEnd;
            }
        }
    }
}
