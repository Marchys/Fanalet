//#define ASTAR_PROFILE

using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pathfinding
{
	/** Contains useful functions for working with paths and nodes.
	 * This class works a lot with the Node class, a useful function to get nodes is AstarPath.GetNearest.
	  * \see AstarPath.GetNearest
	  * \see Pathfinding.Utils.GraphUpdateUtilities
	  * \since Added in version 3.2
	  * \ingroup utils
	  * 
	  */
	public static class PathUtilities {
		/** Returns if there is a walkable path from \a n1 to \a n2.
		 * If you are making changes to the graph, areas must first be recaculated using FloodFill()
		 * \note This might return true for small areas even if there is no possible path if AstarPath.minAreaSize is greater than zero (0).
		 * So when using this, it is recommended to set AstarPath.minAreaSize to 0. (A* Inspector -> Settings -> Pathfinding)
		 * \see AstarPath.GetNearest
		 */
		public static bool IsPathPossible (GraphNode n1, GraphNode n2) {
			return n1.Walkable && n2.Walkable && n1.Area == n2.Area;
		}
		
		/** Returns if there are walkable paths between all nodes.
		 * If you are making changes to the graph, areas must first be recaculated using FloodFill()
		 * \note This might return true for small areas even if there is no possible path if AstarPath.minAreaSize is greater than zero (0).
		 * So when using this, it is recommended to set AstarPath.minAreaSize to 0. (A* Inspector -> Settings -> Pathfinding)
		 * \see AstarPath.GetNearest
		 */
		public static bool IsPathPossible (List<GraphNode> nodes) {
			var area = nodes[0].Area;
			for (var i=0;i<nodes.Count;i++) if (!nodes[i].Walkable || nodes[i].Area != area) return false;
			return true;
		}
		
		/** Returns all nodes reachable from the seed node.
		 * This function performs a BFS (breadth-first-search) or flood fill of the graph and returns all nodes which can be reached from
		 * the seed node. In almost all cases this will be identical to returning all nodes which have the same area as the seed node.
		 * In the editor areas are displayed as different colors of the nodes.
		 * The only case where it will not be so is when there is a one way path from some part of the area to the seed node
		 * but no path from the seed node to that part of the graph.
		 * 
		 * The returned list is sorted by node distance from the seed node
		 * i.e distance is measured in the number of nodes the shortest path from \a seed to that node would pass through.
		 * Note that the distance measurement does not take heuristics, penalties or tag penalties.
		 * 
		 * Depending on the number of reachable nodes, this function can take quite some time to calculate
		 * so don't use it too often or it might affect the framerate of your game.
		 * 
		 * \param seed The node to start the search from
		 * \param tagMask Optional mask for tags. This is a bitmask.
		 * 
		 * \returns A List<Node> containing all nodes reachable from the seed node.
		 * For better memory management the returned list should be pooled, see Pathfinding.Util.ListPool
		 */
		public static List<GraphNode> GetReachableNodes (GraphNode seed, int tagMask = -1) {
			var stack = StackPool<GraphNode>.Claim ();
			var list = ListPool<GraphNode>.Claim ();
			
			/** \todo Pool */
			var map = new HashSet<GraphNode>();
			
			GraphNodeDelegate callback;
			if (tagMask == -1) {
				callback = delegate (GraphNode node) {
					if (node.Walkable && map.Add (node)) {
						list.Add (node);
						stack.Push (node);
					}
				};
			} else {
				callback = delegate (GraphNode node) {
					if (node.Walkable && ((tagMask >> (int)node.Tag) & 0x1) != 0 && map.Add (node)) {
						list.Add (node);
						stack.Push (node);
					}
				};
			}
			
			callback (seed);
			
			while (stack.Count > 0) {
				stack.Pop ().GetConnections (callback);
			}
			
			StackPool<GraphNode>.Release (stack);
			
			return list;
		}

		static Queue<GraphNode> BFSQueue;	
		static Dictionary<GraphNode,int> BFSMap;

		/** Returns all nodes up to a given node-distance from the seed node.
		 * This function performs a BFS (breadth-first-search) or flood fill of the graph and returns all nodes within a specified node distance which can be reached from
		 * the seed node. In almost all cases when \a depth is large enough this will be identical to returning all nodes which have the same area as the seed node.
		 * In the editor areas are displayed as different colors of the nodes.
		 * The only case where it will not be so is when there is a one way path from some part of the area to the seed node
		 * but no path from the seed node to that part of the graph.
		 * 
		 * The returned list is sorted by node distance from the seed node
		 * i.e distance is measured in the number of nodes the shortest path from \a seed to that node would pass through.
		 * Note that the distance measurement does not take heuristics, penalties or tag penalties.
		 * 
		 * Depending on the number of nodes, this function can take quite some time to calculate
		 * so don't use it too often or it might affect the framerate of your game.
		 * 
		 * \param seed The node to start the search from.
		 * \param depth The maximum node-distance from the seed node.
		 * \param tagMask Optional mask for tags. This is a bitmask.
		 *
		 * \returns A List<Node> containing all nodes reachable up to a specified node distance from the seed node.
		 * For better memory management the returned list should be pooled, see Pathfinding.Util.ListPool
		 * 
		 * \warning This method is not thread safe. Only use it from the Unity thread (i.e normal game code).
		 */
		public static List<GraphNode> BFS (GraphNode seed, int depth, int tagMask = -1) {

			if ( BFSQueue == null ) BFSQueue = new Queue<GraphNode>();
			var que = BFSQueue;

			if ( BFSMap == null ) BFSMap = new Dictionary<GraphNode,int>();
			var map = BFSMap;

			// Even though we clear at the end of this function, it is good to
			// do it here as well in case the previous invocation of the method
			// threw an exception for some reason
			// and didn't clear the que and map
			que.Clear ();
			map.Clear ();

			var result = ListPool<GraphNode>.Claim ();

			var currentDist = -1;
			GraphNodeDelegate callback;
			if (tagMask == -1) {
				callback = delegate (GraphNode node) {
					if (node.Walkable && !map.ContainsKey (node)) {
						map.Add (node, currentDist+1);
						result.Add (node);
						que.Enqueue (node);
					}
				};
			} else {
				callback = delegate (GraphNode node) {
					if (node.Walkable && ((tagMask >> (int)node.Tag) & 0x1) != 0 && !map.ContainsKey (node)) {
						map.Add (node, currentDist+1);
						result.Add (node);
						que.Enqueue (node);
					}
				};
			}

			callback (seed);

			while (que.Count > 0 ) {
				var n = que.Dequeue ();
				currentDist = map[n];

				if ( currentDist >= depth ) break;

				n.GetConnections (callback);
			}

			que.Clear ();
			map.Clear ();

			return result;
		}

		/** Returns points in a spiral centered around the origin with a minimum clearance from other points.
		 * The points are laid out on the involute of a circle
		 * \see http://en.wikipedia.org/wiki/Involute
		 * Which has some nice properties.
		 * All points are separated by \a clearance world units.
		 * This method is O(n), yes if you read the code you will see a binary search, but that binary search
		 * has an upper bound on the number of steps, so it does not yield a log factor.
		 * 
		 * \note Consider recycling the list after usage to reduce allocations.
		 * \see Pathfinding.Util.ListPool
		 */
		public static List<Vector3> GetSpiralPoints (int count, float clearance) {
			
			var pts = ListPool<Vector3>.Claim(count);
			
			// The radius of the smaller circle used for generating the involute of a circle
			// Calculated from the separation distance between the turns
			var a = clearance/(2*Mathf.PI);
			float t = 0;
			
			
			pts.Add (InvoluteOfCircle(a, t));
			
			for (var i=0;i<count;i++) {
				var prev = pts[pts.Count-1];
				
				// d = -t0/2 + sqrt( t0^2/4 + 2d/a )
				// Minimum angle (radians) which would create an arc distance greater than clearance
				var d = -t/2 + Mathf.Sqrt (t*t/4 + 2*clearance/a);
				
				// Binary search for separating this point and the previous one
				var mn = t + d;
				var mx = t + 2*d;
				while (mx - mn > 0.01f) {
					var mid = (mn + mx)/2;
					var p = InvoluteOfCircle (a, mid);
					if ((p - prev).sqrMagnitude < clearance*clearance) {
						mn = mid;
					} else {
						mx = mid;
					}
				}
				
				pts.Add ( InvoluteOfCircle (a, mx) );
				t = mx;
			}
			
			return pts;
		}
		
		/** Returns the XZ coordinate of the involute of circle.
		 * \see http://en.wikipedia.org/wiki/Involute
		 */
		private static Vector3 InvoluteOfCircle (float a, float t) {
			return new Vector3(a*(Mathf.Cos(t) + t*Mathf.Sin(t)), 0, a*(Mathf.Sin(t) - t*Mathf.Cos(t)));
		}
		
		/** Will calculate a number of points around \a p which are on the graph and are separated by \a clearance from each other.
		 * This is like GetPointsAroundPoint except that \a previousPoints are treated as being in world space.
		 * The average of the points will be found and then that will be treated as the group center.
		 */
		public static void GetPointsAroundPointWorld (Vector3 p, IRaycastableGraph g, List<Vector3> previousPoints, float radius, float clearanceRadius) {
			if ( previousPoints.Count == 0 ) return;

			var avg = Vector3.zero;
			for ( var i = 0; i < previousPoints.Count; i++ ) avg += previousPoints[i];
			avg /= previousPoints.Count;

			for ( var i = 0; i < previousPoints.Count; i++ ) previousPoints[i] -= avg;

			GetPointsAroundPoint ( p, g, previousPoints, radius, clearanceRadius );
		}

		/** Will calculate a number of points around \a p which are on the graph and are separated by \a clearance from each other.
		 * The maximum distance from \a p to any point will be \a radius.
		 * Points will first be tried to be laid out as \a previousPoints and if that fails, random points will be selected.
		 * This is great if you want to pick a number of target points for group movement. If you pass all current agent points from e.g the group's average position
		 * this method will return target points so that the units move very little within the group, this is often aesthetically pleasing and reduces jitter if using
		 * some kind of local avoidance.
		 * 
		 * \param g The graph to use for linecasting. If you are only using one graph, you can get this by AstarPath.active.graphs[0] as IRaycastableGraph.
		 * Note that not all graphs are raycastable, recast, navmesh and grid graphs are raycastable. On recast and navmesh it works the best.
		 * \param previousPoints The points to use for reference. Note that these should not be in world space. They are treated as relative to \a p.
		 */
		public static void GetPointsAroundPoint (Vector3 p, IRaycastableGraph g, List<Vector3> previousPoints, float radius, float clearanceRadius) {
			
			if (g == null) throw new ArgumentNullException ("g");
			
			var graph = g as NavGraph;
			
			if (graph == null) throw new ArgumentException ("g is not a NavGraph");
			
			var nn = graph.GetNearestForce (p, NNConstraint.Default);
			p = nn.clampedPosition;
			
			if (nn.node == null) {
				// No valid point to start from
				return;
			}
			
			
			// Make sure the enclosing circle has a radius which can pack circles with packing density 0.5
			radius = Mathf.Max (radius, 1.4142f*clearanceRadius*Mathf.Sqrt(previousPoints.Count));//Mathf.Sqrt(previousPoints.Count*clearanceRadius*2));
			clearanceRadius *= clearanceRadius;
			
			for (var i=0;i<previousPoints.Count;i++) {
				
				var dir = previousPoints[i];
				var magn = dir.magnitude;
				
				if (magn > 0) dir /= magn;
			
				var newMagn = radius;//magn > radius ? radius : magn;
				dir *= newMagn;
				
				var worked = false;
				
				GraphHitInfo hit;
				
				var tests = 0;
				do {
					
					var pt = p + dir;

					if (g.Linecast (p, pt, nn.node, out hit)) {
						pt = hit.point;
					}
					
					for (var q = 0.1f; q <= 1.0f; q+= 0.05f) {
						var qt = (pt - p)*q + p;
						worked = true;
						for (var j=0;j<i;j++) {
							if ((previousPoints[j] - qt).sqrMagnitude < clearanceRadius) {
								worked = false;
								break;
							}
						}
						
						if (worked) {
							previousPoints[i] = qt;
							break;
						}
					}
					
					if (!worked) {

						// Abort after 8 tries
						if (tests > 8) {
							worked = true;
						} else {
							clearanceRadius *= 0.9f;
							// This will pick points in 2D closer to the edge of the circle with a higher probability
							dir = Random.onUnitSphere * Mathf.Lerp (newMagn, radius, tests / 5);
							dir.y = 0;
							tests++;
						}
					}
				} while (!worked);
			}
			
		}
		
		/** Returns randomly selected points on the specified nodes with each point being separated by \a clearanceRadius from each other.
		 * Selecting points ON the nodes only works for TriangleMeshNode (used by Recast Graph and Navmesh Graph) and GridNode (used by GridGraph).
		 * For other node types, only the positions of the nodes will be used.
		 * 
		 * clearanceRadius will be reduced if no valid points can be found.
		 */
		public static List<Vector3> GetPointsOnNodes (List<GraphNode> nodes, int count, float clearanceRadius = 0) {
			
			if (nodes == null) throw new ArgumentNullException ("nodes");
			if (nodes.Count == 0) throw new ArgumentException ("no nodes passed");
			
			var rnd = new System.Random();
			
			var pts = ListPool<Vector3>.Claim(count);
			
			// Square
			clearanceRadius *= clearanceRadius;
			
			if (nodes[0] is TriangleMeshNode || nodes[0] is GridNode) {
				//Assume all nodes are triangle nodes or grid nodes
				
				var accs = ListPool<float>.Claim(nodes.Count);
					
				float tot = 0;
				
				for (var i=0;i<nodes.Count;i++) {
					var tnode = nodes[i] as TriangleMeshNode;
					if (tnode != null) {
						float a = Math.Abs(Polygon.TriangleArea(tnode.GetVertex(0), tnode.GetVertex(1), tnode.GetVertex(2)));
						tot += a;
						accs.Add (tot);
					}
					 else {
						var gnode = nodes[i] as GridNode;
						
						if (gnode != null) {
							var gg = GridNode.GetGridGraph (gnode.GraphIndex);
							var a = gg.nodeSize*gg.nodeSize;
							tot += a;
							accs.Add (tot);
						} else {
							accs.Add(tot);
						}
					}
				}
				
				for (var i=0;i<count;i++) {
					
					//Pick point
					var testCount = 0;
					var testLimit = 10;
					var worked = false;
					
					while (!worked) {
						worked = true;
						
						//If no valid points can be found, progressively lower the clearance radius until such a point is found
						if (testCount >= testLimit) {
							clearanceRadius *= 0.8f;
							testLimit += 10;
							if (testLimit > 100) clearanceRadius = 0;
						}
					
						var tg = (float)rnd.NextDouble()*tot;
						var v = accs.BinarySearch(tg);
						if (v < 0) v = ~v;
						
						if (v >= nodes.Count) {
							// This shouldn't happen, due to NextDouble being smaller than 1... but I don't trust floating point arithmetic.
							worked = false;
							continue;
						}
						
						var node = nodes[v] as TriangleMeshNode;
						
						Vector3 p;
						
						if (node != null) {
							// Find a random point inside the triangle
							float v1;
							float v2;
							do {
								v1 = (float)rnd.NextDouble();
								v2 = (float)rnd.NextDouble();
							} while (v1+v2 > 1);
							
							p = ((Vector3)(node.GetVertex(1)-node.GetVertex(0)))*v1 + ((Vector3)(node.GetVertex(2)-node.GetVertex(0)))*v2 + (Vector3)node.GetVertex(0);
						} else {
							var gnode = nodes[v] as GridNode;
							
							if (gnode != null) {
								var gg = GridNode.GetGridGraph (gnode.GraphIndex);
								
								var v1 = (float)rnd.NextDouble();
								var v2 = (float)rnd.NextDouble();
								p = (Vector3)gnode.position + new Vector3(v1 - 0.5f, 0, v2 - 0.5f) * gg.nodeSize;
							} else
							{
								//Point nodes have no area, so we break directly instead
								pts.Add ((Vector3)nodes[v].position);
								break;
							}
						}
						
						// Test if it is some distance away from the other points
						if (clearanceRadius > 0) {
							for (var j=0;j<pts.Count;j++) {
								if ((pts[j]-p).sqrMagnitude < clearanceRadius) {
									worked = false;
									break;
								}
							}
						}
						
						if (worked) {
							pts.Add (p);
							break;
						} else {
							testCount++;
						}
					}
				}
				
				ListPool<float>.Release(accs);
				
			} else {
				for (var i=0;i<count;i++) {
					pts.Add ((Vector3)nodes[rnd.Next (nodes.Count)].position);
				}
			}
			
			return pts;
		}
	}
}

