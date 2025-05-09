using UnityEngine;

public class AnimatedTorchLightWithCurve : MonoBehaviour
{
    [Header("Referencia a la luz")]
    public Light torchLight; // Asigna el componente Light desde el Inspector o se obtiene usando GetComponent<Light>()

    [Header("Parpadeo (Flicker) de Intensidad")]
    public float flickerSpeed = 2f;        // Velocidad del parpadeo
    public float flickerIntensity = 0.5f;  // Magnitud del parpadeo
    public float baseIntensity = 3.0f;     // Intensidad base de la luz

    [Header("Animación de Color con Curve")]
    public Color coolColor = new Color(1.0f, 0.55f, 0.1f); // Estado "frío" anaranjado
    public Color hotColor  = new Color(1.0f, 0.9f, 0.6f);   // Estado "caliente" amarillento/blanquecino
    // Define la curva de transición; por defecto, una curva suave de 0 a 1.
    public AnimationCurve colorTransitionCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public float colorAnimationDuration = 2.0f; // Duración de un ciclo completo de la transición de color en segundos

    void Start()
    {
        // Se obtiene el componente Light si no fue asignado explícitamente
        if (torchLight == null)
            torchLight = GetComponent<Light>();

        torchLight.intensity = baseIntensity;
        torchLight.color = coolColor;
    }

    void Update()
    {
        // Parpadeo de intensidad: usamos Perlin Noise para suavidad en la fluctuación
        float intensityNoise = Mathf.PerlinNoise(Time.time * flickerSpeed, 0.0f);
        torchLight.intensity = baseIntensity + intensityNoise * flickerIntensity;
        
        // Transición de color basada en AnimationCurve:
        // Calculamos un tiempo normalizado que se reinicia cada ciclo, de 0 a 1.
        float normalizedTime = Mathf.Repeat(Time.time, colorAnimationDuration) / colorAnimationDuration;
        // Evaluamos la curva para obtener un valor interpolado.
        float t = colorTransitionCurve.Evaluate(normalizedTime);
        // Interpolamos del color "frío" al "caliente" según el valor t.
        torchLight.color = Color.Lerp(coolColor, hotColor, t);
    }
}