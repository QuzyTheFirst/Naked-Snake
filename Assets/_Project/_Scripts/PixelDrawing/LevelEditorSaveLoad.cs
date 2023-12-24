using System;
using SFB;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorSaveLoad
{
    private Grid<LevelEditorGrid.LevelEditorGridObject> _grid;

    public void SetGrid(Grid<LevelEditorGrid.LevelEditorGridObject> grid)
    {
        _grid = grid;
    }

    public void Save()
    {
        string path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "MyMap", "png");

        if (String.IsNullOrEmpty(path))
            throw new Exception("Path is empty!");
        
        Texture2D texture2D = GetTexture2D(false);
        texture2D.Apply();
        byte[] byteArray = texture2D.EncodeToPNG();
        System.IO.File.WriteAllBytes(path, byteArray);
    }

    public Texture2D Load()
    {
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "png", false);

        if (paths.Length == 0)
            throw new Exception("Path is empty!");
        
        if (String.IsNullOrEmpty(paths[0]))
            throw new Exception("Path is empty!");
        
        Texture2D texture2D = new Texture2D(1, 1, TextureFormat.ARGB32, false);

        byte[] byteArray = System.IO.File.ReadAllBytes(paths[0]);
        texture2D.LoadImage(byteArray);

        if (texture2D.width != 20 || texture2D.height != 20)
        {
            throw new Exception("Texture has wrong width or height! (Texture should be 20x20)");
        }
        
        return texture2D;
    }
    
    public Texture2D GetTexture2D(bool cropTransparentPixels = false)
    {
        Texture2D texture2D = new Texture2D(_grid.GetWidth(), _grid.GetHeight(), TextureFormat.ARGB32, false);
        for(int x = 0; x < _grid.GetWidth(); x++)
        {
            for(int y = 0; y < _grid.GetHeight(); y++)
            {
                LevelEditorGrid.LevelEditorGridObject levelEditorGridObject = _grid.GetGridObject(x, y);
                switch (levelEditorGridObject.GetTileType())
                {
                    case LevelEditorGrid.LevelEditorGridObject.TileType.Empty:
                        texture2D.SetPixel(x, y, Color.clear);
                        break;
                    case LevelEditorGrid.LevelEditorGridObject.TileType.Obstacle:
                        texture2D.SetPixel(x, y, Color.red);
                        break;
                    case LevelEditorGrid.LevelEditorGridObject.TileType.Spawnpoint:
                        texture2D.SetPixel(x, y, Color.green);
                        break;
                    case LevelEditorGrid.LevelEditorGridObject.TileType.Walkable:
                        texture2D.SetPixel(x, y, Color.white);
                        break;
                }
            }
        }

        if (cropTransparentPixels)
        {
            texture2D = CropTransparentPartFromTexture(texture2D);
        }
        
        return texture2D;
    }

    private Texture2D CropTransparentPartFromTexture(Texture2D texture)
    {
        int width = -1, height = -1;
        int startingX = texture.width, endingX = 0;
        int startingY = texture.height, endingY = 0;

        for (int x = 0; x < _grid.GetWidth(); x++)
        {
            for (int y = 0; y < _grid.GetHeight(); y++)
            {
                Color color = texture.GetPixel(x, y);
                if(color == Color.clear)
                    continue;
                if (x < startingX)
                    startingX = x;
                if (x > endingX)
                    endingX = x;
                if (y < startingY)
                    startingY = y;
                if (y > endingY)
                    endingY = y;
            }
        }

        if (startingX == texture.width || endingX == 0 || startingY == texture.height || endingY == 0)
            throw new Exception("Couldn't find any drawings!");
        
        if (endingX < texture.width)
            endingX++;
        if (endingY < texture.height)
            endingY++;
        
        width = endingX - startingX;
        height = endingY - startingY;
        
        Color[] colors = texture.GetPixels(startingX, startingY, width, height);

        Texture2D outputTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);
        outputTexture.SetPixels(colors);
        return outputTexture;
    }
}