using UnityEngine;

namespace Nastar
{
    public class AStarHeuristics
    {
        public static float Manhattan(AStarNode start, AStarNode end, float straightCost, float diagonalCost) 
        {
            return Mathf.Abs(start.X - end.X) * straightCost + 
                   Mathf.Abs(start.Z - end.Z) * straightCost;
        }
	
        public static float Euclidian(AStarNode start, AStarNode end, float straightCost, float diagonalCost) 
        {
            var dx = start.X - end.X;
            var dy = start.Z - end.Z;
		
            return Mathf.Sqrt(dx * dx + dy * dy) * straightCost;
        }
	
        public static float Diagonal(AStarNode start, AStarNode end, float straightCost, float diagonalCost) 
        {
            var dx = Mathf.Abs(start.X - end.X);
            var dy = Mathf.Abs(start.Z - end.Z);
		
            var diagonal = Mathf.Min(dx, dy);
            var straight = dx + dy;
		
            return diagonalCost * diagonal + straightCost * (straight - 2 * diagonal);
        }
    }
}