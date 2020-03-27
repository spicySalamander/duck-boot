using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTester : MonoBehaviour
{
    public MotorController obj;
    public bool test;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            //obj.MoveToPoint(transform.position);
            obj.MoveToTarget(gameObject, 5f);
            test = !test;
        }
    }


}
