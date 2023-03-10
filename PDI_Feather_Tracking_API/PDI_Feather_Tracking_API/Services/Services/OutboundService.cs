using PDI_Feather_Tracking_API.Models;
using PDI_Feather_Tracking_API.Models.ResponseModel;

namespace PDI_Feather_Tracking_API.Services.Services
{
    public interface OutboundService
    {
        InventoryRecords? GetPackageDetailByReferenceNumber(string referenceNo);

        BooleanMessageModel Outbound(string referenceNo);
    }

}
