namespace O9K.Hud.Modules.Map.Predictions.LaneCreeps.LaneData
{
    using System.Collections.Generic;

    using Core.Entities.Units;
    using Core.Managers.Entity;

    using Ensage;

    using SharpDX;

    internal class LanePaths
    {
        private readonly Vector3[] direBotPath =
        {
            new Vector3(6239, 3717, 384),
            new Vector3(6166, 3097, 384),
            new Vector3(6407, 635, 384),
            new Vector3(6422, -816, 384),
            new Vector3(6477, -2134, 384),
            new Vector3(5995, -3054, 384),
            new Vector3(6071, -3601, 384),
            new Vector3(6127, -4822, 384),
            new Vector3(5460, -5893, 384),
            new Vector3(5070, -5915, 384),
            new Vector3(4412, -6048, 384),
            new Vector3(3388, -6125, 384),
            new Vector3(2641, -6135, 384),
            new Vector3(1320, -6374, 384),
            new Vector3(-174, -6369, 384),
            new Vector3(-1201, -6308, 384),
            new Vector3(-3036, -6072, 257)
        };

        private readonly Vector3[] direMidPath =
        {
            new Vector3(4101, 3652, 384),
            new Vector3(3549, 3041, 256),
            new Vector3(2639, 2061, 256),
            new Vector3(2091, 1605, 256),
            new Vector3(1091, 845, 256),
            new Vector3(-163, -62, 256),
            new Vector3(-789, -579, 178),
            new Vector3(-1327, -1017, 256),
            new Vector3(-2211, -1799, 256),
            new Vector3(-2988, -2522, 256),
            new Vector3(-4103, -3532, 256)
        };

        private readonly Vector3[] direTopPath =
        {
            new Vector3(3154, 5811, 384),
            new Vector3(2903, 5818, 299),
            new Vector3(-248, 5852, 384),
            new Vector3(-1585, 5979, 384),
            new Vector3(-3253, 5981, 384),
            new Vector3(-3587, 5954, 384),
            new Vector3(-3954, 5860, 384),
            new Vector3(-4988, 5618, 384),
            new Vector3(-5714, 5514, 384),
            new Vector3(-6051, 5092, 384),
            new Vector3(-6299, 4171, 384),
            new Vector3(-6342, 3806, 384),
            new Vector3(-6419, 2888, 384),
            new Vector3(-6434, -2797, 256)
        };

        private readonly Vector3[] radiantBotPath =
        {
            new Vector3(-3628, -6121, 384),
            new Vector3(-3138, -6168, 256),
            new Vector3(-2126, -6234, 256),
            new Vector3(-1064, -6383, 384),
            new Vector3(196, -6602, 384),
            new Vector3(1771, -6374, 384),
            new Vector3(3740, -6281, 384),
            new Vector3(5328, -5813, 384),
            new Vector3(5766, -5602, 384),
            new Vector3(6265, -4992, 384),
            new Vector3(6112, -3603, 384),
            new Vector3(6103, 1724, 256),
            new Vector3(6133, 2167, 256)
        };

        private readonly Vector3[] radiantMidPath =
        {
            new Vector3(-5000, -4458, 384),
            new Vector3(-4775, -4020, 384),
            new Vector3(-4242, -3594, 256),
            new Vector3(-3294, -2941, 256),
            new Vector3(-2771, -2454, 256),
            new Vector3(-2219, -1873, 256),
            new Vector3(-1193, -1006, 256),
            new Vector3(-573, -449, 128),
            new Vector3(-46, -49, 256),
            new Vector3(679, 461, 256),
            new Vector3(979, 692, 256),
            new Vector3(2167, 1703, 256),
            new Vector3(2919, 2467, 256),
            new Vector3(3785, 3132, 256)
        };

        private readonly Vector3[] radiantTopPath =
        {
            new Vector3(-6604, -3979, 384),
            new Vector3(-6613, -3679, 384),
            new Vector3(-6775, -3420, 384),
            new Vector3(-6743, -3042, 369),
            new Vector3(-6682, -2124, 256),
            new Vector3(-6640, -1758, 384),
            new Vector3(-6411, -226, 384),
            new Vector3(-6370, 2523, 384),
            new Vector3(-6198, 3997, 384),
            new Vector3(-6015, 4932, 384),
            new Vector3(-5888, 5204, 384),
            new Vector3(-5389, 5548, 384),
            new Vector3(-4757, 5740, 384),
            new Vector3(-4066, 5873, 384),
            new Vector3(-3068, 6009, 384),
            new Vector3(-2139, 6080, 384),
            new Vector3(-1327, 6052, 384),
            new Vector3(-82, 6011, 384),
            new Vector3(1682, 5993, 311),
            new Vector3(2404, 6051, 256)
        };

        public LanePaths()
        {
            if (EntityManager9.Owner.Team == Team.Radiant)
            {
                this.Lanes[LanePosition.Easy] = this.direTopPath;
                this.Lanes[LanePosition.Middle] = this.direMidPath;
                this.Lanes[LanePosition.Hard] = this.direBotPath;
            }
            else
            {
                this.Lanes[LanePosition.Easy] = this.radiantBotPath;
                this.Lanes[LanePosition.Middle] = this.radiantMidPath;
                this.Lanes[LanePosition.Hard] = this.radiantTopPath;
            }
        }

        public Dictionary<LanePosition, Vector3[]> Lanes { get; } = new Dictionary<LanePosition, Vector3[]>();

        public LanePosition GetCreepLane(Unit9 unit)
        {
            foreach (var lane in this.Lanes)
            {
                if (unit.Distance(lane.Value[0]) < 500)
                {
                    return lane.Key;
                }
            }

            return LanePosition.Unknown;
        }

        public Vector3[] GetLanePath(LanePosition lane)
        {
            return this.Lanes[lane];
        }
    }
}