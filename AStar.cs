using UnityEngine;
using System;
using System.Collections.Generic;

namespace Nastar
{
    public class AStar 
    {	
		public List<AStarNode> FindPath(
			SparseMatrix<AStarNode> grid, 
			AStarNode start, AStarNode end, 
			Func<AStarNode, AStarNode, float, float, float> heuristic, 
			bool isDiagonal, float straightCost, float diagonalCost) 
		{
			var result = new List<AStarNode>();
			var open   = new List<AStarNode>();
			
			var currentNode = start;
			
			var openNodes   = new Dictionary<AStarNode, bool>();
			var closedNodes = new Dictionary<AStarNode, bool>();
			
			currentNode.G = 0;
			currentNode.H = heuristic(start, end, straightCost, diagonalCost);
			currentNode.F = start.G + start.H;
			
			while (currentNode != end) 
			{
				var startX = Mathf.Max(grid.MinX - 1, currentNode.X - 1);
				var startY = Mathf.Max(grid.MinY - 1, currentNode.Z - 1);
				
				var endX = Mathf.Min(grid.MaxX - 1, currentNode.X + 1);
				var endY = Mathf.Min(grid.MaxY - 1, currentNode.Z + 1);
				
				for (var i = startX; i <= endX; i++) {
					for (var j = startY; j <= endY; j++) {
						var test = grid.Take(i, j);
	
						if (test == null)
						{
							continue;
						}
						
						if (test == currentNode) {
							continue;
						}
						
						if (!test.Walkable  ||
							grid.Take(currentNode.X, test.Z) == null || !grid.Take(currentNode.X, test.Z).Walkable ||
							grid.Take(test.X, currentNode.Z) == null || !grid.Take(test.X, currentNode.Z).Walkable) {
							continue;
						}
	
						var cost = straightCost;
						
						if (!(currentNode.X == test.X || currentNode.Z == test.Z)) {
							if (!isDiagonal) {
								continue;
							}
								
							cost = diagonalCost;
						}
						
						var g = currentNode.G + cost;
						var h = heuristic(test, end, straightCost, diagonalCost);
						var f = g + h;
						
						if (openNodes.ContainsKey(test) || closedNodes.ContainsKey(test)) {
							if (test.F > f) {
								test.G = g;
								test.H = h;
								test.F = f;
								
								test.Parent = currentNode;
							}
						} else {
							test.G = g;
							test.H = h;
							test.F = f;
							
							test.Parent = currentNode;
							
							open.Add(test);
							openNodes.Add(test, true);
						}
					}
				}
				
				if (!closedNodes.ContainsKey(currentNode)) {
					closedNodes.Add(currentNode, true);	
				}
				
				if (open.Count == 0) {
					return result;
				}
				
				open.Sort();
				
				currentNode = open[0];
				open.RemoveAt(0);
			}
			
			currentNode = end;
			
			while (currentNode != start) 
			{
				currentNode = currentNode.Parent;
				
				result.Add(currentNode);
			}
			
			result.Reverse();
			result.Add(end);
			
			return result;
		}
	}
}
