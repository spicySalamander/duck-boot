using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{

    public float targetTime;
    private bool collided;
    private GameObject duck;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        duck = collision.gameObject;
        collided = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        collided = false;
    }

    private void Update()
    {
        OnUpdate();
    }


    public void OnUpdate()
    {
        if (collided)
        {

            if (Input.GetMouseButtonUp(0) && duck.tag == "Duck")
            {
                Debug.Log("Collding while mouse is up");

                float currCountdownValue;
                StartCoroutine(Timer());
                IEnumerator Timer(float countdownValue = 10)
                {
                    currCountdownValue = countdownValue;
                    while (currCountdownValue > 0)
                    {
                        Debug.Log("Countdown: " + currCountdownValue);
                        duck.GetComponent<SpriteRenderer>().enabled = false;
                        yield return new WaitForSeconds(1.0f);
                        currCountdownValue--;
                    }

                    duck.GetComponent<SpriteRenderer>().enabled = true;
                }

                    Debug.Log(targetTime);
            }
        }
    }


    void timerEnded()
    {
        
    }


}
