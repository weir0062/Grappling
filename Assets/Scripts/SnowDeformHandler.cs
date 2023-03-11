using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowDeformHandler : MonoBehaviour
{

  public RenderTexture target;
  public Material Compositor;
    Vector2 scale;
    Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        scale = new Vector2(1, 1);
        offset = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Graphics.Blit(target,Compositor);
    }
}
