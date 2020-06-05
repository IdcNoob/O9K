namespace O9K.AIO.Heroes.PhantomLancer
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_phantom_lancer)]
    internal class PhantomLancerBase : BaseHero
    {
        public PhantomLancerBase(IContext9 context)
            : base(context)
        {
        }
    }
}