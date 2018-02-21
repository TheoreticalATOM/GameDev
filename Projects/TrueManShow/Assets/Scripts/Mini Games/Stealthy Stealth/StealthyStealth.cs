using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StealthyStealth : MonoBehaviour
{
    public Image player;
    public float playerSpeed = 200.0f;
    public bool CanControl;

    public Image camera1;
    public Image camera2;
    public Image camera3;
    public float speed = 0.5f;
    public float maxRotation = 30f;
    public Image door;
    public GameObject gameStuff;
    public GameObject gameWon;
    public GameObject MenuStuff;
    public bool menuActive = true;

    public UnityEvent FinishedGame;

    private Vector3 mStartPos;

    public void StartGame()
    {
        gameObject.SetActive(true);
        MenuStuff.SetActive(true);
        gameStuff.SetActive(false);
        gameWon.SetActive(false);
    }

    void Start()
    {
        gameObject.SetActive(false);
        MenuStuff.SetActive(false);
        gameStuff.SetActive(false);
        gameWon.SetActive(false);

        mStartPos = transform.position;
    }


    void Update()
    {
        if (CanControl)
        {
            var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            transform.position += move * playerSpeed * Time.deltaTime;

            if (Input.GetKeyDown("space") && menuActive)
            {
                MenuStuff.SetActive(false);
                gameStuff.SetActive(true);
                gameWon.SetActive(false);
            }
        }
        
        camera1.transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));
        camera2.transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));
        camera3.transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            gameStuff.SetActive(false);
            gameWon.SetActive(true);
            //player.gameObject.SetActive(false);
            CanControl = false;
            FinishedGame.Invoke();
        }

        if (other.gameObject.CompareTag("VisionCone"))
        {
            player.transform.position = mStartPos;
        }
    }
}