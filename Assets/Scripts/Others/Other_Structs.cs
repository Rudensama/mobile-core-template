using UnityEngine;

public struct Vector3Hex
{
    public int q;
    public int r;
    public int s;
    public bool valid;

    public Vector3Hex(int q, int r, int s)
    {
        this.q = q;
        this.r = r;
        this.s = s;
        this.valid = q + r + s == 0;
    }
}

