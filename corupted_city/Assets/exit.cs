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
    private bool isIn;
    private void Start()
    {
        isIn = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isIn = true;
        Destroy(health);
        Destroy(playersprite);
        Destroy(inventar);
        Destroy(end);
        Destroy(player);
        text.text = "�������������� ����� � ����� �������������" ;
    }
    
    private void Update()
    {
        if (isIn)
        {
            text.text = "�������������� ����� � ����� �������������";
        }
        if (Input.GetKeyDown(KeyCode.E) && isIn)
        {
            camera.cullingMask = 5;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
