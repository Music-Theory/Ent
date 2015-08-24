using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ent.Data {
	/// <summary>
	/// A node in a tree.
	/// </summary>
	/// <typeparam name="T">The type of the value of the node.</typeparam>
	class Node<T> {

		Node<T> parent;

		T value;

		List<Node<T>> children;

		public Node(T value, Node<T> parent = null) {
			this.value = value;
			this.parent = parent;
		}

		/// <summary>
		/// Adds a node to the list of children. If children == null, it makes a new list for it.
		/// </summary>
		/// <param name="a">The value of the node to be added.</param>
		public void add(T a) {
			if (children == null) {
				children = new List<Node<T>>();
			}
			children.Add(new Node<T>(a, this));
		}

		public bool remove(T r) {
			if (children == null) {
				return false;
			}
			return children.Remove(getFirst(r));
		}

		public Node<T> getFirst(T val) {
			Node<T> candidate = null;
			if (this.value.Equals(val)) {
				return this;
			}
			Node<T> temp;
			foreach (Node<T> n in children) {
				temp = n.getFirst(val);
				if (temp != null) {
					candidate = temp;
					break;
				}
			}
			return candidate;
		}

		public List<Node<T>> getAll(T val) {
			List<Node<T>> nodes = new List<Node<T>>();

			if (this.value.Equals(val)) { nodes.Add(this); }
			
			foreach (Node<T> n in children) {
				nodes.AddRange(n.getAll(val));
			}

			return nodes;
		}

	}

}
