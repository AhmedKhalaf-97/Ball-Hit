using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Player player;
    public Text distanceLabel, velocityLabel;

    public GameObject playInstructionsPanel;
    
    void OnEnable()
    {
        player.UpdateAllInfoUI();
        playInstructionsPanel.SetActive(true);
    }

    public void SetValues(float distanceTraveled, float velocity)
    {
        distanceLabel.text = string.Format("{0:0,0}", ((int)(distanceTraveled * 10f)));
        velocityLabel.text = ((int)(velocity * 10f)).ToString();
    }
}