using System;
using LorendisCore.Utilities;

namespace LorendisCore.Common.Stats
{
    public class ComplexStat : IStatObject
    {
        private readonly string _name;
        private int _val;
        private int _mod;
        private int _max;
        private int _exh;

        public ComplexStat(string name, int value, int mod, int max, int exhaustion)
        {
            _name = Validate.ThrowIfNullOrWhitespace(name, nameof(name));
            _val = Validate.ThrowIfNegative(value, nameof(value));
            _mod = mod;
            _max = Validate.ThrowIfNegative(max, nameof(max));
            _exh = Validate.ThrowIfNegative(exhaustion, nameof(exhaustion));
        }

        /// <summary>
        /// Gets the effective maximum the stat can be raised to, including modifier and exhaustion.
        /// </summary>
        public int GetTotal() 
            => MathUtil.NonNeg(_max + _mod - _exh);

        /// <summary>
        /// Gets the maximum the stat can be raised to, including modifier but <i>not</i> exhaustion.
        /// </summary>
        public int GetModifiedMaximum() 
            => MathUtil.NonNeg(_max + _mod);

        public string GetStatName() 
            => _name;
        
        public int GetValue() 
            => _val;

        /// <summary>
        /// Gets the effective value of the stat.
        /// Cannot be greater than the maximum.
        /// </summary>
        public int GetEffectiveValue() 
            => Math.Min(GetTotal(), _val);

        /// <summary>
        /// Sets the effective value of the stat.
        /// Since there is no modifier applied to the stat but rather the total,
        /// this is equivalent to <see cref="SetValue"/>.
        /// </summary>
        public void SetEffectiveValue(int val) 
            => SetValue(val);

        /// <summary>
        /// Sets the value of the stat.
        /// Cannot be greater than the maximum, including modifier and exhaustion.
        /// </summary>
        public void SetValue(int val) 
            => _val = MathUtil.NonNeg(Math.Min(GetTotal(), val));

        /// <summary>
        /// Modifies the value of the stat.
        /// Result will not be greater than the maximum, including modifier and exhaustion.
        /// </summary>
        public void AddValue(int add) 
            => SetValue(_val + add);

        public int GetModifier() 
            => _mod;
        public void SetModifier(int val) 
            => _mod = val;
        public void AddModifier(int add) 
            => _mod += add;

        public int GetMaximum() 
            => _max;

        /// <summary>
        /// Sets the maximum value and then ensures the stat's value is below the maximum.
        /// </summary>
        public void SetMaximum(int val)
        {
            _max = MathUtil.NonNeg(val);
            SetValue(_val); // ensure is under total
        }
        
        /// <summary>
        /// Modifies the maximum value and then ensures the stat's value is below the maximum.
        /// </summary>
        public void AddMax(int add) 
            => SetMaximum(_max + add);

        public int GetExhaustion() => _exh;
        
        /// <summary>
        /// Sets the exhaustion. Cannot be negative.
        /// </summary>
        public void SetExhaustion(int val) 
            => _exh = MathUtil.NonNeg(val);
        
        /// <summary>
        /// Modifies the exhaustion. Result will not be less than zero.
        /// </summary>
        public void AddExhaustion(int add) 
            => SetExhaustion(_exh + add);
    }
}
