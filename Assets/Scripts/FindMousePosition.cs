using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMousePosition : MonoBehaviour
{
    public Vector2 mouseVector;
    public Vector3 mousePos;
    public Vector3 playerPos;
    Vector3[] positions;
    LineRenderer lr;
    Transform tr;

    private void Start()
    {

    }

    private void Update()
    {
        tr = GetComponent<Transform>();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPos = tr.position;
        //playerPos = playerPos + new Vector3(playerPos.x, playerPos.y, 10);

        lr = GetComponent<LineRenderer>();

        Vector3[] positions = new Vector3[2];
        positions[0] = mousePos;
        positions[1] = playerPos;

        lr.positionCount = positions.Length;
        lr.SetPositions(positions);

        //float dist = Vector3.Distance(mousePos, playerPos);   
        //if (dist > 5)
        //    return;
    }



    public Vector2 VectorToMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseVector = new Vector3(mousePosition.x, mousePosition.y, 0) - transform.position;
        return mouseVector;
    }

}
