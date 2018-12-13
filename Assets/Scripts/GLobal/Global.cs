using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace global {
	public static class Global{

		public static int weight = Config.initialWeight;

		public static int throwWeight = -5;

		public static int layer = 1;

		public static int checkpointLayer = 0;

		public static float getDeadTime() {
			return - 0.000075f * weight * weight + 10;
			//暂时用这个公式，后面再协调
		}

		public static float getJumpTakeOffSpeed() {
			return - 0.000125f * weight * weight + 10;
		}

		public static void throwMeat() {
			int theWeight = Global.weight + throwWeight;
			if (theWeight > Config.maxWeight) {
				theWeight = Config.maxWeight;
			} else if (theWeight < Config.minWeight) {
				theWeight = Config.minWeight;
			}
			Global.weight = theWeight;
		}

		public static void eatFood(int _weitht) {
			int theWeight = Global.weight + _weitht;
			if (theWeight > Config.maxWeight) {
				theWeight = Config.maxWeight;
			} else if (theWeight < Config.minWeight) {
				theWeight = Config.minWeight;
			}
			Global.weight = theWeight;
		}
	}
}
