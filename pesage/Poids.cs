using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pesage
{
    internal class Poids
    {
        private bool _isStable;
        private bool _isNet;
        private double _tare;
        private double _weight;
        public double Weight
        { get { return _weight; } set { _weight = value; } }
        public double Tare
        { get { return _tare; } set { _tare = value; } }
        public bool IsStable
        { get { return _isStable; } set { _isStable = value; } }
        public bool IsNet
        {
            get
            {
                return _isNet;
            }
            set
            {
                if (_isNet != value)
                {
                    _isNet = value;
                    if (value)
                    {
                        _tare = _weight;
                        _weight = 0;
                    }
                    else
                    {
                        _tare = 0;
                    }
                }
            }
        }

        public Poids()
        {
            this._isStable = false;
            this._isNet = false;
            this._tare = 0;
            this._weight = 0;
        }

        public Poids(double w, bool stable, bool net, double t)
        {
            this._isStable = stable;
            this._isNet = net;
            this._tare = t;
            this._weight = w;
        }
    }
}
