using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObject : MonoBehaviour
{
    //Declarations







    //Monobehaviors







    //Utilities
    public void KillSelf()
    {
        //Deactiviate meshes and renderers
        GetComponent<DeactivateCollidersAndMeshes>().DisableMeshRenderersAndCollider();

        //spawn explosion
        GetComponent<SpawnExplosion>().CreateExplosion();

        //DestroySelf
        Destroy(gameObject);
    }




}
