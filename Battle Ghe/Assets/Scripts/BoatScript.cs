using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoatScript : MonoBehaviour
{
    private GameObject pressedTile;
    private int hitCount = 0;
    public int boatSize;

    public float xOffset = 0;
    public float zOffset = 0;
    public float nextZRotation = 90f;

    private Material[] allMaterials;

    List<Color> allColors = new List<Color>();
    List<GameObject> activeTiles = new List<GameObject>();

    private void Start()
    {
        allMaterials = GetComponent<Renderer>().materials;
        for (int i = 0; i < allMaterials.Length; i++)
            allColors.Add(allMaterials[i].color);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Tiles"))
        {
            activeTiles.Add(collision.gameObject);
        }

    }
    public void ClearTileList()
    {
        activeTiles.Clear();
    }

    public Vector3 GetOffsetVec(Vector3 tilePos)
    {
        return new Vector3(tilePos.x + xOffset, 2, tilePos.z + zOffset);
    }

    public void RotateBoat()
    {
        if (pressedTile == null) return;
        activeTiles.Clear();
        transform.localEulerAngles += new Vector3(0, 0, nextZRotation);
        nextZRotation *= -1;
        float temp = xOffset;
        xOffset = zOffset;
        zOffset = temp;
        SetPosition(pressedTile.transform.position);
    }

    public void SetPosition(Vector3 newVec)
    {
        ClearTileList();
        transform.localPosition = new Vector3(newVec.x + xOffset, 2, newVec.z + zOffset);
    }

    public void SetPressedTile(GameObject tile)
    {
        pressedTile = tile;
    }
    
    public bool OnGameBoard()
    {
        Debug.Log("ActiveTiles: " + activeTiles.Count);
        return activeTiles.Count == boatSize;
    }

    public bool HitCheckSank()
    {
        hitCount++;
        return boatSize <= hitCount;
    }

    public void FlashColor(Color tempColor)
    {
        foreach (Material mat in allMaterials)
        {
            mat.color = tempColor;
        }
        Invoke("ResetColor", 0.5f);
    }

    private void ResetColor()
    {
        int i = 0;
        foreach (Material mat in allMaterials)
        {
            mat.color = allColors[i++];
        }
    }
}
