using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KillObject : MonoBehaviour
{
    //Declarations
    public UnityEvent onDeath;

    [SerializeField] private CommuncateInputToPlayerObject _inputCommunicator;
    [SerializeField] private OldShieldBehaviour _shieldBehavior;




    //Monobehaviors
    private void Awake()
    {
        _inputCommunicator = GetComponent<CommuncateInputToPlayerObject>();
        
    }






    //Utilities
    public void KillSelf()
    {
        if (_inputCommunicator != null)
        {
            //_inputCommunicator.DisableMovement();
            //_inputCommunicator.DisableShooting();
        }

        _shieldBehavior.DisableShields();
        

        //Deactiviate meshes and renderers
        //GetComponent<DeactivateCollidersAndMeshes>().DisableMeshRenderersAndCollider();

        //spawn explosion
        //GetComponent<SpawnExplosion>().CreateExplosion();

        onDeath?.Invoke();

        //DestroySelf
       // Destroy(gameObject);
    }




}
