using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class UITileButtonHolder : MonoBehaviour
{
    public TileBase tile;
    public Button button;
    public Image buttonImageOverlay;
    public TilePainter tilePainter;


    void Start() {
        SetTile(tile);
    }

    public void SetTile(TileBase tile) {
        if(tile == null) return;

        this.tile = tile;
        buttonImageOverlay.sprite = tile.GetSpritePreview();
    }

    public void SetImage(Image buttonImageOverlay) {
        this.buttonImageOverlay = buttonImageOverlay;
    }

    public void SetAsPaintingTile() {
        tilePainter.SetTileToPaint(tile);
    }

    public RectTransform GetRectTransform() {
        return buttonImageOverlay.rectTransform;
    }

    internal Button GetButton()
    {
       if(button == null) button = GetComponent<Button>();
       return button;
    }
}
