using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealthyStealth : MonoBehaviour
{
    public Image player;
    public float playerSpeed = 200.0f;

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

    void Start()
    {
        MenuStuff.SetActive(true);
        gameStuff.SetActive(false);
        gameWon.SetActive(false);
    }


    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move *playerSpeed * Time.deltaTime;

        camera1.transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));
        camera2.transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));
        camera3.transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));

        if (Input.GetKeyDown("space") && menuActive )
        {
            MenuStuff.SetActive(false);
            gameStuff.SetActive(true);
            gameWon.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            Debug.Log("gameWon");
            gameStuff.SetActive(false);
            gameWon.SetActive(true);
        }

        if (other.gameObject.CompareTag("VisionCone"))
        {
            player.transform.position = new Vector3(-760, 400, 0);
        }
    }
}