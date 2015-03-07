//#define ASTARDEBUG

using System;
using System.Collections.Generic;
using Pathfinding.Util;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Pathfinding {
	[AddComponentMenu ("Pathfinding/Modifiers/Funnel")]
	[Serializable]
	/** Simplifies paths on navmesh graphs using the funnel algorithm.
	 * The funnel algorithm is an algorithm which can, given a path corridor with nodes in the path where the nodes have an area, like triangles, it can find the shortest path inside it.
	 * This makes paths on navmeshes look much cleaner and smoother.
	 * \image html images/funnelModifier_on.png
	 * \ingroup modifiers
	 */
	public class FunnelModifier : MonoModifier {
		
	#if UNITY_EDITOR
		[MenuItem ("CONTEXT/Seeker/Add Funnel Modifier")]
		public static void AddComp (MenuCommand command) {
			(command.context as Component).gameObject.AddComponent (typeof(FunnelModifier));
		}
	#endif
		
		public override ModifierData input {
			get { return ModifierData.StrictVectorPath; }
		}
		
		public override ModifierData output {
			get { return ModifierData.VectorPath; }
		}
		
		public override void Apply (Path p, ModifierData source) {
			var path = p.path;
			var vectorPath = p.vectorPath;
			
			if (path == null || path.Count == 0 || vectorPath == null || vectorPath.Count != path.Count) {
				return;
			}
			
			var funnelPath = ListPool<Vector3>.Claim ();
			
			//Claim temporary lists and try to find lists with a high capacity
			var left = ListPool<Vector3>.Claim (path.Count+1);
			var right = ListPool<Vector3>.Claim (path.Count+1);
			
			AstarProfiler.StartProfile ("Construct Funnel");
			
			left.Add (vectorPath[0]);
			right.Add (vectorPath[0]);
			
			for (var i=0;i<path.Count-1;i++) {
				var a = path[i].GetPortal (path[i+1], left, right, false);
				var b = false;//path[i+1].GetPortal (path[i], right, left, true);
				
				if (!a && !b) {
					left.Add ((Vector3)path[i].position);
					right.Add ((Vector3)path[i].position);
					
					left.Add ((Vector3)path[i+1].position);
					right.Add ((Vector3)path[i+1].position);
				}
			}
			
			left.Add (vectorPath[vectorPath.Count-1]);
			right.Add (vectorPath[vectorPath.Count-1]);
			
			if (!RunFunnel (left,right,funnelPath)) {
				//If funnel algorithm failed, degrade to simple line
				funnelPath.Add (vectorPath[0]);
				funnelPath.Add (vectorPath[vectorPath.Count-1]);
			}
			
			ListPool<Vector3>.Release (p.vectorPath);
			p.vectorPath = funnelPath;
			
			ListPool<Vector3>.Release (left);
			ListPool<Vector3>.Release (right);
		}
		
		/** Calculate a funnel path from the \a left and \a right portal lists.
		 * The result will be appended to \a funnelPath
		 */
		public bool RunFunnel (List<Vector3> left, List<Vector3> right, List<Vector3> funnelPath) {
			
			if (left == null) throw new ArgumentNullException("left");
			if (right == null) throw new ArgumentNullException("right");
			if (funnelPath == null) throw new ArgumentNullException("funnelPath");
			
			if (left.Count != right.Count) throw new ArgumentException("left and right lists must have equal length");
			
			if (left.Count <= 3) {
				return false;
			}
			
			//Remove identical vertices
			while (left[1] == left[2] && right[1] == right[2]) {
				//System.Console.WriteLine ("Removing identical left and right");
				left.RemoveAt (1);
				right.RemoveAt (1);
				
				if (left.Count <= 3) {
					return false;
				}
				
			}
			
			var swPoint = left[2];
			if (swPoint == left[1]) {
				swPoint = right[2];
			}
			
			//Test
			while (Polygon.IsColinear (left[0],left[1],right[1]) || Polygon.Left (left[1],right[1],swPoint) == Polygon.Left (left[1],right[1],left[0])) {
				
				left.RemoveAt (1);
				right.RemoveAt (1);
				
				if (left.Count <= 3) {
					return false;
				}
				
				swPoint = left[2];
				if (swPoint == left[1]) {
					swPoint = right[2];
				}
			}
			
			//Switch left and right to really be on the "left" and "right" sides
			if (!Polygon.IsClockwise (left[0],left[1],right[1]) && !Polygon.IsColinear (left[0],left[1],right[1])) {
				//System.Console.WriteLine ("Wrong Side 2");
				var tmp = left;
				left = right;
				right = tmp;
			}
			
			
			funnelPath.Add (left[0]);
			
			var portalApex = left[0];
			var portalLeft = left[1];
			var portalRight = right[1];
			
			var apexIndex = 0;
			var rightIndex = 1;
			var leftIndex = 1;
			
			for (var i=2;i<left.Count;i++) {
				
				if (funnelPath.Count > 2000) {
					Debug.LogWarning ("Avoiding infinite loop. Remove this check if you have this long paths.");
					break;
				}
				
				var pLeft = left[i];
				var pRight = right[i];
				
				/*Debug.DrawLine (portalApex,portalLeft,Color.red);
				Debug.DrawLine (portalApex,portalRight,Color.yellow);
				Debug.DrawLine (portalApex,left,Color.cyan);
				Debug.DrawLine (portalApex,right,Color.cyan);*/
				
				if (Polygon.TriangleArea2 (portalApex,portalRight,pRight) >= 0) {
					
					if (portalApex == portalRight || Polygon.TriangleArea2 (portalApex,portalLeft,pRight) <= 0) {
						portalRight = pRight;
						rightIndex = i;
					} else {
						funnelPath.Add (portalLeft);
						portalApex = portalLeft;
						apexIndex = leftIndex;
						
						portalLeft = portalApex;
						portalRight = portalApex;
						
						leftIndex = apexIndex;
						rightIndex = apexIndex;
						
						i = apexIndex;
						
						continue;
					}
				}
				
				if (Polygon.TriangleArea2 (portalApex,portalLeft,pLeft) <= 0) {
					
					if (portalApex == portalLeft || Polygon.TriangleArea2 (portalApex,portalRight,pLeft) >= 0) {
						portalLeft = pLeft;
						leftIndex = i;
						
					} else {
						
						funnelPath.Add (portalRight);
						portalApex = portalRight;
						apexIndex = rightIndex;
						
						portalLeft = portalApex;
						portalRight = portalApex;
						
						leftIndex = apexIndex;
						rightIndex = apexIndex;
						
						i = apexIndex;
						
						continue;
					}
				}
			}
			
			funnelPath.Add (left[left.Count-1]);
			return true;
		}
	}
	
	/** Graphs implementing this interface have support for the Funnel modifier */
	public interface IFunnelGraph {
		
		void BuildFunnelCorridor (List<GraphNode> path, int sIndex, int eIndex, List<Vector3> left, List<Vector3> right);
		
		/** Add the portal between node \a n1 and \a n2 to the funnel corridor. The left and right edges does not necesarily need to be the left and right edges (right can be left), they will be swapped if that is detected. But that works only as long as the edges do not switch between left and right in the middle of the path.
		  */
		void AddPortal (GraphNode n1, GraphNode n2, List<Vector3> left, List<Vector3> right);
	}
}