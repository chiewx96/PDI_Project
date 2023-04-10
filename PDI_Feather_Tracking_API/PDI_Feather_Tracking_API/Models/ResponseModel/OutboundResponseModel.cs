using System.Diagnostics;

namespace PDI_Feather_Tracking_API.Models.ResponseModel
{
    public class OutboundResponseModel
    {
        public List<string> OutboundedReferenceNo { get; set; } = new List<string>();

        public List<string> NotExistsReferenceNo { get; set; } = new List<string>();
    }
}
