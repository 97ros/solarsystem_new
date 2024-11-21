using UnityEngine;

public class settinglens : MonoBehaviour
{
    void Start()
    {
        // Imposta la proprietà globale per ignorare il blocco dei trigger da parte dei lens flares
        Physics.queriesHitTriggers = false;
    }
}
