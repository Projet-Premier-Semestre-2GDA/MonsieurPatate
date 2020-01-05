using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour {
    private Image Icon;
    private Image[] IconsArray;

    private void Start() {
        this.Icon = transform.Find("Icon").GetComponent<Image>();
    }

    public void UpdateIcon(int iconIndex) {
        this.Icon.sprite = this.IconsArray[iconIndex].sprite;
    }
    
}
