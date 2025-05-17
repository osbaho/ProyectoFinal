using UnityEngine;
using UnityEngine.InputSystem; // Asegúrate de importar Input System

public class ObjectLauncher : MonoBehaviour
{
    [SerializeField] private GameObject throwablePrefab; // Prefab del objeto a lanzar
    [SerializeField] private Transform spawnPoint; // Punto desde donde se lanzará

    void LaunchObject()
    {
        if (throwablePrefab != null && spawnPoint != null)
        {
            // Instanciar el objeto en el punto de lanzamiento
            GameObject throwableInstance = Instantiate(throwablePrefab, spawnPoint.position, spawnPoint.rotation);

            // Obtener el Rigidbody del objeto instanciado
            Rigidbody rb = throwableInstance.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearVelocity = spawnPoint.forward * 10f; // Aplicar velocidad de lanzamiento
            }
            else
            {
                Debug.LogWarning("El objeto instanciado no tiene un Rigidbody.");
            }
        }
        else
        {
            Debug.LogWarning("ObjectLauncher: Faltan referencias en el Inspector.");
        }
    }
}