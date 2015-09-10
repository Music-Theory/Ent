namespace Ent.Data {
	public class Tree<T> {

		/// <summary>
		/// The root node.
		/// </summary>
		Node<T> root;

		/// <summary>
		/// The amount of nodes in the tree.
		/// </summary>
		int count;

		public delegate Node<T> NodePlacementDelegate(Node<T> rootNode);
		public static NodePlacementDelegate defaultPlacement = rootNode => rootNode;

		public Tree(T root) {
			this.root = new Node<T>(root);
		}

		/// <summary>
		/// Adds a value to the tree using a delegate.
		/// </summary>
		/// <param name="val">The value to be added.</param>
		/// <param name="nodePlacement">The method for determining where to place it. Use Tree.defaultPlacement if you don't care.</param>
		public void Add(T val, NodePlacementDelegate nodePlacement) {
			nodePlacement(root).Add(val);
			count++;
		}

		public Node<T> Get(T val) {
			return root.GetFirst(val);
		}

	}
}
