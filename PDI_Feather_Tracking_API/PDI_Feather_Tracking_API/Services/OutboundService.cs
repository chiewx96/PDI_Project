using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
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

        public List<InventoryRecords>? GetUnassignContainerResults()
        {
            // return only those not cancelled
            return dbContext.InventoryRecords.AsNoTracking().Where(x => string.IsNullOrEmpty(x.OutgoingContainer) && x.CancelStatus == 0).ToList();
        }

        public void UpdateContainerIdForOutboundedItems(Dictionary<string, object> requestModel, string user_id)
        {
            string? container_id = requestModel["container_id"].ToString();
            List<InventoryRecords>? package_list = JsonConvert.DeserializeObject<List<InventoryRecords>>(requestModel["packages"].ToString());

            if (string.IsNullOrEmpty(container_id))
                throw new Exception("container id cannot be null.");
            else if (package_list == null || package_list.Count <= 0)
                throw new Exception("packages count cannot be empty.");

            foreach (var package_no in package_list)
            {
                var _package = dbContext.InventoryRecords.Where(z => z.Id == package_no.Id).FirstOrDefault();
                if (_package != null)
                {
                    _package.OutgoingContainer = container_id;
                    _package.UpdatedAt= DateTime.Now;
                    _package.UpdatedBy = int.Parse(user_id);
                }
            }
            dbContext.SaveChanges();
        }
    }
}
