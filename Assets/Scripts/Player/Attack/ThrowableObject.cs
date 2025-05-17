using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    [SerializeField] private float destroyDistance = 5f; // Distancia límite para destruir
    [SerializeField] private GameObject destructionEffect; // Prefab de efecto de destrucción
    [SerializeField] private Vector3 angularVelocity = new Vector3(0, 720f, 0); // grados por segundo

    private Vector3 startPosition;
    private Rigidbody rb;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogWarning("El objeto necesita un Rigidbody.");
        }
        else
        {
            rb.angularVelocity = angularVelocity * Mathf.Deg2Rad; // Convierte a radianes por segundo
        }
    }

    void Update()
    {
        // Verificar si ha alcanzado la distancia límite
        if (Vector3.Distance(startPosition, transform.position) >= destroyDistance)
        {
            TriggerDestructionEffect();
        }
    }

    public void SetVelocity(Vector3 velocity)
    {
        if (rb != null)
            rb.linearVelocity = velocity;
    }

    void TriggerDestructionEffect()
    {
        if (destructionEffect != null)
        {
            GameObject effectInstance = Instantiate(destructionEffect, transform.position, Quaternion.identity);
            Destroy(effectInstance, 2f);
        }
        Destroy(gameObject);
    }
}
