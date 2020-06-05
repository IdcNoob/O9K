namespace O9K.ItemManager.Modules.OrderHelper
{
    using System.ComponentModel.Composition;

    internal interface IOrderSync
    {
        bool ForceNextOrderManual { get; set; }

        bool IgnoreSoulRingOrder { get; set; }
    }

    [Export(typeof(IOrderSync))]
    internal class OrderSync : IOrderSync
    {
        public bool ForceNextOrderManual { get; set; }

        public bool IgnoreSoulRingOrder { get; set; }
    }
}