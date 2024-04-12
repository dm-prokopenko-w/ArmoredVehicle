using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var rot = Random.Range(0, 359);
        transform.Rotate(0, rot, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }
}