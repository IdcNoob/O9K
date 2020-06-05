namespace O9K.Core.Entities.Abilities.Heroes.Invoker.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base;

    using BaseAbilities;

    using Ensage;

    using Entities.Units;

    using Managers.Entity;

    internal class InvokeHelper<T>
        where T : ActiveAbility, IInvokableAbility
    {
        private readonly T invokableAbility;

        private readonly List<ActiveAbility> myOrbs = new List<ActiveAbility>();

        private readonly Dictionary<string, AbilityId> orbModifiers = new Dictionary<string, AbilityId>(3);

        private Invoke invoke;

        private float invokeTime;

        private Unit9 owner;

        public InvokeHelper(T ability)
        {
            this.invokableAbility = ability;
        }

        public Exort Exort { get; private set; }

        public bool IsInvoked
        {
            get
            {
                if (!this.invokableAbility.BaseAbility.IsHidden)
                {
                    return true;
                }

                return (this.invokeTime + 0.5f) > Game.RawGameTime;
            }
        }

        public Quas Quas { get; private set; }

        public Wex Wex { get; private set; }

        public bool CanInvoke(bool checkAbilityManaCost)
        {
            if (this.IsInvoked)
            {
                return true;
            }

            if (this.invoke?.CanBeCasted() != true)
            {
                return false;
            }

            if (checkAbilityManaCost && this.owner.Mana < (this.invoke.ManaCost + this.invokableAbility.ManaCost))
            {
                return false;
            }

            return true;
        }

        public bool Invoke(List<AbilityId> currentOrbs, bool queue, bool bypass)
        {
            if (this.IsInvoked)
            {
                return true;
            }

            if (this.invoke?.CanBeCasted() != true)
            {
                return false;
            }

            var orbs = currentOrbs ?? this.owner.BaseUnit.Modifiers.Where(x => !x.IsHidden && this.orbModifiers.ContainsKey(x.Name))
                           .Select(x => this.orbModifiers[x.Name])
                           .ToList();

            foreach (var id in this.GetMissingOrbs(orbs))
            {
                var orb = this.myOrbs.FirstOrDefault(x => x.BaseAbility.Id == id && x.CanBeCasted());
                if (orb == null)
                {
                    return false;
                }

                if (!orb.UseAbility(queue, bypass))
                {
                    return false;
                }
            }

            var invoked = this.invoke.UseAbility(queue, bypass);
            if (invoked)
            {
                this.invokeTime = Game.RawGameTime;
            }

            return invoked;
        }

        public void SetOwner(Unit9 newOwner)
        {
            this.owner = newOwner;

            var wexAbility = newOwner.GetAbilityById(AbilityId.invoker_wex)
                             ?? ObjectManager.GetEntitiesFast<Ability>().FirstOrDefault(x => x.Id == AbilityId.invoker_wex);

            if (wexAbility != null)
            {
                this.Wex = (Wex)EntityManager9.AddAbility(wexAbility);
                this.orbModifiers.Add(this.Wex.ModifierName, this.Wex.Id);
                this.myOrbs.Add(this.Wex);
            }

            var quasAbility = newOwner.GetAbilityById(AbilityId.invoker_quas)
                              ?? ObjectManager.GetEntitiesFast<Ability>().FirstOrDefault(x => x.Id == AbilityId.invoker_quas);

            if (quasAbility != null)
            {
                this.Quas = (Quas)EntityManager9.AddAbility(quasAbility);
                this.orbModifiers.Add(this.Quas.ModifierName, this.Quas.Id);
                this.myOrbs.Add(this.Quas);
            }

            var exortAbility = newOwner.GetAbilityById(AbilityId.invoker_exort)
                               ?? ObjectManager.GetEntitiesFast<Ability>().FirstOrDefault(x => x.Id == AbilityId.invoker_exort);

            if (exortAbility != null)
            {
                this.Exort = (Exort)EntityManager9.AddAbility(exortAbility);
                this.orbModifiers.Add(this.Exort.ModifierName, this.Exort.Id);
                this.myOrbs.Add(this.Exort);
            }

            var invokeAbility = newOwner.GetAbilityById(AbilityId.invoker_invoke);
            if (invokeAbility != null)
            {
                this.invoke = (Invoke)EntityManager9.AddAbility(invokeAbility);
            }
        }

        private IEnumerable<AbilityId> GetMissingOrbs(List<AbilityId> castedOrbs)
        {
            var orbs = castedOrbs.ToList();
            var missing = this.invokableAbility.RequiredOrbs.Where(x => !orbs.Remove(x)).ToList();

            if (missing.Count == 0)
            {
                return Enumerable.Empty<AbilityId>();
            }

            castedOrbs.RemoveRange(0, Math.Max((castedOrbs.Count - this.invokableAbility.RequiredOrbs.Length) + missing.Count, 0));
            castedOrbs.AddRange(missing);

            return missing.Concat(this.GetMissingOrbs(castedOrbs));
        }
    }
}