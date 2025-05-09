using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button myButton; // Referencia al botón

    void Start()
    {
        myButton.onClick.AddListener(OnButtonClick); // Asignar evento
    }

    void OnButtonClick()
    {
        Debug.Log("¡Botón presionado!"); // Acción al hacer clic
    }
}
