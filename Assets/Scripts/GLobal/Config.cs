using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace global {
	public class Config {

		public enum food {

			CABBAGE,

			BANANA,

			APPLE,

			CANDY,

			MILK_TEA,

			COLA,

			CHOCOLATE,

			CHICKEN_LEG,

			BEEF,

			BAMBURGER,

			POT
		};

		public static Dictionary<food, int> weights = new Dictionary<food, int> {
			{food.CABBAGE, -5},

			{food.BANANA, - 15},

			{food.APPLE, -10},

			{food.CANDY, 3},

			{food.MILK_TEA, 5},

			{food.COLA, 10},

			{food.CHOCOLATE, 12},

			{food.CHICKEN_LEG, 15},

			{food.BEEF, 20},

			{food.BAMBURGER, 25},

			{food.POT, 50}
		};

		public static int checkPointPerPlatform = 30;

        public static int initialWeight = 200;

        public static int minWeight = 50;

        public static int maxWeight = 220;
    }
}
