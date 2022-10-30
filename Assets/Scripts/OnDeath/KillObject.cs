using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KillObject : MonoBehaviour
{
    //Declarations
    public UnityEvent onDeath;






    //Monobehaviors







    //Utilities
    public void KillSelf()
    {
        //Deactiviate meshes and renderers
        GetComponent<DeactivateCollidersAndMeshes>().DisableMeshRenderersAndCollider();

        //spawn explosion
        GetComponent<SpawnExplosion>().CreateExplosion();

        onDeath?.Invoke();

        //DestroySelf
        Destroy(gameObject);
    }




}
