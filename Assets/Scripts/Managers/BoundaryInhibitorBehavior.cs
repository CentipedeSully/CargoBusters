using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryInhibitorBehavior : MonoBehaviour
{
    //Declarations
    //...


    //monos
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ship"))
            collision.gameObject.GetComponent<SystemDisabler>().StartBoundaryTimer();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ship"))
            collision.gameObject.GetComponent<SystemDisabler>().EndBoundaryTimer();

    }

    //Utilies
    //...


}
