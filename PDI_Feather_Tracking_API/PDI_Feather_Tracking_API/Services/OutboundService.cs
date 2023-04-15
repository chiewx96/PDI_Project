using Microsoft.EntityFrameworkCore;
using PDI_Feather_Tracking_API.Models;
using PDI_Feather_Tracking_API.Models.RequestModel;
using PDI_Feather_Tracking_API.Models.ResponseModel;

namespace PDI_Feather_Tracking_API.Services
{
    public class OutboundService
    {
        private PDIFeatherTrackingDbContext dbContext;

        public OutboundService(PDIFeatherTrackingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public InventoryRecords? GetPackageDetailByReferenceNumber(string referenceNo)
        {
            return dbContext.InventoryRecords.AsNoTracking().Where(x => x.BatchNo == referenceNo).FirstOrDefault();
        }

        public BooleanMessageModel Outbound(string referenceNo, string user_id)
        {
            var result = dbContext.InventoryRecords.Where(x => x.BatchNo == referenceNo).FirstOrDefault();
            if (result != null && result.OutgoingPic == 0)
            {
                result.OutgoingDateTime = DateTime.Now;
                result.OutgoingPic = int.Parse(user_id);
                dbContext.SaveChanges();
                return new BooleanMessageModel(true, "saved");
            }
            else if (result != null && result.OutgoingPic != 0)
            {
                return new BooleanMessageModel(false, "Batch number has been outbound.");
            }
            else
            {
                return new BooleanMessageModel(false, "Batch number not exists");
            }
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
    }
}
