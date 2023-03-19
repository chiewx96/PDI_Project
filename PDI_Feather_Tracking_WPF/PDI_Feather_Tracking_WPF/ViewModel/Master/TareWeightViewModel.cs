using GalaSoft.MvvmLight;
using Microsoft.EntityFrameworkCore;
using PDI_Feather_Tracking_WPF.Global;
using PDI_Feather_Tracking_WPF.Models;
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
            set { childCount = value; RaisePropertyChanged(nameof(ChildCount)); }
        }

        private decimal tareWeight;

        public decimal TareWeight
        {
            get { return tareWeight; }
            set { tareWeight = value; RaisePropertyChanged(nameof(TareWeight)); }
        }

        FeatherDbContext? _context;

        public TareWeightViewModel() { }

        public TareWeightViewModel(FeatherDbContext context)
        {
            _context = context;
            ChildCount = _context.TareWeightSetting.AsNoTracking().First().ChildCount;
            TareWeight = _context.TareWeightSetting.AsNoTracking().First().Weight;
        }

        public void set(TareWeightSetting tareWeightSetting)
        {
            TareWeight = tareWeightSetting.Weight;
            ChildCount = tareWeightSetting.ChildCount;
        }

    }
}
