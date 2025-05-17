using UnityEngine;
using Holders;

public class InstanciadorJugador : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject[] jugadorPrefabs;
    [SerializeField] private Sprite healIcon;
    [SerializeField] private Sprite areaIcon;
    [SerializeField] private Sprite projectileIcon;

    void Start()
    {
        Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : Vector3.zero;
        Quaternion spawnRot = spawnPoint != null ? spawnPoint.rotation : Quaternion.identity;

        int seleccion = PlayerPrefs.GetInt("SeleccionJugador", -1);
        GameObject playerInstance = null;

        if (seleccion >= 0 && seleccion < jugadorPrefabs.Length)
        {
            var prefab = jugadorPrefabs[seleccion];
            if (prefab != null)
            {
                playerInstance = Instantiate(prefab, spawnPos, spawnRot);
                if (playerInstance == null)
                    Debug.LogError("InstanciadorJugador: Falló la instanciación del jugador.");
            }
            else
            {
                Debug.LogError($"InstanciadorJugador: El prefab para la selección {seleccion} es nulo.");
            }
        }
        else
        {
            Debug.LogError($"InstanciadorJugador: Valor de selección inválido ({seleccion}). Verifica que la selección se haya guardado correctamente y que los prefabs estén asignados en InstanciadorJugador.");
        }

        // Asigna el jugador a la UI fija en la escena
        if (playerInstance != null)
        {
            var playerUI = Object.FindFirstObjectByType<PlayerUI>();
            if (playerUI != null)
            {
                // Busca el statHolder en hijos también
                var statHolder = playerInstance.GetComponentInChildren<PlayableStatHolder>();
                if (statHolder != null)
                {
                    statHolder.InitializeAbilities(healIcon, areaIcon, projectileIcon);
                    Debug.Log($"InstanciadorJugador: Asignando statHolder a PlayerUI ({statHolder.gameObject.name})");
                    playerUI.SetStatHolder(statHolder);
                }
                else
                {
                    Debug.LogWarning("InstanciadorJugador: No se encontró un componente de tipo PlayableStatHolder en el jugador instanciado.");
                }
            }
            else
            {
                Debug.LogWarning("InstanciadorJugador: No se encontró PlayerUI en la escena.");
            }
        }
    }
}
