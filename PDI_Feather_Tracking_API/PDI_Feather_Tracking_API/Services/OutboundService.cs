using Microsoft.EntityFrameworkCore;
using PDI_Feather_Tracking_API.Models;
using PDI_Feather_Tracking_API.Models.RequestModel;
using PDI_Feather_Tracking_API.Models.ResponseModel;
using System.Reflection.Metadata.Ecma335;

namespace PDI_Feather_Tracking_API.Services
{
    public class OutboundService
    {
        private PDIFeatherTrackingDbContext dbContext;

        public OutboundService(PDIFeatherTrackingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public OutboundResponseModel Outbound(OutboundRequestModel requestModel, string user_id)
        {
            OutboundResponseModel responseModel = new OutboundResponseModel();
            foreach (var referenceNo in requestModel.PackageReferenceNo)
            {
                var result = dbContext.InventoryRecords.Where(x => x.BatchNo == referenceNo).FirstOrDefault();
                if (result != null && result.OutgoingPic == 0)
                {
                    result.OutgoingDateTime = DateTime.Now;
                    result.OutgoingPic = int.Parse(user_id);
                    result.OutgoingContainer = requestModel.ContainerId;
                    dbContext.SaveChanges();
                }
                else if (result != null && result.OutgoingPic != 0)
                {
                    responseModel.OutboundedReferenceNo.Add(referenceNo);
                }
                else
                {
                    responseModel.NotExistsReferenceNo.Add(referenceNo);
                }
            }
            return responseModel;
        }

        public void cancelOutbound(string batch_no)
        {
            InventoryRecords? inventoryRecord = dbContext.InventoryRecords.Where(z => z.BatchNo == batch_no).FirstOrDefault();
            if (inventoryRecord == null)
                throw new KeyNotFoundException();
            else if (string.IsNullOrEmpty(inventoryRecord.OutgoingContainer))
                throw new Exception("The provided batch no has not been outbound yet.");
            else
            {
                inventoryRecord.CancelStatus = 1;
                dbContext.SaveChanges();
            }
        }
    }
}
