using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    private float scroll = 0.55f;
    private float dropPoint = -80f;

    void Update()
    {
        if (GameController.isPlaying == true)
        {
            transform.Translate(Vector2.left * scroll);
        }

        if (transform.position.x <= dropPoint) {
            Destroy(gameObject);
        }
    }
}
