namespace DcProcurement
{
    public class BidProcessAttachment
    {
        public int Id { get; set; }
        public int? SubmittedBidId { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
    }
}