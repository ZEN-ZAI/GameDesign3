using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelTarget : MonoBehaviour
{
    #region Singleton
    public static PanelTarget instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject panelTarget;
    public Image imageTarget;
    public Image hpBarTarget;
    public TextMeshProUGUI hpTarget;
    public TextMeshProUGUI nameTarget;
    public TextMeshProUGUI atkTarget;
    public TextMeshProUGUI defTarget;

    public DummyCharacter targetCharacter; 

    public void UpdateUI()
    {
        imageTarget.sprite = targetCharacter.GetAttribute.icon;
        hpBarTarget.fillAmount = targetCharacter.GetAttribute.currentHp / targetCharacter.GetAttribute.maxHp;
        hpTarget.text = targetCharacter.GetAttribute.currentHp + "/" + targetCharacter.GetAttribute.maxHp;
        nameTarget.text = targetCharacter.GetAttribute.name;
        atkTarget.text = "Atk: " + targetCharacter.GetAttribute.attack.ToString();
        defTarget.text = "Def: " + targetCharacter.GetAttribute.defense.ToString();
    }

    public void PanelTargetActive(bool boolean)
    {
        panelTarget.SetActive(boolean);
    }



}
