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
                // Busca ManaKnight en hijos (más específico que PlayableStatHolder)
                var manaKnight = playerInstance.GetComponentInChildren<ManaKnight>();
                if (manaKnight != null)
                {
                    // Crea instancias de datos y llama a Awake para inicializar valores
                    var manaComponent = new Components.ManaComponent();
                    manaComponent.Awake();
                    var healthComponent = new Components.HealthComponent();
                    healthComponent.Awake();
                    // Asigna prefabs si los tienes (puedes usar los mismos que en el inspector)
                    var explosionVFX = manaKnight.GetType().GetField("explosionVFXPrefab", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(manaKnight) as GameObject;
                    var projectilePrefab = manaKnight.GetType().GetField("projectilePrefab", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(manaKnight) as GameObject;
                    manaKnight.RobustInitialize(manaComponent, healthComponent, explosionVFX, projectilePrefab);
                    manaKnight.InitializeAbilities(healIcon, areaIcon, projectileIcon);
                    Debug.Log($"InstanciadorJugador: Asignando manaKnight a PlayerUI ({manaKnight.gameObject.name})");
                    playerUI.SetStatHolder(manaKnight);
                }
                else
                {
                    // Fallback: busca PlayableStatHolder genérico
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
            }
            else
            {
                Debug.LogWarning("InstanciadorJugador: No se encontró PlayerUI en la escena.");
            }
        }
    }
}
