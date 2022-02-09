using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialYeelightController.Core;

namespace MaterialYeelightController.MVVM.Model
{
    internal class ColorFlowModel : ObservableObject
    {
        private int _speed;

        public int Speed
        {
            get { return _speed; }
            set
            {
                if (value != _speed)
                {
                    _speed = value;
                    OnPropertyChanged(nameof(Speed)); 
                }                
            }
        }

        private int _sleep;

        public int Sleep
        {
            get { return _sleep; }
            set
            {
                if (value != _sleep)
                {
                    _sleep = value;
                    OnPropertyChanged(nameof(Sleep));
                }
            }
        }


        private string _flowColor1;

        public string FlowColor1
        {
            get { return _flowColor1; }
            set
            {
                if (value != _flowColor1)
                {
                    _flowColor1 = value;
                    OnPropertyChanged(nameof(FlowColor1));
                }
            }
        }
        private string _flowColor2;

        public string FlowColor2
        {
            get { return _flowColor2; }
            set
            {
                if (value != _flowColor2)
                {
                    _flowColor2 = value;
                    OnPropertyChanged(nameof(FlowColor2));
                }
            }
        }


        private string _flowColor3;

        public string FlowColor3
        {
            get { return _flowColor3; }
            set
            {
                if (value != _flowColor3)
                {
                    _flowColor3 = value;
                    OnPropertyChanged(nameof(FlowColor3));
                }
            }
        }

        private string _flowColor4;

        public string FlowColor4
        {
            get { return _flowColor4; }
            set
            {
                if (value != _flowColor4)
                {
                    _flowColor4 = value;
                    OnPropertyChanged(nameof(FlowColor4));
                }
            }
        }
    }
}