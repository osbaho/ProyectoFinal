using UnityEngine;
using Interfaces;

public class ResourceArea : MonoBehaviour
{
    public int resourceAmount = 20;

    private void OnTriggerEnter(Collider other)
    {
        var resourceUser = other.GetComponent<IResourceUser>();
        if (resourceUser != null)
        {
            resourceUser.RecoverResource(resourceAmount);
        }
    }
}
