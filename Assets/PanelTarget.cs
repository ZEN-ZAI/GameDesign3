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
    public Slider hpBarTarget;
    public TextMeshProUGUI hpTarget;
    public TextMeshProUGUI nameTarget;
    public TextMeshProUGUI atkTarget;
    public TextMeshProUGUI defTarget;

    public DummyCharacter targetCharacter; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetCharacter != null)
        {
            panelTarget.SetActive(true);

            imageTarget.sprite = targetCharacter.GetAttribute.icon;
            hpBarTarget.value = targetCharacter.GetAttribute.currentHp / targetCharacter.GetAttribute.maxHp;
            hpTarget.text = targetCharacter.GetAttribute.currentHp +"/"+ targetCharacter.GetAttribute.maxHp;
            nameTarget.text = targetCharacter.GetAttribute.name;
            atkTarget.text = "Atk: " + targetCharacter.GetAttribute.attack.ToString();
            defTarget.text = "Def: "+targetCharacter.GetAttribute.defense.ToString();
        }
        else
        {
            panelTarget.SetActive(false);
        }
    }


}
