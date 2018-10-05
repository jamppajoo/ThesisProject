using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerCollider : MonoBehaviour
{

    private BallChangeColor ballChangeColor;

    void Awake()
    {
        ballChangeColor = gameObject.transform.parent.GetComponent<BallChangeColor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ballChangeColor.gameObject.layer == collision.gameObject.layer)
            ballChangeColor.TogglePlayerInsideObject(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ballChangeColor.gameObject.layer == collision.gameObject.layer)
            ballChangeColor.TogglePlayerInsideObject(false);

    }


}
