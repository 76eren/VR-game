using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materialhandler : MonoBehaviour
{
    public Material originalMaterial;
    public Material highlightedMaterial;

    public void setHighlightedMaterial()
    {
        GetComponent<Renderer>().material = highlightedMaterial;
    }

    public void setOriginalMaterial()
    {
        GetComponent<Renderer>().material = originalMaterial;
    }
}
