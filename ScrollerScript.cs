using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollerScript : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mt = mr.material;
        Vector2 offset = mt.mainTextureOffset;

        offset.y -= Time.deltaTime * speed;
        mt.mainTextureOffset = offset;
    }
}
