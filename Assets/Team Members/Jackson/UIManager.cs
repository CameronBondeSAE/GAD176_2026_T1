using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private AIPlayerSense aiPlayerSense;
    public EnemySense[] enemySenses;
    [SerializeField] private GameObject uiOverlay;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject gameOverScreen;
    private bool winScreenActive = false;

    private void Start()
    {
        aiPlayerSense = FindFirstObjectByType<AIPlayerSense>();
    }
    
    void Update()
    {
        enemySenses = FindObjectsByType<EnemySense>(FindObjectsSortMode.None);
        
        if (aiPlayerSense.pointsEarned == 5)
        {
            uiOverlay.SetActive(false);
            winScreen.SetActive(true);
            winScreenActive = true;
        }

        foreach (EnemySense enemySense in enemySenses)
        {
            if (!winScreenActive && enemySense.touchedPlayer)
            {
                uiOverlay.SetActive(false);
                gameOverScreen.SetActive(true);
                break;
            }
        }
    }
}
