using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button myButton; // Referencia al bot�n

    void Start()
    {
        myButton.onClick.AddListener(OnButtonClick); // Asignar evento
    }

    void OnButtonClick()
    {
        Debug.Log("�Bot�n presionado!"); // Acci�n al hacer clic
    }
}
