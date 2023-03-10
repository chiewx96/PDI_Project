namespace PDI_Feather_Tracking_API.Models.ResponseModel
{
    public class BooleanMessageModel
    {
        public bool status { get; set; }
        public string message { get; set; }

        public BooleanMessageModel(bool status, string message)
        {
            this.status = status;
            this.message = message; 
        }
    }
}
