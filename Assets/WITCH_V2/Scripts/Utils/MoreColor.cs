using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class MoreColor
{
    public static Color Transparent(Color color, float alpha)
    {
        Color c = color;
        c.a = alpha;
        return c;
    }
}
