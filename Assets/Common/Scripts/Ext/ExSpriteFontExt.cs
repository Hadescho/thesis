using UnityEngine;

public static class ExSpriteFontExt
{
	public static void SetAlpha(this exSpriteFont exf, float alpha)
	{
		Color c;

		c = exf.topColor;
		c.a = alpha;
		exf.topColor = c;

		c = exf.botColor;
		c.a = alpha;
		exf.botColor = c;

		c = exf.outlineColor;
		c.a = alpha;
		exf.outlineColor = c;
	}
}