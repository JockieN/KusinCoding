using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;
    private Vector3 originalPos;
    private float groundDistance;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        originalPos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontical = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontical, 0, moveVertical);

        rb.AddForce(movement * speed);

        if (Input.GetKey(KeyCode.Space) && IsGrounded())
            rb.velocity = Vector3.up * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }

        if (other.gameObject.CompareTag("FallTag"))
            this.gameObject.transform.position = originalPos;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, groundDistance + 1f);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString() + " / 18";
        if (count >= 18)
        {
            countText.text = "Completed!";
            winText.text = "You Win!"; // Doesn't work
        }
    }
}