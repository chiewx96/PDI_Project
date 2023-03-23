namespace PDI_Feather_Tracking_API.Models.ResponseModel
{
    public class BooleanMessageModel
    {
        public bool status { get; set; }
        public object message { get; set; }

        public BooleanMessageModel(bool status, object message)
        {
            this.status = status;
            this.message = message; 
        }
    }
}
