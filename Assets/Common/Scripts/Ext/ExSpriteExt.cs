using UnityEngine;

public static class ExSpriteExt
{
	public static void SetSprite(this exSprite exs, string name, bool changeDefaultAnimSprite = false)
	{
		exs.SetSprite(exs.atlas, exs.atlas.GetIndexByName(name), changeDefaultAnimSprite);
	}

	public static void SetAlpha(this exSprite exs, float alpha)
	{
		Color c = exs.color;
		c.a = alpha;
		exs.color = c;
	}
}