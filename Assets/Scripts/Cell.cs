

using System;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {
    public enum SetCellValue
    {
        NONE,
        CROSS,
        NOUGHT
    }

    public Image Image;
    public Button Button;
    public SetCellValue Value = SetCellValue.NONE;

    internal void SetCellImage(Sprite currentCellSprite, SetCellValue currentValue)
    {
         if(Image.enabled)
        {
            return;
        }
        Image.sprite = currentCellSprite;
        Image.enabled = true;
        Value = currentValue;
        Button.enabled = false;
    }
}
