using System;

namespace Nastar
{
	public class AStarNode : IComparable 
	{
		public int X;
		public int Z;

		public bool Walkable;

		internal AStarNode Parent;

		internal float G;
		internal float H;
		internal float F;
    
		public int CompareTo(object other)
		{
			var otherNode = (other as AStarNode);
			if (otherNode == null)
			{
				return 1;
			}

			if (F == otherNode.F)
			{
				return 0;
			}
		
			if (F > otherNode.F) {
				return 1;
			}
		
			return -1;
		}
		
		public override string ToString()
		{
			return $"[Node x={X}, z={Z}]";
		}
	}
}
