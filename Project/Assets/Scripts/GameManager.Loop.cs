using Data;

using UnityEngine;

using View;

public partial class GameManager : MonoBehaviour
{
    private void Tick()
    {
        this.TickStep_Grow();

        this.TickStep_Propagate();

        this.TickStep_Life();

        this.TickStep_OrganicCreation();
    }

    private void TickStep_Grow()
    {
        // TODO
    }

    private void TickStep_Propagate()
    {
        // TODO
    }

    private void TickStep_Life()
    {
        // TODO
    }

    private void TickStep_OrganicCreation()
    {
        // TODO
    }
}
