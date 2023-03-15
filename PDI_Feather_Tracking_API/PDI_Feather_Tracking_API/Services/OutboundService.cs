using Microsoft.EntityFrameworkCore;
using PDI_Feather_Tracking_API.Models;
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

        public BooleanMessageModel Outbound(string referenceNo)
        {
            var result = dbContext.InventoryRecords.Where(x => x.BatchNo == referenceNo).FirstOrDefault();
            if (result != null)
            {
                result.OutgoingDateTime = DateTime.Now;
                //result.OutgoingPic = 
                dbContext.SaveChanges();
            }
            return new BooleanMessageModel(true, "saved");
        }
    }
}
