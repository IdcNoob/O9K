namespace O9K.AIO.Heroes.Enigma
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_enigma)]
    internal class EnigmaBase : BaseHero
    {
        public EnigmaBase(IContext9 context)
            : base(context)
        {
        }
    }
}