using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace global {
	public class FoodFactory : MonoBehaviour {

		//巧克力
		public GameObject chocolate;

		//大白菜
		public GameObject cabbage;

		//牛排
		public GameObject beef;

		//奶茶
		public GameObject milkTea;

		//鸡腿
		public GameObject chickenLeg;

		//可乐
		public GameObject cola;

		//糖果
		public GameObject candy;

		//香蕉
		public GameObject banana;

		//苹果
		public GameObject apple;

		//汉堡
		public GameObject hamburger;

		//相扑火锅
		public GameObject pot;

        private static FoodFactory foodFactory;

        public static FoodFactory _instance
        {
            get
            {
                if (!foodFactory)
                {
                    foodFactory = FindObjectOfType(typeof(FoodFactory)) as FoodFactory;

                    if (!foodFactory)
                    {
                        GameObject _foodFactory = new GameObject();
                        foodFactory = _foodFactory.AddComponent<FoodFactory>();
                    }
                    else
                    {
                        foodFactory.Init();
                    }
                }

                return foodFactory;
            }
        }

        private void Init()
        {

        }

        //相扑火锅不在这里生成
        public GameObject generateAnFood() {
			List<GameObject> foodList = new List<GameObject> () {
				chocolate,
				cabbage,
				beef,
				milkTea,
				chickenLeg,
				cola,
				candy,
				banana,
				apple,
				hamburger
			};

			int value = Random.Range (0, foodList.Count);
			GameObject theFood = Instantiate (foodList [value]);
			return theFood;
		}

		public GameObject generatePot() {
			GameObject thePot = Instantiate (pot);
			return thePot;
		}
	}
}
