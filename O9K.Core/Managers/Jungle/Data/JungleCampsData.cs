namespace O9K.Core.Managers.Jungle.Data
{
    using Camp;

    using Ensage;

    using SharpDX;

    internal class JungleCampsData
    {
        public JungleCamp[] GetJungleCamps()
        {
            return new[]
            {
                // radiant
                new JungleCamp
                {
                    Id = 1,
                    Team = Team.Radiant,
                    IsMedium = true,
                    CreepsPosition = new Vector3(-3984, 1291, 384),
                    DrawPosition = new Vector3(-4349, 1206, 255),
                    StackTime = 54
                },
                new JungleCamp
                {
                    Id = 2,
                    Team = Team.Radiant,
                    IsLarge = true,
                    CreepsPosition = new Vector3(-4945, -417, 384),
                    DrawPosition = new Vector3(-5091, -51, 384),
                    StackTime = 56,
                },
                new JungleCamp
                {
                    Id = 3,
                    Team = Team.Radiant,
                    IsLarge = true,
                    CreepsPosition = new Vector3(-2543, -583, 256),
                    DrawPosition = new Vector3(-2821, -782, 256),
                    StackTime = 55,
                },
                new JungleCamp
                {
                    Id = 4,
                    Team = Team.Radiant,
                    IsMedium = true,
                    CreepsPosition = new Vector3(-122, -2094, 256),
                    DrawPosition = new Vector3(-98, -1803, 256),
                    StackTime = 55,
                },
                new JungleCamp
                {
                    Id = 5,
                    Team = Team.Radiant,
                    IsLarge = true,
                    CreepsPosition = new Vector3(-1819, -4235, 256),
                    DrawPosition = new Vector3(-2235, -4266, 256),
                    StackTime = 55,
                },
                new JungleCamp
                {
                    Id = 6,
                    Team = Team.Radiant,
                    IsMedium = true,
                    CreepsPosition = new Vector3(-364, -3360, 384),
                    DrawPosition = new Vector3(-340, -3045, 384),
                    StackTime = 54,
                },
                new JungleCamp
                {
                    Id = 7,
                    Team = Team.Radiant,
                    IsAncient = true,
                    CreepsPosition = new Vector3(1272, -5322, 384),
                    DrawPosition = new Vector3(887, -5406, 384),
                    StackTime = 55,
                },
                new JungleCamp
                {
                    Id = 8,
                    Team = Team.Radiant,
                    IsSmall = true,
                    CreepsPosition = new Vector3(3010, -4452, 256),
                    DrawPosition = new Vector3(2835, -4632, 256),
                    StackTime = 55,
                },
                new JungleCamp
                {
                    Id = 9,
                    Team = Team.Radiant,
                    IsLarge = true,
                    CreepsPosition = new Vector3(4832, -4214, 256),
                    DrawPosition = new Vector3(4644, -4410, 256),
                    StackTime = 55,
                },

                // dire
                new JungleCamp
                {
                    Id = 10,
                    Team = Team.Dire,
                    IsLarge = true,
                    CreepsPosition = new Vector3(-4321, 3457, 256),
                    DrawPosition = new Vector3(-4739, 3476, 256),
                    StackTime = 54,
                },
                new JungleCamp
                {
                    Id = 11,
                    Team = Team.Dire,
                    IsSmall = true,
                    CreepsPosition = new Vector3(-2454, 4814, 256),
                    DrawPosition = new Vector3(-2499, 5133, 256),
                    StackTime = 55,
                },
                new JungleCamp
                {
                    Id = 12,
                    Team = Team.Dire,
                    IsMedium = true,
                    CreepsPosition = new Vector3(-579, 5279, 384),
                    DrawPosition = new Vector3(-282, 5355, 384),
                    StackTime = 55,
                },
                new JungleCamp
                {
                    Id = 13,
                    Team = Team.Dire,
                    IsLarge = true,
                    CreepsPosition = new Vector3(-57, 3742, 384),
                    DrawPosition = new Vector3(-68, 3391, 384),
                    StackTime = 54,
                },
                new JungleCamp
                {
                    Id = 14,
                    Team = Team.Dire,
                    IsMedium = true,
                    CreepsPosition = new Vector3(-918, 2233, 384),
                    DrawPosition = new Vector3(-849, 1990, 384),
                    StackTime = 55,
                },
                new JungleCamp
                {
                    Id = 15,
                    Team = Team.Dire,
                    IsLarge = true,
                    CreepsPosition = new Vector3(1173, 3377, 256),
                    DrawPosition = new Vector3(1300, 3707, 302),
                    StackTime = 53,
                },
                new JungleCamp
                {
                    Id = 16,
                    Team = Team.Dire,
                    IsMedium = true,
                    CreepsPosition = new Vector3(2094, -318, 256),
                    DrawPosition = new Vector3(2214, -53, 256),
                    StackTime = 55,
                },
                new JungleCamp
                {
                    Id = 17,
                    Team = Team.Dire,
                    IsLarge = true,
                    CreepsPosition = new Vector3(4488, 844, 384),
                    DrawPosition = new Vector3(4336, 1176, 384),
                    StackTime = 55,
                },
                new JungleCamp
                {
                    Id = 18,
                    Team = Team.Dire,
                    IsAncient = true,
                    CreepsPosition = new Vector3(4233, -291, 384),
                    DrawPosition = new Vector3(4060, -3, 384),
                    StackTime = 55,
                },
            };
        }
    }
}