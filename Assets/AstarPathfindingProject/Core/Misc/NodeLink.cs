using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Pathfinding {
	[AddComponentMenu("Pathfinding/Link")]
	public class NodeLink : GraphModifier {
		
		/** End position of the link */
		public Transform end;
		
		/** The connection will be this times harder/slower to traverse.
		 * Note that values lower than one will not always make the pathfinder choose this path instead of another path even though this one should
		  * lead to a lower total cost unless you also adjust the Heuristic Scale in A* Inspector -> Settings -> Pathfinding or disable the heuristic altogether.
		  */
		public float costFactor = 1.0f;
		
		/** Make a one-way connection */
		public bool oneWay = false;
		
		/** Delete existing connection instead of adding one */
		public bool deleteConnection = false;
		
		public Transform Start {
			get { return transform; }
		}
		
		public Transform End {
			get { return end; }
		}
	
		public override void OnPostScan () {
			
			if (AstarPath.active.isScanning) {
				InternalOnPostScan ();
			} else {
				AstarPath.active.AddWorkItem (new AstarPath.AstarWorkItem (delegate (bool force) {
					InternalOnPostScan ();
					return true;
				}));
			}
		}
		
		public void InternalOnPostScan () {
			Apply ();
		}
	
		public override void OnGraphsPostUpdate () {
			if (!AstarPath.active.isScanning) {
				AstarPath.active.AddWorkItem (new AstarPath.AstarWorkItem (delegate (bool force) {
					InternalOnPostScan ();
					return true;
				}));
			}
		}
	
		public virtual void Apply () {
			if (Start == null || End == null || AstarPath.active == null) return;
			
			var startNode = AstarPath.active.GetNearest (Start.position).node;
			var endNode = AstarPath.active.GetNearest (End.position).node;
			
			if (startNode == null || endNode == null) return;
			
			
			if (deleteConnection) {
				startNode.RemoveConnection (endNode);
				if (!oneWay)
					endNode.RemoveConnection (startNode);
			} else {
				var cost = (uint)Math.Round ((startNode.position-endNode.position).costMagnitude*costFactor);
	
				startNode.AddConnection (endNode,cost);
				if (!oneWay)
					endNode.AddConnection (startNode,cost);
			}
		}
		
		public void OnDrawGizmos () {
			
			if (Start == null || End == null) return;
			
			var p1 = Start.position;
			var p2 = End.position;
			
			Gizmos.color = deleteConnection ? Color.red : Color.green;
			DrawGizmoBezier (p1,p2);
		}
		
		private void DrawGizmoBezier (Vector3 p1, Vector3 p2) {
			
			var dir = p2-p1;
			
			if (dir == Vector3.zero) return;
			
			var normal = Vector3.Cross (Vector3.up,dir);
			var normalUp = Vector3.Cross (dir,normal);
			
			normalUp = normalUp.normalized;
			normalUp *= dir.magnitude*0.1f;
			
			var p1c = p1+normalUp;
			var p2c = p2+normalUp;
			
			var prev = p1;
			for (var i=1;i<=20;i++) {
				var t = i/20.0f;
				var p = AstarMath.CubicBezier (p1,p1c,p2c,p2,t);
				Gizmos.DrawLine (prev,p);
				prev = p;
			}
		}
		
	#if UNITY_EDITOR
		[MenuItem ("Edit/Pathfinding/Link Pair %&l")]
		public static void LinkObjects () {
			var tfs = Selection.transforms;
			if (tfs.Length == 2) {
				LinkObjects (tfs[0],tfs[1], false);
			}
			SceneView.RepaintAll ();
		}
		
		[MenuItem ("Edit/Pathfinding/Unlink Pair %&u")]
		public static void UnlinkObjects () {
			var tfs = Selection.transforms;
			if (tfs.Length == 2) {
				LinkObjects (tfs[0],tfs[1], true);
			}
			SceneView.RepaintAll ();
		}
		
		[MenuItem ("Edit/Pathfinding/Delete Links on Selected %&b")]
		public static void DeleteLinks () {
			var tfs = Selection.transforms;
			for (var i=0;i<tfs.Length;i++) {
				var conns = tfs[i].GetComponents<NodeLink> ();
				for (var j=0;j<conns.Length;j++) DestroyImmediate (conns[j]);
			}
			SceneView.RepaintAll ();
		}
		
		public static void LinkObjects (Transform a, Transform b, bool removeConnection) {
			NodeLink connecting = null;
			var conns = a.GetComponents<NodeLink> ();
			for (var i=0;i<conns.Length;i++) {
				if (conns[i].end == b) {
					connecting = conns[i];
					break;
				}
			}
			
			conns = b.GetComponents<NodeLink> ();
			for (var i=0;i<conns.Length;i++) {
				if (conns[i].end == a) {
					connecting = conns[i];
					break;
				}
			}
			
			if (removeConnection) {
				if (connecting != null) DestroyImmediate (connecting);
			} else {
				if (connecting == null) {
					connecting = a.gameObject.AddComponent<NodeLink> ();
					connecting.end = b;
				} else {
					connecting.deleteConnection = !connecting.deleteConnection;
				}
			}
		}
	#endif
	}
}