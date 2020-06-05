namespace O9K.Core.Entities.Heroes.Unique
{
    using System;

    using Abilities.Base;
    using Abilities.Talents;

    using Ensage;

    using Helpers;

    using Logger;

    using Metadata;

    [HeroId(HeroId.npc_dota_hero_queenofpain)]
    internal class QueenOfPain : Hero9, IDisposable
    {
        private readonly Sleeper talentSleeper = new Sleeper();

        private SpellBlockTalent linkensSphereTalent;

        public QueenOfPain(Hero baseHero)
            : base(baseHero)
        {
        }

        public override bool IsLinkensProtected
        {
            get
            {
                return base.IsLinkensProtected || (this.linkensSphereTalent?.Level > 0 && !this.talentSleeper.IsSleeping);
            }
        }

        public void Dispose()
        {
            Entity.OnParticleEffectReleased -= this.OnParticleEffectReleased;
        }

        internal override void Ability(Ability9 ability, bool added)
        {
            base.Ability(ability, added);

            if (added && ability.Id == AbilityId.special_bonus_spell_block_15)
            {
                this.linkensSphereTalent = (SpellBlockTalent)ability;
                Entity.OnParticleEffectReleased += this.OnParticleEffectReleased;
            }
        }

        private void OnParticleEffectReleased(Entity sender, ParticleEffectReleasedEventArgs args)
        {
            try
            {
                if (sender == null || sender.Handle != this.Handle || args.ParticleEffect.Name != "particles/items_fx/immunity_sphere.vpcf")
                {
                    return;
                }

                if (this.linkensSphereTalent?.IsValid != true || this.linkensSphereTalent.Level <= 0)
                {
                    return;
                }

                this.talentSleeper.Sleep(this.linkensSphereTalent.SpellBlockCooldown);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}