using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Levels : MonoBehaviour {

	public class Config
	{
		public int[][][] enemies = new int[][][]
		{
			new int[][] //level1
			{
				new int[] //wave1
				{
					0,0,0,0,0
				},
				new int[]	//wave2
				{
					0,0,0,0,0,0,0
				},
				new int[]	//wave3
				{
					0,0,0,0,0,0,3,3
				}
			},
			new int[][] //level2
			{
				new int[] //wave1
				{
					0,0,0,0,0,0,0
				},
				new int[]	//wave2
				{
					0,0,3,0,0,3,3
				},
				new int[]	//wave3
				{
					0,3,3,3,3,3,3,0,0
				}
			},
			new int[][] //level3
			{
				new int[] //wave1
				{
					0,1,0,1,0,2,0,1,0,1
				},
				new int[]	//wave2
				{
					0,1,0,1,0,2,0,1,0,1
				},
					new int[]	//wave3
				{
					0,1,0,1,0,2,0,1,0,1
				}
			},
			new int[][] //level4
			{
				new int[] //wave1
				{
					1,1,1,1,1,1
				},
				new int[]	//wave2
				{
					1,4,1,4,1,4,1,4
				},
					new int[]	//wave3
				{
					1,1,1,4,4,4,5,5,5
				}
			}
		};
		
		
		public int[][][] enemiesPaths = new int[][][]
		{
			new int[][] //level1
			{
				new int[] //wave1
				{
					0,0,0,0,0
				},
				new int[]	//wave2
				{
					0,0,0,0,0,0,0
				},
				new int[]	//wave3
				{
					0,0,0,0,0,0,0,0
				}
			},
			new int[][] //level2
			{
				new int[] //wave1
				{
					0,0,0,0,0,0,0
				},
				new int[]	//wave2
				{
					0,0,0,0,0,0,0
				},
				new int[]	//wave3
				{
					0,0,0,0,0,0,0,0,0
				}
			},
			new int[][] //level3
			{
				new int[] //wave1
				{
					0,1,0,1,0,1,1,1,0,0
				},
				new int[]	//wave2
				{
					0,1,0,1,0,1,0,1,0,1
				},
					new int[]	//wave3
				{
					0,1,0,1,0,1,0,1,0,1
				}
			},
			new int[][] //level4
			{
				new int[] //wave1
				{
					0,0,0,0,0,0	
				},
				new int[]	//wave2
				{
					0,1,0,1,0,1,0,1
				},
					new int[]	//wave3
				{
					0,1,1,1,0,0,0,1,0
				}
			}
		};
		
		public int[][] enemiesNotOnLevel = new int[][]
		{
			new int[] //level1
				{
					3,4,6
				},
			new int[]	//level2
			{
				4,5,6
			},
			new int[]	//level3
			{
				4,5,6
			},
			new int[]	//level4
			{
				0,3,2
			}
		};
		public int[][] enemiesOnLevel = new int[][]
		{
			new int[] //level1
			{
				1,2,5
			},
			new int[]	//level2
			{
				1,2,3
			},
			new int[]	//level3
			{
				1,2,3
			},
			new int[]	//level4
			{
				1,4,5
			}
		};
	}
	
	public static Config config = new Config();
	void Start () 
	{
	
	}
	

	void Update () 
	{
	
	}
}
