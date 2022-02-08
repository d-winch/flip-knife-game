using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Knife : MonoBehaviour {

    public Rigidbody rb;
    [Range(1, 10)]
    public float force = 5f;
    [Range(1, 10)]
    public float torque = 20f;
    Vector3 startSwipe;
    Vector3 endSwipe;

    private float timeFlying;

    public Text scoreText;
    private int scoreCount = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!rb.isKinematic)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            startSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            endSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Swipe();
        }
    }

    void Swipe()
    {
        rb.isKinematic = false;
        timeFlying = Time.time;
        Vector2 direction = endSwipe - startSwipe;
        rb.AddForce(direction * force, ForceMode.Impulse);
        rb.AddTorque(0f, 0f, -torque, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.name);
        if (col.tag == "Block")
        {
            rb.isKinematic = true;
            scoreCount++;
            scoreText.text = "Score: " + scoreCount.ToString();
        } else
        {
            Restart();
        }
    }

    void OnCollisionEnter()
    {
        float timeInAir = Time.time - timeFlying;
        if (!rb.isKinematic && timeInAir >= .1f)
        {
            Debug.Log("Fail");
            Restart();
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
