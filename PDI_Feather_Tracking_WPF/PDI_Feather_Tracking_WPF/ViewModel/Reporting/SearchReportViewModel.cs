using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Feather_Tracking_WPF.ViewModel
{
    internal class SearchReportViewModel
    {
		private int reportTypeId;

		public int ReportTypeID
		{
			get { return reportTypeId; }
			set { reportTypeId = value; }
		}

		private int skuTypeId;

		public int SkuTypeId
		{
			get { return skuTypeId; }
			set { skuTypeId = value; }
		}

		private DateTime? incomingFromDate;

		public DateTime? IncomingFromDate
		{
			get { return incomingFromDate; }
			set { incomingFromDate = value; }
		}

		private DateTime? incomingtoDate;

		public DateTime? IncomingToDate
		{
			get { return incomingtoDate; }
			set { incomingtoDate = value; }
		}

        private DateTime? outgoingFromDate;

        public DateTime? OutgoingFromDate
        {
            get { return outgoingFromDate; }
            set { outgoingFromDate = value; }
        }

        private DateTime? outgoingtoDate;

        public DateTime? OutgoingToDate
        {
            get { return outgoingtoDate; }
            set { outgoingtoDate = value; }
        }

    }
}
