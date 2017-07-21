using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedDataTypes : MonoBehaviour {

	public enum cellType {wall, clear};

	public class pair {
		public pair (int a, int b) {
			first = a;
			second = b;
		}
		public int first { get; set; }
		public int second { get; set; }
	}

}
