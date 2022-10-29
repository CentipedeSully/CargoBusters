using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateCollidersAndMeshes : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> _meshRenderers;
    [SerializeField] private Collider2D _collider;

    private void Awake()
    {
        GetMeshRenderersOnGameObjectAndChildren();
        GetCollider2D();
    }


    private void GetMeshRenderersOnGameObjectAndChildren()
    {
       // _meshRenderers.Add(GetComponent<MeshRenderer>());
        MeshRenderer[] childMeshRenderers = GetComponentsInChildren<MeshRenderer>();

        int i = 0;
        while (i < childMeshRenderers.Length)
        {
            _meshRenderers.Add(childMeshRenderers[i]);
            i++;
        }
    }

    private void GetCollider2D()
    {
        _collider = GetComponent<Collider2D>();
    }


    public void DisableMeshRenderersAndCollider()
    {
        foreach (MeshRenderer renderer in _meshRenderers)
            renderer.enabled = false;

        _collider.enabled = false;
    }

    public void EnableMeshRenderersAndCollider()
    {
        foreach (MeshRenderer renderer in _meshRenderers)
            renderer.enabled = true;

        _collider.enabled = true;
    }
}
