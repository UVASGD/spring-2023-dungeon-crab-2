using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialTiler : MonoBehaviour
{
    // Start is called before the first frame update

    public float tileX = 20;
    public float tileY = 2000;
    Mesh mesh;
    private Material mat;
 
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Update is called once per frame
    void Update()
    {
        mat.mainTextureScale = new Vector2((mesh.bounds.size.x * transform.localScale.x) / 100 * tileX, (mesh.bounds.size.y * transform.localScale.y) / 100 * tileY);
    }
}
