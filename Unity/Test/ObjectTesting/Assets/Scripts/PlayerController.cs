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

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (speed != 5)
                speed = 5;
            else
                speed = 20;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Boost Tag"))
        {
            StartCoroutine(BoostPickup());
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Scale Tag"))
        {
            StartCoroutine(ScalePickup());
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("FallTag"))
            this.gameObject.transform.position = originalPos;
    }

    IEnumerator BoostPickup()
    {
        speed = 10;
        yield return new WaitForSeconds(3f);
        speed = 5;
    }

    IEnumerator ScalePickup()
    {
        transform.localScale += new Vector3(1f, 1f, 1f);
        yield return new WaitForSeconds(3f);
        transform.localScale -= new Vector3(1f, 1f, 1f);
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
            StartCoroutine(WinText());
        }
    }

    IEnumerator WinText()
    {
        winText.text = "You Win!";
        yield return new WaitForSeconds(5f);
        winText.text = "";
    }
}