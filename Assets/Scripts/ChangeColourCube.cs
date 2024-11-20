using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColourCube : MonoBehaviour
{
    public Material enterColor;
    public Material exitColor;

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Renderer>().material = enterColor;
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<Renderer>().material = exitColor;
    }
}


