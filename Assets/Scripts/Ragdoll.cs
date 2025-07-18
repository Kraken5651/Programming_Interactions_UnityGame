using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Animator animator;
    public CapsuleCollider capsuleCollider;


    public List<Rigidbody> rigidbodies = new List<Rigidbody>();
    private List<Collider> colliders = new List<Collider>();


    public bool isRagdollActive = false;
    private AudioSource audioSource;

    private void Awake()
    {
        TryGetComponent<Animator>(out animator);
        TryGetComponent<CapsuleCollider>(out capsuleCollider);
        TryGetComponent<AudioSource>(out audioSource);

        if (animator == null)
        {
            return;
        }

        GetComponentsInChildren(rigidbodies);
        GetComponentsInChildren(colliders);

        for (int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].isKinematic = true;
            colliders[i].isTrigger = true;
        }

        capsuleCollider.isTrigger = false;
    }

    
    public void EnableRagdoll()
    {
        audioSource.Play();

        if(isRagdollActive)
        {
            return;
        }

        isRagdollActive = !isRagdollActive;

        for (int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].isKinematic = false;
            rigidbodies[i].linearVelocity = Vector3.zero;
            colliders[i].isTrigger = false;
        }
        capsuleCollider.isTrigger = true;
        animator.enabled = false;
    }
}
