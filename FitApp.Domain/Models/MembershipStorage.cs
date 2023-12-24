namespace FitApp.Domain.Models
{
    public class MembershipStorage
    {
        public Membership Membership { get; private set; }
        public uint Quantity { get; set; }

        public MembershipStorage(Membership membership, uint quantity)
        {
            Membership = membership;
            Quantity = quantity;
        }
    }
}
