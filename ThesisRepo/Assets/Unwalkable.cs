using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unwalkable : MonoBehaviour
{
    // Start is called before the first frame update

    public bool invisAtStart;
    public bool ChangeMaterial;
    public Material MaterialToChangeTo;

    public Renderer tile;
    void Start()
    {
        gameObject.tag = "Unwalkable";

        if (invisAtStart == true)
        {
            tile.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }

        if (ChangeMaterial == true)
        {
            tile.material = MaterialToChangeTo;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
