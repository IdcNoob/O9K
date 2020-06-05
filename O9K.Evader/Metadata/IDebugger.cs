namespace O9K.Evader.Metadata
{
    using Evader.EvadeModes;

    internal interface IDebugger
    {
        void AddEvadeResult(EvadeResult evadeResult);
    }
}