using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CenterSlotScript : SlotScript {
    [SerializeField] private Image groupIcon;
    public Sprite[] GroupIconsArray;

    private void Start() {
        this.Icon = transform.Find("LimbIcon").GetComponent<Image>();
        this.groupIcon = transform.Find("GroupIcon").GetComponent<Image>();
    }
    
    public void UpdateIconGroup(int iconIndex) {
        this.groupIcon.sprite = this.GroupIconsArray[iconIndex];
    }
}
