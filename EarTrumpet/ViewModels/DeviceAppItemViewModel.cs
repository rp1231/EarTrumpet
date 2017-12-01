﻿using System;
using EarTrumpet.DataModel;
using EarTrumpet.Extensions;

namespace EarTrumpet.ViewModels
{
    public class DeviceAppItemViewModel : BindableBase
    {
        private readonly VirtualDefaultAudioDevice _device;

        public string Id => _device.Id;
        public float PeakValue => _device.PeakValue;
        public double IconHeight { get; set; }
        public double IconWidth { get; set; }

        public int Volume
        {
            get
            {
                return _device.Volume.ToVolumeInt();
            }
            set
            {
                _device.Volume = value/100f;
                RaisePropertyChanged("Volume");
            }
        }

        public bool IsMuted
        {
            get
            {
                return _device.IsMuted;
            }
            set
            {
                if (_device.IsMuted != value)
                {
                    _device.IsMuted = value;
                    RaisePropertyChanged("IsMuted");
                }
            }
        }

        public DeviceAppItemViewModel(VirtualDefaultAudioDevice device)
        {
            IconHeight = IconWidth = 32;

            _device = device;
            _device.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == "IsMuted" ||
                    e.PropertyName == "Volume")
                {
                    RaisePropertyChanged(e.PropertyName);
                }
            };
        }

        public bool IsSame(DeviceAppItemViewModel other)
        {
            return other.Id == Id;
        }

        internal void TriggerPeakCheck()
        {
            RaisePropertyChanged("PeakValue");
        }
    }
}
