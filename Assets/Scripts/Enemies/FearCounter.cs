using UnityEngine;
using TMPro; // Necesario para manejar TextMeshPro

public class FearCounter : MonoBehaviour
{
    [Header("Referencias")]
    public TMP_Text textoContador;

    [Header("Configuración")]
    public float currentFear = 0f;
    public float fearMultiplier = 1f;

    void Update()
    {
        // 1. Aumentar el contador
        currentFear += Time.deltaTime * fearMultiplier;

        // 2. Lógica de conversión por divisiones
        ActualizarInterfaz();
    }

    void ActualizarInterfaz()
    {
        if (textoContador != null)
        {
            // Sacamos los minutos (división entera)
            int minutos = Mathf.FloorToInt(currentFear / 60);

            // Sacamos el sobrante (módulo o resto) para los segundos
            // Usamos Mathf.RoundToInt para el redondeo que pediste
            float residuo = currentFear % 60;
            int segundos = Mathf.RoundToInt(residuo);

            // Ajuste por si el redondeo de segundos llega a 60
            if (segundos == 60)
            {
                minutos++;
                segundos = 0;
            }

            // 3. Unir como cadena en formato 00:00
            // el ":D2" asegura que siempre tenga dos dígitos (ej: 05 en vez de 5)
            textoContador.text = minutos.ToString("00") + ":" + segundos.ToString("00");
        }
    }
}