using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public enum PaintLayer  {
    Bottom, Floor, Object
}

public class TilePainter : MonoBehaviour
{
    public TileBase tileToPaint;
    public Tilemap bottomMap;
    public Tilemap floorMap;
    public Tilemap objectMap;

    public PaintLayer paintLayer;
    public bool eraseMode;

    public Image uiIndicator;

    private Dictionary<PaintLayer, Tilemap> maps = new Dictionary<PaintLayer, Tilemap>();

    void Start() {
         Cursor.visible = false;
         maps.Add(PaintLayer.Bottom, bottomMap);
         maps.Add(PaintLayer.Floor, floorMap);
         maps.Add(PaintLayer.Object, objectMap);
        SetLayersOpacities();
    }
 
    void Update()
    {
        if (Input.GetMouseButton(0) && !IsPointerOverUIObject()) {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coordinate = GetMap().WorldToCell(mouseWorldPos);
            
            GetMap().SetTile(coordinate, eraseMode ? null : tileToPaint);
        }
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private Tilemap GetMap()
    {
        Tilemap map;
        maps.TryGetValue(paintLayer, out map);
        return map;
    }

    internal void SetEraseMode(bool v)
    {
        eraseMode = v;
    }

    public void SetTileToPaint(TileBase tile) {
        this.tileToPaint = tile;

        if(uiIndicator != null && tile != null) 
            uiIndicator.sprite = tile.GetSpritePreview();
    }


    internal void SetAllLayersOpacities(float opacity)
    {
        bottomMap.color = new Color(1,1,1, opacity);
        floorMap.color = new Color(1,1,1, opacity);
        objectMap.color = new Color(1,1,1, opacity);
    }

    internal void ResetLayersOpacities()
    {
       SetAllLayersOpacities(1f);
    }

    internal void SetLayersOpacities()
    {
        SetAllLayersOpacities(0.5f);
        GetMap().color = Color.white;
    }
}


// TODO: Own class, split by classess ecc ecc
public static class MyExtensions
{
    public static Sprite GetSpritePreview(this TileBase tile)
    {
        if(tile is Tile) {
            return ((Tile)tile).sprite;
        }

        if(tile is AnimatedTile) {
            return ((AnimatedTile)tile).m_AnimatedSprites[0];
        }

        return null;
    }
}
