using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour {


    public bool randomized = false;
    public int randomiseEverySecond = 2;

    private Material objectMaterial;

    private int ranNumber = 1;


    private void Start()
    {
        objectMaterial = gameObject.GetComponent<Renderer>().material;
        if (randomized)
            StartCoroutine(randomiseColor());
    }

    IEnumerator randomiseColor()
    {
        while (true)
        {
            ranNumber = Random.Range(1, 5);

            switch (ranNumber)
            {
                case 1:
                    redChoosed();
                    break;
                case 2:
                    greenChoosed();
                    break;
                case 3:
                    yellowChoosed();
                    break;
                case 4:
                    pinkChoosed();
                    break;
            }
            yield return new WaitForSeconds(randomiseEverySecond);
        }
    }

    void redChoosed()
    {
        objectMaterial.color = new Color(255, 0, 0, .66f);
        gameObject.layer = LayerMask.NameToLayer("RedObject");
    }
    void greenChoosed()
    {
        objectMaterial.color = new Color(0, 255, 0, 0.66f);
        gameObject.layer = LayerMask.NameToLayer("GreenObject");
    }
    void yellowChoosed()
    {
        objectMaterial.color = new Color(255, 237, 0.66f);
        gameObject.layer = LayerMask.NameToLayer("YellowObject");
    }
    void pinkChoosed()
    {
        objectMaterial.color = new Color(255, 0, 216, 0.66f);
        gameObject.layer = LayerMask.NameToLayer("PinkObject");
    }
    


}
