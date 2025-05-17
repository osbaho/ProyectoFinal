using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using System.IO;

/// <summary>
/// SeleccionJugador
/// Clase encargada de gestionar la selección de jugadores.
/// Permite al usuario elegir entre dos prefabs de jugador.
/// </summary>

public class SeleccionJugador : MonoBehaviour
{
    [SerializeField] private GameObject jugador1Prefab; // Prefab del jugador 1
    [SerializeField] private GameObject jugador2Prefab; // Prefab del jugador 2
    public static GameObject jugadorSeleccionado { get; private set; } // Referencia al jugador seleccionado

    private void SeleccionarJugador1()
    {
        PlayerPrefs.SetInt("SeleccionJugador", 0); // 0 para el primer prefab
        PlayerPrefs.Save();
        CargarJuego();
    }

    private void SeleccionarJugador2()
    {
        PlayerPrefs.SetInt("SeleccionJugador", 1); // 1 para el segundo prefab
        PlayerPrefs.Save();
        CargarJuego();
    }

    private void CargarJuego()
    {
        // Carga la siguiente escena del juego
        SceneManager.LoadScene("Health");
    }
}
