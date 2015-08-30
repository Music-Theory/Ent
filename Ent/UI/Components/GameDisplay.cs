namespace Ent.UI.Components {
	/// <summary>
	/// Allows a window entity to show the game.
	/// </summary>
	class GameDisplay : Component {

		/// <summary>
		/// Whether the entity is currently displaying the game.
		/// </summary>
		public bool current;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="current">Whether it should display the game.</param>
		public GameDisplay(bool current = true) {
			this.current = current;
			name = "GameDisplay";
		}

	}
}
