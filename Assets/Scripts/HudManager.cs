using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudManager : MonoBehaviour
{
    public TMP_Text scoreLabel;
    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        scoreLabel.text = "Coins: " + GameManager.instance.score;
    }
}
