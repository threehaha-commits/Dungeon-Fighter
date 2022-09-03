using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinder
{
    private bool DrawPath = false;
    private List<Vector3Int> Current = new();
    private Queue<Vector3> Path = new();
    private List<Vector3Int> Banned = new();
    private const float Offset = 0.16f;
    private TileBase PickedTile; //Invalid :D
    private Grid Grid; // Grid
    private Tilemap Tile; // Ground
    private int IndexBlock = 0;
    public Transform transform { private get; set; }
    
    public PathFinder(Grid grid, Tilemap tilemap)
    {
        Tile = tilemap;
        Grid = grid;
    }
    
    public Queue<Vector3> FindPath(Vector3 playerPosition)
    {
        Path.Clear();
        Banned.Clear();
        IndexBlock = 100;
        Vector3 currentPosition = transform.position;
        float distance = (playerPosition - currentPosition).sqrMagnitude;
        while (distance > 0.1f)
        {
            if (IndexBlock <= 0)
                return null;
            GetRoundTiles(currentPosition);
            currentPosition = PickTile(playerPosition);
            Path.Enqueue(currentPosition);
            distance = (playerPosition - currentPosition).sqrMagnitude;
            IndexBlock--;
        }
        return Path;
    }

    private void GetRoundTiles(Vector3 pos)
    {
        Current.Clear();
        Banned.Add(Tile.WorldToCell(pos));
        var tiles = new[]
        {
            new Vector3(pos.x + Offset, pos.y, 0), //Right
            new Vector3(pos.x, pos.y + Offset, 0), //Up
            new Vector3(pos.x - Offset, pos.y, 0), //Left
            new Vector3(pos.x, pos.y - Offset, 0) //Down
        };

        for(int i = 0; i < tiles.Length; i++)
        {
            Vector3Int tilePos = Tile.WorldToCell(tiles[i]);
            if (Banned.Contains(tilePos))
                continue;
            if(Tile.GetSprite(tilePos) != null)
                Current.Add(tilePos);
            else
                Banned.Add(tilePos);
        }
    }

    private Vector3 PickTile(Vector3 playerPos)
    {
        float maxDistance = Mathf.Infinity;
        Vector3Int currentTile = Vector3Int.zero;
        Vector3Int playerPosition = Tile.WorldToCell(playerPos);
        for (int i = 0; i < Current.Count; i++)
        {
            float distance = (playerPosition - Current[i]).sqrMagnitude;
            if (distance < maxDistance)
            {
                maxDistance = distance;
                currentTile = Current[i];
            }
        }
        foreach(var v3 in Current)
        {
            if (v3 != currentTile) 
            {
                if (Banned.Contains(v3) == false)
                    Banned.Add(v3);
            }
        }
        if(DrawPath)
            Tile.SetTile(currentTile, PickedTile);
        return Tile.CellToWorld(currentTile);
    }

}
