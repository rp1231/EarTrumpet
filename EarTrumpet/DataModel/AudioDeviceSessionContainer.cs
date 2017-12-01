﻿using SoundControlAPI_Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EarTrumpet.DataModel
{
    public class AudioDeviceSessionContainer : IAudioDeviceSession
    {
        List<IAudioDeviceSession> _sessions = new List<IAudioDeviceSession>();

        public AudioDeviceSessionContainer(IAudioDeviceSession session)
        {
            GroupingParam = session.GroupingParam; // can change at runtime.

            AddSession(session);
        }

        private void Session_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }

        public IEnumerable<IAudioDeviceSession> Sessions => _sessions;

        public void AddSession(IAudioDeviceSession session)
        {
            _sessions.Add(session);

            session.PropertyChanged += Session_PropertyChanged;
        }

        public void RemoveSession(IAudioDeviceSession session)
        {
            session.PropertyChanged -= Session_PropertyChanged;
            _sessions.Remove(session);
        }

        public uint BackgroundColor => _sessions[0].BackgroundColor;

        public string DisplayName => _sessions[0].DisplayName;

        public Guid GroupingParam { get; private set; }

        public string IconPath => _sessions[0].IconPath;

        public string Id => _sessions[0].Id;

        public bool IsDesktopApp => _sessions[0].IsDesktopApp;

        public bool IsHidden => _sessions[0].IsHidden;

        public bool IsMuted { get => _sessions[0].IsMuted; set => _sessions.ForEach(s => s.IsMuted = value); }

        public bool IsSystemSoundsSession => _sessions[0].IsSystemSoundsSession;

        public float PeakValue => _sessions[0].PeakValue;

        public int ProcessId => _sessions[0].ProcessId;

        public _AudioSessionState State => _sessions[0].State;

        public float Volume { get => _sessions[0].Volume; set => _sessions.ForEach(s => s.Volume = value); }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
