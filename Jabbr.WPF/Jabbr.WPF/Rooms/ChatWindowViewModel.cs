﻿using System.Collections.Generic;
using Caliburn.Micro;
using JabbR.Client;
using Jabbr.WPF.Infrastructure;
using Jabbr.WPF.Infrastructure.Services;

namespace Jabbr.WPF.Rooms
{
    public class ChatWindowViewModel : Conductor<RoomViewModel>.Collection.OneActive
    {
        private readonly JabbRClient _client;
        private readonly RoomService _roomService;
        private readonly ServiceLocator _serviceLocator;
        private IEnumerable<RoomViewModel> _availableRooms;
        private RoomViewModel _selectedRoom;
        private string _sendText;

        public ChatWindowViewModel(RoomService roomService, ServiceLocator serviceLocator, JabbRClient client)
        {
            _roomService = roomService;
            _serviceLocator = serviceLocator;
            _client = client;

            Initialize();
        }

        public string SendText
        {
            get { return _sendText; }
            set
            {
                if (_sendText == value)
                    return;

                _sendText = value;
                NotifyOfPropertyChange(() => SendText);
            }
        }

        public IEnumerable<RoomViewModel> AvailableRooms
        {
            get { return _availableRooms; }
        }

        public RoomViewModel SelectedRoom
        {
            get { return _selectedRoom; }
            set
            {
                if (_selectedRoom == value)
                    return;

                _selectedRoom = value;
                NotifyOfPropertyChange(() => SelectedRoom);
            }
        }

        public void OnRoomSelected()
        {
            if (SelectedRoom == null)
                return;

            _roomService.JoinRoom(SelectedRoom);
            SelectedRoom = null;
        }

        private void Initialize()
        {
            DisplayName = "Chat Window";

            _roomService.JoiningRoom += OnJoiningRoom;
            _roomService.RoomsRetrieved += OnRoomsRetrieved;
        }

        private void OnRoomsRetrieved(object sender, RoomsRetrievedEventArgs roomsRetrievedEventArgs)
        {
            _availableRooms = roomsRetrievedEventArgs.Rooms;
            NotifyOfPropertyChange(() => AvailableRooms);
        }

        private void OnJoiningRoom(object sender, JoiningRoomEventArgs joiningRoomEventArgs)
        {
            ActivateItem(joiningRoomEventArgs.Room);
        }

        public void Send()
        {
            if (string.IsNullOrEmpty(SendText))
                return;

            _client.Send(SendText, ActiveItem.DisplayName);
            SendText = null;
        }
    }
}