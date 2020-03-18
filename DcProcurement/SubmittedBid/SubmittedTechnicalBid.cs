namespace DcProcurement
{
    public class SubmittedTechnicalBid
    {
        public int Id { get; set; }
        public int? SubmittedBidId { get; set; }
        public int? RequisitionItemId { get; set; }



        public SubmittedBid SubmittedBid { get; set; }
        public RequisitionItem RequisitionItem { get; set; }
    }
}