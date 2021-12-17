using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSwitcherUIManager : MonoBehaviour
{
    public Button[] itemsButtons;
    public GameObject[] itemsPanels;

    public Player player;

    [Header("Store Manager Unlimited Balls Button")]
    public Button unlimitedBallsButton;

    int buttonPressedInt = 0;

    void OnEnable()
    {
        ButtonPressed(buttonPressedInt);

        player.UpdateAllInfoUI();
    }

    public void ButtonPressed(int buttonIndex)
    {
        for (int i = 0; i < itemsButtons.Length; i++)
        {
            ColorBlock itemButtonColor = itemsButtons[i].colors;
            if (i == buttonIndex)
            {
                itemButtonColor.normalColor = Color.white;
                itemsButtons[i].colors = itemButtonColor;
                itemsPanels[i].SetActive(true);
            }
            else
            {
                itemButtonColor.normalColor = Color.grey;
                itemsButtons[i].colors = itemButtonColor;
                itemsPanels[i].SetActive(false);
            }
        }
    }

    public void CheckIfUnlimitedBalls()
    {
        buttonPressedInt = 1;

        itemsButtons[0].transform.parent.GetChild(1).gameObject.SetActive(false);
        itemsButtons[0].gameObject.SetActive(false);

        unlimitedBallsButton.gameObject.SetActive(false);
    }
}
