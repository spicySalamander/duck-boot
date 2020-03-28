using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    FindMousePosition MouseComp;
    Vector3 movementDirection;
    public float speed = 1;
    bool moving;


    // Start is called before the first frame update
    void Start()
    {
        MouseComp = GetComponent<FindMousePosition>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && moving == false)
        {
            moving = true;
            movementDirection = MouseComp.VectorToMouse();
        }
    }

    private void FixedUpdate()
    {
        if (moving == true)
        {
            transform.position += movementDirection.normalized * speed * Time.deltaTime;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)      //Timer will disable sprite renderer for its duration
    {
        if (collision.gameObject.tag == "Wall")
        {
            moving = false;

            float currCountdownValue;
            StartCoroutine(Timer());
            IEnumerator Timer(float countdownValue = 10)
            {
                currCountdownValue = countdownValue;
                while (currCountdownValue > 0)
                {
                    Debug.Log("Countdown: " + currCountdownValue);
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    yield return new WaitForSeconds(1.0f);
                    currCountdownValue--;
                }

                gameObject.GetComponent<SpriteRenderer>().enabled = true;

            }
        }
    }

    //IEnumerator Timer()
    //{
    //    if (moving == false)
    //    {
    //        Debug.Log("Started Coroutine at timestamp : " + Time.time);
    //        yield return new WaitForSeconds(5);
    //        Debug.Log("Ended Coroutine at timestamp : " + Time.time);
    //    }
    //}
}
