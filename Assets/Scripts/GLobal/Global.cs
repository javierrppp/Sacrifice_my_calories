using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace global {
	public static class Global{

		public static int weight = Config.initialWeight;

		public static int throwWeight = -5;

        //生成到的层数
		public static int layer = 1;

        //玩家所达到的层数
        public static int reachLayer = 0;

        //在重生后，玩家的基层，用于计算，一般等于checkpointLayer，但是checkpointLayer会更新，这个只有死亡时才会更新
        public static int baseLayer = 0;

        //关卡检查层，保留当前到达的关卡数
		public static int checkpointLayer = 0;

        public static int score = 0;

        public static int heartNum = 3;

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

        //type为0表示吃食物增加的分数， type为1表示跳跃平台加的分数
        public static void addScore(int _score, int type)
        {
            if (type == 0)
            {
                score += _score;
            } else
            {
                score += 2 * _score;
            }
            ScoreController._instance.changeScore(score);
        }
	}
}
