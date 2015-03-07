#pragma warning disable 0162
#pragma warning disable 0414
#define PROFILE
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Pathfinding
{
	
	
	public class Profile {
		const bool PROFILE_MEM = false;
		
		public string name;
		Stopwatch w;
		int counter = 0;
		long mem = 0;
		long smem = 0;
		
		int control = 1 << 30;
		bool dontCountFirst = false;
		
		public int ControlValue () {
			return control;
		}
		
		public Profile (string name) {
			this.name = name;
			w = new Stopwatch();
		}
		
		[Conditional("PROFILE")]
		public void Start () {
			if (PROFILE_MEM) {
				smem = GC.GetTotalMemory(false);
			}
			if (dontCountFirst && counter == 1) return;
			w.Start();
		}
		
		[Conditional("PROFILE")]
		public void Stop () {
			counter++;
			if (dontCountFirst && counter == 1) return;
			
			w.Stop();
			if (PROFILE_MEM) {
				mem += GC.GetTotalMemory(false)-smem;
			}
			
		}
		
		[Conditional("PROFILE")]
		/** Log using Debug.Log */
		public void Log () {
			Debug.Log (ToString());
		}
		
		[Conditional("PROFILE")]
		/** Log using System.Console */
		public void ConsoleLog () {
#if !NETFX_CORE || UNITY_EDITOR
			Console.WriteLine (ToString());
#endif
		}
		
		[Conditional("PROFILE")]
		public void Stop (int control) {
			counter++;
			if (dontCountFirst && counter == 1) return;
			
			w.Stop();
			if (PROFILE_MEM) {
				mem += GC.GetTotalMemory(false)-smem;
			}
			
			if (this.control == 1 << 30) this.control = control;
			else if (this.control != control) throw new Exception("Control numbers do not match " + this.control + " != " + control);
		}
		
		[Conditional("PROFILE")]
		public void Control (Profile other) {
			if (ControlValue() != other.ControlValue()) {
				throw new Exception("Control numbers do not match ("+name + " " + other.name + ") " + this.ControlValue() + " != " + other.ControlValue());
			}
		}
		
		public override string ToString () {
			var s = name + " #" + counter + " " + w.Elapsed.TotalMilliseconds.ToString("0.0 ms") + " avg: " + (w.Elapsed.TotalMilliseconds/counter).ToString("0.00 ms");
			if (PROFILE_MEM) {
				s += " avg mem: " + (mem/(1.0*counter)).ToString("0 bytes");
			}
			return s;
		}

	}
}

