using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using SDL2;

namespace Ent.Utility {
	public static class Utility {

		public static XmlDocument xDoc = new XmlDocument();

		public static string COLORFILE;
		public static string MAPFILE;
		public static string TILEFILE;
		public static string TILESET;
		public static string TILEINFO;

		public static string SPECIES;
		public static string HAIRTYPES;
		public static string BODYTYPES;
		public static string BODYPARTS;

		public static int RES_X = 1600;
		public static int RES_Y = 900;
		public static uint MAX_FPS = 60;
		public static uint TICKS_PER_FRAME = 1000 / MAX_FPS;
		public static int SET_WIDTH;
		public static int SET_HEIGHT;
		public static int TILE_HEIGHT;
		public static int TILE_WIDTH;
		public static int MAP_WIDTH;
		public static int MAP_HEIGHT;

		public static int TILE_RENDER_WIDTH;
		public static int TILE_RENDER_HEIGHT;

		public static SDL.SDL_Keycode MOVE_N;
		public static SDL.SDL_Keycode MOVE_NE;
		public static SDL.SDL_Keycode MOVE_E;
		public static SDL.SDL_Keycode MOVE_SE;
		public static SDL.SDL_Keycode MOVE_S;
		public static SDL.SDL_Keycode MOVE_SW;
		public static SDL.SDL_Keycode MOVE_W;
		public static SDL.SDL_Keycode MOVE_NW;

		public static int PLAYER_KEY = -1;

		public static XmlDocument COLORXML = new XmlDocument();
		public static XmlDocument TILEXML = new XmlDocument();
		public static XmlDocument BODYPARTXML = new XmlDocument();
		public static XmlDocument BODYTYPEXML = new XmlDocument();

		public static string BASE_PATH;
		public static TextWriter OSTREAM = Console.Out;
		public static ErrorLogger erLogger = new ErrorLogger();

		public static Random rand = new Random();

		public static void initSettings() {
			xDoc.Load(getResourcePath() + "\\Settings.xml");
			COLORFILE = getResourcePath() + xDoc.SelectSingleNode("//settings/colorFile").InnerText;
			COLORXML.Load(COLORFILE);
			MAPFILE = getResourcePath() + xDoc.SelectSingleNode("//settings/mapFile").InnerText;
			TILEFILE = getResourcePath() + xDoc.SelectSingleNode("//settings/tileFile").InnerText;
			TILEXML.Load(TILEFILE);

			RES_X = Convert.ToInt16(xDoc.SelectSingleNode("//settings/resX").InnerText);
			RES_Y = Convert.ToInt16(xDoc.SelectSingleNode("//settings/resY").InnerText);
			MAX_FPS = Convert.ToUInt16(xDoc.SelectSingleNode("//settings/maxFPS").InnerText);

			MOVE_N = SDL.SDL_GetKeyFromName(xDoc.SelectSingleNode("//key[@func='moveN']").InnerText);
			MOVE_NE = SDL.SDL_GetKeyFromName(xDoc.SelectSingleNode("//key[@func='moveNE']").InnerText);
			MOVE_E = SDL.SDL_GetKeyFromName(xDoc.SelectSingleNode("//key[@func='moveE']").InnerText);
			MOVE_SE = SDL.SDL_GetKeyFromName(xDoc.SelectSingleNode("//key[@func='moveSE']").InnerText);
			MOVE_S = SDL.SDL_GetKeyFromName(xDoc.SelectSingleNode("//key[@func='moveS']").InnerText);
			MOVE_SW = SDL.SDL_GetKeyFromName(xDoc.SelectSingleNode("//key[@func='moveSW']").InnerText);
			MOVE_W = SDL.SDL_GetKeyFromName(xDoc.SelectSingleNode("//key[@func='moveW']").InnerText);
			MOVE_NW = SDL.SDL_GetKeyFromName(xDoc.SelectSingleNode("//key[@func='moveNW']").InnerText);

			xDoc.Load(TILEFILE);
			TILEINFO = getResourcePath() + xDoc.SelectSingleNode("//tiles").Attributes["tileinfo"].InnerText;

			xDoc.Load(TILEINFO);
			TILESET = getResourcePath() + xDoc.SelectSingleNode("//tileInfo").Attributes["tileset"].InnerText;
			SET_WIDTH = Convert.ToInt16(xDoc.SelectSingleNode("//tileInfo/setWidth").InnerText);
			SET_HEIGHT = Convert.ToInt16(xDoc.SelectSingleNode("//tileInfo/setHeight").InnerText);

			TILE_WIDTH = Convert.ToInt16(xDoc.SelectSingleNode("//tileInfo/tileWidth").InnerText);
			TILE_HEIGHT = Convert.ToInt16(xDoc.SelectSingleNode("//tileInfo/tileHeight").InnerText);
			TILE_RENDER_WIDTH = TILE_WIDTH * 4;
			TILE_RENDER_HEIGHT = TILE_HEIGHT * 4;
		}

		public static string getBasePath() {
			if (BASE_PATH == null) {
				BASE_PATH = SDL2.SDL.SDL_GetBasePath();
			}
			return BASE_PATH;
		}

		public static string getResourcePath() {
			if (BASE_PATH == null) {
				getBasePath();
			}
			return BASE_PATH + "Resources" + getPathDelimiter();
		}

		public static char getPathDelimiter() {
			if (SDL2.SDL.SDL_GetPlatform().Equals("Windows")) {
				return '\\';
			} else {
				return '/';
			}
		}

		public static void write(string message) {
			OSTREAM.WriteLine(message);
		}

	}
}
