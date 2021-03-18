using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GymBuilderTilesMenuBuilder : MonoBehaviour
{

    public List<TileBase> tiles;    
    public int columns;
    public int padding = 2;
    public int pixelWidth = 16;

    private UITileButtonHolder prototypeButtonHolder;  

    void Start()
    {
        prototypeButtonHolder = GetComponentInChildren<UITileButtonHolder>();
        Build();
    }

    public void Build() {
        int currentRow = 0;
        int currentColumn = 0;
        Vector2 ogAnchor = prototypeButtonHolder.GetRectTransform().anchoredPosition;
        Transform ogTranform = prototypeButtonHolder.transform;
        foreach(TileBase tile in tiles) {
            UITileButtonHolder obj = prototypeButtonHolder;
            if(currentRow != 0 || currentColumn != 0) {
                // Generate new Object
                obj = Instantiate(obj, ogTranform.position, Quaternion.identity, ogTranform.parent);
                
                // Position
                Vector2 offset = new Vector2(currentRow, -currentColumn) * (padding+pixelWidth);
                obj.GetRectTransform().anchoredPosition = ogAnchor + offset;
            }

            // Tile
            obj.SetTile(tile);

            // Close on tap
            obj.GetButton().onClick.AddListener(CloseMenu);

            // Increase index
            currentRow += 1;
            if(currentRow > columns) {
                currentColumn += 1;
                currentRow = 0;
            }


        }
    }


    public void OpenMenu() {
        gameObject.SetActive(true);
    }

    public void CloseMenu() {
        gameObject.SetActive(false);
    }

}
