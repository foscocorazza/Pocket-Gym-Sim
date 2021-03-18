using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GymBuilderState {
    Paint, Erase, Play, Move, PaletteMenuOpen
}

public class GymBuilderStateController : MonoBehaviour
{
    public GymBuilderState currentState = GymBuilderState.Paint;

    [Header("Paint Mode")]
    public TilePainter tilePainter;
    public GameObject paintModeButtons;
    public GameObject paintModeButtonPreviewTile;

    [Header("Move Mode")]
    public CameraPanning cameraPanning;

    [Header("UI")]
    public Image primarySelectionImage;
    public Image secondarySelectionImage;

    
    // Options
    public void SetMoveMode(Button sender) {
        SelectOption(sender, GymBuilderState.Move);

        tilePainter.enabled = false;
        tilePainter.ResetLayersOpacities();
        paintModeButtons.SetActive(false);
        cameraPanning.enabled = true;
        
    }

    public void SetPlayMode(Button sender) {
        SelectOption(sender, GymBuilderState.Play);

        tilePainter.enabled = false;
        tilePainter.ResetLayersOpacities();
        paintModeButtons.SetActive(false);
        cameraPanning.enabled = false;
    }

    public void SetEraseMode(Button sender) {
        SelectOption(sender,GymBuilderState.Erase);

        tilePainter.SetEraseMode(true);
        tilePainter.enabled = true;
        tilePainter.SetLayersOpacities();
        paintModeButtons.SetActive(true);
        paintModeButtonPreviewTile.SetActive(false);
        cameraPanning.enabled = false;
    }

    public void SetPaintMode(Button sender) {
        SelectOption(sender, GymBuilderState.Paint);

        tilePainter.SetEraseMode(false);
        tilePainter.enabled = true;
        tilePainter.SetLayersOpacities();
        paintModeButtons.SetActive(true);
        paintModeButtonPreviewTile.SetActive(true);
        cameraPanning.enabled = false;
    }


    // Paint Layers
    public void SetPaintLayerBottomMode(Button sender) {
        SetPaintLayerMode(sender, PaintLayer.Bottom);
    }

    public void SetPaintLayerFloorMode(Button sender) {
        SetPaintLayerMode(sender, PaintLayer.Floor);
    }

    public void SetPaintLayerObjectMode(Button sender) {
        SetPaintLayerMode(sender, PaintLayer.Object);
    }

    private void SetPaintLayerMode(Button sender, PaintLayer paintLayer) {
        SelectButton(sender, secondarySelectionImage);
        tilePainter.paintLayer = paintLayer;
        tilePainter.SetLayersOpacities();
    }


    // Manage UI Selection
    private void SelectOption(Button sender, GymBuilderState state)
    {
        currentState = state;
        SelectButton(sender, primarySelectionImage);
    }

    private void SelectButton(Button sender, Image selectionImage)
    {
        Vector2 selection = sender.GetComponent<Image>().rectTransform.anchoredPosition;
        selectionImage.rectTransform.anchoredPosition = selection;
    }
}
