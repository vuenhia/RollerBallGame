using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuiHudController : MonoBehaviour
{
    // Timer gui text
    [SerializeField]
    private TextMeshProUGUI timerGUIText = null;

    // Game timer
    [SerializeField]
    private Timer gameTimer = null;

    [SerializeField]
    private RectTransform inGameMenuPnl = null;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inGameMenuPnl.gameObject.SetActive(false);
        timerGUIText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("InGameMenu"))
        {
            if (inGameMenuPnl.gameObject.activeSelf)
            {
                // Turn Off
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
                inGameMenuPnl.gameObject.SetActive(false);

            }
            else
            {
                // Turn On
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
                inGameMenuPnl.gameObject.SetActive(true);
            }
        }
    }
    private void OnGUI()
    {
        if (gameTimer.isRunning())
        {
            timerGUIText.text = "" + Mathf.Round(gameTimer.getTimerTime() * 100) / 100.0f;
        }
    }
}
