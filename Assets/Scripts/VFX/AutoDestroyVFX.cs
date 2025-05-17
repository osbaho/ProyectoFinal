using UnityEngine;

public class AutoDestroyVFX : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 1.5f;

    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
