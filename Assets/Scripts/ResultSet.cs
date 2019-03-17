public class ResultSet
{

    public int Index1, Index2, Index3;
    public ResultSet(int index1, int index2, int index3)
    {
        Index1 = index1;
        Index2 = index2;
        Index3 = index3;
    }

    public bool IndexExits(int index)
    {
        if (index == Index1 || index == Index2 || index == Index3)
        {
            return true;
        }
        return false;

    }

}
