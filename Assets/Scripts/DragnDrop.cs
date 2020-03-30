using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragnDrop : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;

    // Update is called once per frame
    void Update()
    {
        if(isBeingHeld == true)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {

            // Vector 3 to hold the X and Y value of the mouse
            Vector3 mousePos;
            //Getting the mouse position
            mousePos = Input.mousePosition;
            //Convert the screen point of the mouse to the world point (the in game point)
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            
            //locks to mouse position instead of snapping the middle of the object to the mouse 
            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            isBeingHeld = true;
        }
    }

    private void OnMouseUp()
    {

        isBeingHeld = false;

        //snaps to default position when mouse button is lifted
        this.gameObject.transform.localPosition = new Vector3(0, 0, 0);

    }
}
