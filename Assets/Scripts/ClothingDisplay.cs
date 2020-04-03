using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingDisplay : MonoBehaviour
{
    public Clothing clothing;

    // Start is called before the first frame update
    void Start()
    {
        clothing.Print();
    }
}
