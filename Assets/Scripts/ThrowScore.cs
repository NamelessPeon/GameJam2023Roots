using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;

public class ThrowScore : MonoBehaviour
{
    public TMP_Text score;

    public void UpdateScore(int new_score)
    {
        score.text = new_score.ToString();
    }
}
