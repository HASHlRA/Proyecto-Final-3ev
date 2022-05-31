using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCoin : MonoBehaviour
{
    private float upperLim = 30f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Rotar las aspas 
        transform.Rotate(new Vector3(90f, 0f, 0f) * Time.deltaTime);


    }
}
