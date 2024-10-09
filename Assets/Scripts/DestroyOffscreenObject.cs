using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DestroyOffscreenObject : MonoBehaviour
{

    void Update()
    {
        Vector2 posi = gameObject.transform.position;
        if (posi.y < -8 || posi.y > 8) {
            Destroy(this.gameObject);
        }
    }
}
