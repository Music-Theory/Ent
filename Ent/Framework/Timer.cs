using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soul.Framework {
	class Timer {

		public bool started = false;
		public bool paused = false;

		public uint startTicks = 0;
		public uint pausedTicks = 0;

		public void start() {
			started = true;
			paused = false;

			startTicks = SDL.SDL_GetTicks();
			pausedTicks = 0;
		}

		public void stop() {
			started = false;
			paused = false;

			startTicks = 0;
			pausedTicks = 0;
		}

		public void pause() {
			if (started && !paused) {
				paused = true;

				pausedTicks = SDL.SDL_GetTicks() - startTicks;
				startTicks = 0;
			}
		}

		public void unpause() {
			if (started && paused) {
				paused = false;

				startTicks = SDL.SDL_GetTicks() - pausedTicks;
				pausedTicks = 0;
			}
		}

		public uint getTicks() {

			if (started) {
				if (paused) {
					return pausedTicks;
				} else {
					return SDL.SDL_GetTicks() - startTicks;
				}
			}

			return 0;
		}

	}
}
