using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class exit : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Movement player;
    [SerializeField] private Camera camera;
    [SerializeField] private THE_END end;
    [SerializeField] private GameObject inventar;
    [SerializeField] private GameObject health;
    [SerializeField] private SpriteRenderer playersprite;
    [SerializeField] private AudioSource audio;
    [SerializeField] private Text Tasktext;
    [SerializeField] private Text Bullets;
    [SerializeField] private Text Endtext;
    public bool Infected;
    private bool isIn;
    private void Start()
    {
        isIn = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Endtext.GetComponent<ENDSUBS>().Ending = player.Corrupted;
        isIn = true;
        Destroy(audio);
        Destroy(health);
        Destroy(playersprite);
        Destroy(inventar);
        Destroy(end);
        Destroy(player);
        Destroy(Tasktext);
        Destroy(Bullets);
        camera.cullingMask = 1<<5;
        text.text = "" ;

    }
    
    private void Update()
    {
        if (isIn)
        {
            text.text = "";
        }
        if (Input.GetKeyDown(KeyCode.E) && isIn)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
