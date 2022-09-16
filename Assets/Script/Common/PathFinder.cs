using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinder
{
    private bool DrawPath;
    private List<Vector3Int> Current = new();
    private Queue<Vector3> Path = new();
    private List<Vector3Int> Banned = new();
    private const float Offset = 0.16f;
    private TileBase PickedTile;
    private Grid Grid; // Grid
    private Tilemap Tile; // Ground
    private int IndexBlock = 0;
    public Transform transform { private get; set; }
    private readonly Vector3 CellSize;
    
    public PathFinder(Grid grid, Tilemap tilemap, bool drawPath = false)
    {
        Tile = tilemap;
        Grid = grid;
        CellSize = Tile.cellSize;
        DrawPath = drawPath;
        if (DrawPath)
        {
            var drawSprite = Resources.Load<SpriteRenderer>("DrawPathIcon").sprite;
            var tile = new Tile();
            tile.sprite = drawSprite;
            PickedTile = tile;
        }
    }
    
    public Queue<Vector3> FindPath(Vector3 playerPosition)
    {
        Path.Clear();
        Banned.Clear();
        IndexBlock = 100;
        Vector3 currentPosition = transform.position;
        var distance = (playerPosition - currentPosition).sqrMagnitude;
        while (distance > 0.1f)
        {
            if (IndexBlock <= 0)
                return null;
            currentPosition = FindNewCellToMove(playerPosition, currentPosition);
            distance = (playerPosition - currentPosition).sqrMagnitude;
            IndexBlock--;
        }
        return Path;
    }

    private Vector3 FindNewCellToMove(Vector3 playerPosition, Vector3 currentPosition)
    {
        var tileSizeToVector3 = Vector3.zero;
        FindTilesAroundPosition(currentPosition);
        currentPosition = PickTile(playerPosition);
        tileSizeToVector3 = new Vector3(currentPosition.x + CellSize.x / 2, currentPosition.y + CellSize.y / 2, 0);
        Path.Enqueue(tileSizeToVector3);
        return currentPosition;
    }

    private void FindTilesAroundPosition(Vector3 pos)
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
        var currentTile = FindNearCurrentTile(playerPos);
        BanOtherTiles(currentTile);
        DrawPathOnBlocks(currentTile);
        return Tile.CellToWorld(currentTile);
    }

    private Vector3Int FindNearCurrentTile(Vector3 playerPos)
    {
        var maxDistance = Mathf.Infinity;
        var currentTile = Vector3Int.zero;
        var playerPosition = Tile.WorldToCell(playerPos);
        for (int i = 0; i < Current.Count; i++)
        {
            float distance = (playerPosition - Current[i]).sqrMagnitude;
            if (distance < maxDistance)
            {
                maxDistance = distance;
                currentTile = Current[i];
            }
        }

        return currentTile;
    }

    private void BanOtherTiles(Vector3Int currentTile)
    {
        foreach (var v3 in Current)
        {
            if (v3 != currentTile)
            {
                if (Banned.Contains(v3) == false)
                    Banned.Add(v3);
            }
        }
    }

    private void DrawPathOnBlocks(Vector3Int currentTile)
    {
        if (DrawPath)
            Tile.SetTile(currentTile, PickedTile);
    }
}
