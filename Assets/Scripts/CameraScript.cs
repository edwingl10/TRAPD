using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public SpriteRenderer background;

    void Start()
    {
        float orthoSize = background.size.x * Screen.height / Screen.width * 0.42f;
        Camera.main.orthographicSize = orthoSize;

    }

}
