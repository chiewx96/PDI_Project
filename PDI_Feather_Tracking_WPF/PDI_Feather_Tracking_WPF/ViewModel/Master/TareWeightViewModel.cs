using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Feather_Tracking_WPF.ViewModel
{
    public class TareWeightViewModel : ViewModelBase
    {
		private int childCount;

		public int ChildCount
		{
			get { return childCount; }
			set { childCount = value; }
		}

		private decimal tareWeight;

		public decimal TareWeight
		{
			get { return tareWeight; }
			set { tareWeight = value; }
		}

	}
}
