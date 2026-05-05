public class BigLeverModel
{
    public bool isUsed = false;
    public bool firstHit = true;

    public void Toggle()
    {
        if (!isUsed)
        {
            firstHit = false;
            isUsed = true;
        }
        else
        {
            isUsed = !isUsed;
        }
    }
}
