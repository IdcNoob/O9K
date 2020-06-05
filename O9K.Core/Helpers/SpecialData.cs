namespace O9K.Core.Helpers
{
    using System;
    using System.Globalization;
    using System.Linq;

    using Ensage;

    using Exceptions;

    using Logger;

    using Managers.Entity;

    public class SpecialData
    {
        private readonly Func<uint, float> getDataFunc;

        private readonly Ability talent;

        private readonly float talentValue;

        private readonly float[] value;

        public SpecialData(Entity talentOwner, AbilityId talentId)
        {
            try
            {
                this.talent = EntityManager9.BaseAbilities.FirstOrDefault(x => x.Id == talentId && x.Owner?.Handle == talentOwner.Handle);

                if (this.talent != null)
                {
                    this.talentValue = this.talent.AbilitySpecialData.First(x => x.Name == "value").Value;
                    this.getDataFunc = this.GetTalentValue;
                }
                else
                {
                    this.getDataFunc = _ => 1;
                }
            }
            catch
            {
                this.getDataFunc = _ => 0;

                var ex = new BrokenAbilityException(talentId.ToString());
                if (this.talent?.IsValid == true)
                {
                    ex.Data["Ability"] = new
                    {
                        Ability = this.talent.Name
                    };
                }

                Logger.Error(ex);
            }
        }

        public SpecialData(Ability ability, AbilityId talentId, string name)
        {
            try
            {
                var data = ability.AbilitySpecialData.First(x => x.Name == name);

                var unit = ability.Owner as Unit;
                if (unit != null)
                {
                    this.talent = unit.Spellbook.Spells.FirstOrDefault(x => x.Id == talentId);
                }

                if (this.talent != null)
                {
                    this.talentValue = this.talent.AbilitySpecialData.First(x => x.Name == "value").Value;
                    this.getDataFunc = this.GetValueWithTalent;
                }
                else
                {
                    this.getDataFunc = this.GetValueDefault;
                }

                this.value = new float[data.Count];

                for (var i = 0u; i < this.value.Length; i++)
                {
                    this.value[i] = data.GetValue(i);
                }
            }
            catch
            {
                this.getDataFunc = _ => 1;

                var ex = new BrokenAbilityException(ability.Name);
                if (ability.IsValid)
                {
                    ex.Data["Ability"] = new
                    {
                        Ability = ability.Name,
                        SpecialData = name,
                    };
                }

                Logger.Error(ex);
            }
        }

        public SpecialData(Ability ability, string name)
        {
            try
            {
                var data = ability.AbilitySpecialData.First(x => x.Name == name);
                var talentId = data.SpecialBonusAbility;

                if (talentId != AbilityId.ability_base)
                {
                    var unit = ability.Owner as Unit;
                    if (unit != null)
                    {
                        this.talent = unit.Spellbook.Spells.FirstOrDefault(x => x.Id == talentId);
                    }
                }

                if (this.talent != null)
                {
                    this.talentValue = this.talent.AbilitySpecialData.First(x => x.Name == "value").Value;
                    this.getDataFunc = this.GetValueWithTalent;
                }
                else
                {
                    this.getDataFunc = this.GetValueDefault;
                }

                if (data.Count == 0)
                {
                    this.value = ability.KeyValues.FindKeyValues(name)
                        .StringValue.Split()
                        .Select(x => float.Parse(x, CultureInfo.InvariantCulture))
                        .ToArray();
                }
                else
                {
                    this.value = new float[data.Count];

                    for (var i = 0u; i < this.value.Length; i++)
                    {
                        this.value[i] = data.GetValue(i);
                    }
                }
            }
            catch
            {
                this.getDataFunc = _ => 1;

                var ex = new BrokenAbilityException(ability.Name);
                if (ability.IsValid)
                {
                    ex.Data["Ability"] = new
                    {
                        Ability = ability.Name,
                        SpecialData = name,
                    };
                }

                Logger.Error(ex);
            }
        }

        public SpecialData(Ability ability, Func<uint, int> baseData)
        {
            try
            {
                this.value = new float[Math.Max(ability.MaximumLevel, 1)];

                for (var i = 0u; i < this.value.Length; i++)
                {
                    this.value[i] = baseData(i);
                }

                this.getDataFunc = this.GetValueDefault;
            }
            catch
            {
                this.getDataFunc = _ => 1;

                var ex = new BrokenAbilityException(ability.Name);
                if (ability.IsValid)
                {
                    ex.Data["Ability"] = new
                    {
                        Ability = ability.Name,
                        BaseSpecialData = baseData.Method.Name
                    };
                }

                Logger.Error(ex);
            }
        }

        public SpecialData(AbilityId abilityId, string key)
        {
            try
            {
                var abilityData = Ability.GetAbilityDataById(abilityId);
                var specialData = abilityData.AbilitySpecialData.FirstOrDefault(x => x.Name == key);

                if (specialData != null)
                {
                    this.value = new float[specialData.Count];

                    for (var i = 0u; i < this.value.Length; i++)
                    {
                        this.value[i] = specialData.GetValue(i);
                    }
                }
                else
                {
                    var keyData = abilityData.KeyValues.FindKeyValues(key).StringValue;
                    var stringValues = keyData.Split(' ');

                    this.value = new float[stringValues.Length];

                    for (var i = 0u; i < this.value.Length; i++)
                    {
                        this.value[i] = float.Parse(stringValues[i], CultureInfo.InvariantCulture);
                    }
                }

                this.getDataFunc = this.GetValueDefault;
            }
            catch
            {
                this.getDataFunc = _ => 0;
                var ex = new BrokenAbilityException(abilityId + "/" + key);
                Logger.Error(ex);
            }
        }

        public SpecialData(Ability ability, Func<uint, float> baseData)
        {
            try
            {
                this.value = new float[Math.Max(ability.MaximumLevel, 1)];

                for (var i = 0u; i < this.value.Length; i++)
                {
                    this.value[i] = baseData(i);
                }

                this.getDataFunc = this.GetValueDefault;
            }
            catch
            {
                this.getDataFunc = _ => 1;

                var ex = new BrokenAbilityException(ability.Name);
                if (ability.IsValid)
                {
                    ex.Data["Ability"] = new
                    {
                        Ability = ability.Name,
                        BaseSpecialData = baseData.Method.Name
                    };
                }

                Logger.Error(ex);
            }
        }

        // ReSharper disable once RedundantAssignment
        public float GetTalentValue(uint level)
        {
            level = this.talent.Level;

            if (level == 0)
            {
                return 0;
            }

            return this.talentValue;
        }

        public float GetValue(uint level)
        {
            return this.getDataFunc(level);
        }

        public float GetValueDefault(uint level)
        {
            if (level == 0)
            {
                return 0;
            }

            return this.value[Math.Min(level, this.value.Length) - 1];
        }

        public float GetValueWithTalent(uint level)
        {
            if (level == 0)
            {
                return 0;
            }

            var data = this.value[Math.Min(level, this.value.Length) - 1];

            if (this.talent?.Level > 0)
            {
                data += this.talentValue;
            }

            return data;
        }

        public float GetValueWithTalentMultiply(uint level)
        {
            if (level == 0)
            {
                return 0;
            }

            var data = this.value[Math.Min(level, this.value.Length) - 1];

            if (this.talent?.Level > 0)
            {
                data *= (this.talentValue / 100) + 1;
            }

            return data;
        }

        public float GetValueWithTalentMultiplySimple(uint level)
        {
            if (level == 0)
            {
                return 0;
            }

            var data = this.value[Math.Min(level, this.value.Length) - 1];

            if (this.talent?.Level > 0)
            {
                data *= this.talentValue;
            }

            return data;
        }

        public float GetValueWithTalentSubtract(uint level)
        {
            if (level == 0)
            {
                return 0;
            }

            var data = this.value[Math.Min(level, this.value.Length) - 1];

            if (this.talent?.Level > 0)
            {
                data -= this.talentValue;
            }

            return data;
        }
    }
}