using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Time, Health, Mp}
    public InfoType type;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch (type) {
            case InfoType.Exp:
            float curExp = GameManager.Instance.exp;
            float maxExp = GameManager.Instance.nextExp[GameManager.Instance.level];
            mySlider.value = curExp / maxExp;
            break;
            
            case InfoType.Level:
            myText.text = string.Format("Lv.{0:F0}", GameManager.Instance.level);
            break;
            
            case InfoType.Time:
            break;
            
            case InfoType.Health:
            float curHealth = GameManager.Instance.curHealth;
            float maxHealth = GameManager.Instance.maxHealth;
            mySlider.value = curHealth / maxHealth;
            break;

            case InfoType.Mp:
            float curMp = GameManager.Instance.curMp;
            float maxMp = GameManager.Instance.maxMp;
            mySlider.value = curMp / maxMp;
            break;
        }
        
    }
}
