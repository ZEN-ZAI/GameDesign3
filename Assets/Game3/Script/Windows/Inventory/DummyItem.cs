using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyItem : MonoBehaviour
{
    #region Singleton
    public static DummyItem instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    //public Image icon;
    //public Button removeButton;
    //public Character character;

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetEnable(Image icon)
    {
        transform.GetComponent<RectTransform>().GetChild(0).GetComponent<Image>().enabled = true;
        transform.GetComponent<RectTransform>().GetChild(0).GetComponent<Image>().sprite = icon.sprite;
    }

    public void SetDisable()
    {
        transform.GetComponent<RectTransform>().GetChild(0).GetComponent<Image>().enabled = false;
    }
}
