
namespace LorendisCore.Common.Damage
{
    public class InstantDamage : Damage
    {
        private bool _isDone = false;
        protected override bool IsDoneCondition() => _isDone;
        
        /// <summary>
        /// Creates an instant damage event with a duration of 0, which is to be 
        /// </summary>
        public InstantDamage(DamageType type, int amount)
            : base(type, amount, 0) { }
        
        protected override int CalculateTick()
        {
            _isDone = true;
            return Total;
        }

        public override string ToDataString()
        {
            return $"[({Total}){Type.GetName()}]";
        }

        protected override string ChildInfoString()
        {
            return $"{Total} instant {Type.GetName()} damage";
        }
    }
}
