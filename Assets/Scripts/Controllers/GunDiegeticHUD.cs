using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunDiegeticHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text m_guiMovesCounter;
    [SerializeField] private PlayerState so_playerState;
    // Start is called before the first frame update
    void Start()
    {
        m_guiMovesCounter.text = "x" + so_playerState.CurrentScaleFactor;

    }

    // Update is called once per frame
    void Update()
    {
        m_guiMovesCounter.text = "x" + so_playerState.CurrentScaleFactor;
    }
}
