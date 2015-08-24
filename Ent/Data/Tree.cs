using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ent.Data {
	class Tree<T> {

		/// <summary>
		/// The root node.
		/// </summary>
		Node<T> root;

		/// <summary>
		/// The amount of nodes in the tree.
		/// </summary>
		int count;

		public delegate Node<T> nodePlacementDelegate(Node<T> rootNode);
		public static nodePlacementDelegate defaultPlacement = (Node<T> rootNode) => { return rootNode; };

		public Tree(T root) {
			this.root = new Node<T>(root);
		}

		/// <summary>
		/// Adds a value to the tree using a delegate.
		/// </summary>
		/// <param name="val">The value to be added.</param>
		/// <param name="nodePlacement">The method for determining where to place it. Use Tree.defaultPlacement if you don't care.</param>
		public void add(T val, nodePlacementDelegate nodePlacement) {
			nodePlacement(root).add(val);
			count++;
		}

		public Node<T> get(T val) {
			return root.getFirst(val);
		}

	}
}
