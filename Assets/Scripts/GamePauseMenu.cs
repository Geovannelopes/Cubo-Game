using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseMenu : MonoBehaviour
{
    private LTDescr restartAnimation;

    [SerializeField]
    private TMPro.TextMeshProUGUI highScore;

    public void Restart()
    {
        restartAnimation.pause();
        gameObject.SetActive(false);

        GameManager.Instance.Enable();
    }
}