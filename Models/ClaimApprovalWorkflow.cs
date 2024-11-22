namespace ContractMonthlyClaimSystem.Models
{
    public class ClaimApprovalWorkflow
    {
        public bool ValidateClaim(ClaimModel claim)
        {
            return Convert.ToDouble(claim.HOURS_WORKED) > 0 && Convert.ToDouble(claim.HOURLY_RATE) > 0;
        }
        public void ApproveClaim(ClaimModel claim)
        {
            if (ValidateClaim(claim))
            {
                claim.STATUS = "Approved";
            }
            else
            {
                claim.STATUS = "REJECTED";
            }
        }
    }
}
