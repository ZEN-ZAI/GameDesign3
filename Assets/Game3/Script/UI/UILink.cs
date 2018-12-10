using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILink : MonoBehaviour
{
    public TMP_Text stateGameText;
    public TMP_Text statePlayerText;

    public Canvas mainCanvas;
    public GameObject inventoryPanel;

    public static UILink instance;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        stateGameText.text = "Game State: " + GameSystem.instance.active.Method.ToString().Substring(5);
        statePlayerText.text = "Player State: " + PlayerController.instance.active.Method.ToString().Substring(5);

        // Inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryPanel.activeInHierarchy)
            {
                OpenInventory();
            }
            else if (inventoryPanel.activeInHierarchy)
            {
                CloseInventory();
            }
        }

        // reset UI
        if (Input.GetKey(KeyCode.U))
        {
            MoveInventoryUI();
        }
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
    }

    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
    }

    public void MoveInventoryUI()
    {
        // inventoryPanel.GetComponent<RectTransform>().position = Input.mousePosition;

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvas.transform as RectTransform, Input.mousePosition, mainCanvas.worldCamera, out pos);
        inventoryPanel.GetComponent<RectTransform>().position = mainCanvas.transform.TransformPoint(pos);

    }



}
