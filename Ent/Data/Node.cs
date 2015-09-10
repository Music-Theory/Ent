using System.Collections.Generic;

namespace Ent.Data {
	/// <summary>
	/// A node in a tree.
	/// </summary>
	/// <typeparam name="T">The type of the value of the node.</typeparam>
	public class Node<T> {

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
		public void Add(T a) {
			if (children == null) {
				children = new List<Node<T>>();
			}
			children.Add(new Node<T>(a, this));
		}

		public bool Remove(T r) {
			if (children == null) {
				return false;
			}
			return children.Remove(GetFirst(r));
		}

		public Node<T> GetFirst(T val) {
			Node<T> candidate = null;
			if (value.Equals(val)) {
				return this;
			}
			Node<T> temp;
			foreach (Node<T> n in children) {
				temp = n.GetFirst(val);
				if (temp != null) {
					candidate = temp;
					break;
				}
			}
			return candidate;
		}

		public List<Node<T>> GetAll(T val) {
			List<Node<T>> nodes = new List<Node<T>>();

			if (value.Equals(val)) { nodes.Add(this); }
			
			foreach (Node<T> n in children) {
				nodes.AddRange(n.GetAll(val));
			}

			return nodes;
		}

	}

}
