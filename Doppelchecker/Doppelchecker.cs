using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reactive.Linq;
using System.Security.Cryptography;
using System.Text;
using CsvHelper;
using Doppelchecker.Models;
using Doppelchecker.ViewModels;
using Doppelchecker.Views;
using Grabacr07.KanColleViewer.Composition;
using Grabacr07.KanColleWrapper;
using Grabacr07.KanColleWrapper.Models.Raw;
using Reactive.Bindings.Extensions;

namespace Doppelchecker
{
    [ExportMetadata("Title", "Doppelchecker")]
    [ExportMetadata("Description", "艦娘の所属状況を表示します。")]
    [ExportMetadata("Version", "0.5.1")]
    [ExportMetadata("Author", "@hgzr")]
    [ExportMetadata("Guid", "E2FBF552-0797-464B-BB01-B39A6571C4F9")]
    [Export(typeof(IPlugin))]
    [Export(typeof(ITool))]
    public class Doppelchecker : IPlugin, ITool
    {
        private ShipListViewModel _viewModel;
        private IDisposable PortSubscribe { get; set; }

        public void Initialize()
        {
            ImportCsv();

            PortSubscribe = KanColleClient.Current.Proxy.api_port.TryParse<kcsapi_port>().Subscribe(x => HomeportSubscription());
        }

        public void ImportCsv()
        {
            _viewModel = new ShipListViewModel();
            _viewModel.Initialize();

            using (var reader = new StreamReader("static\\ShipListMaster.csv", Encoding.UTF8))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap(new ShipMap());

                var records = csv.GetRecords<Ship>();
                foreach (var record in records)
                {
                    var ship = new ShipViewModel {Ship = record};
                    if (ship.Ship.IsImplemented)
                    {
                        _viewModel.ShipList.Add(ship);
                    }
                }
            }
        }

        public void HomeportSubscription()
        {
            if (KanColleClient.Current?.Homeport?.Organization?.Ships == null) return;
            KanColleClient.Current?.Homeport?.Organization.ObserveProperty(x => x.Ships).Subscribe(x => ShipSubscribe(x.Values));
            PortSubscribe.Dispose();
        }

        public void ShipSubscribe(IEnumerable<Grabacr07.KanColleWrapper.Models.Ship> homeportShips)
        {
            foreach (var ship in homeportShips)
            {
                var shipName = ship.Info.Name;
                
                // Variation name is mostly "" not nil and need length check or will be true
                var matchedEnumerable = _viewModel.ShipList.Where(
                    x => shipName.StartsWith(x.Ship.ShipName) ||
                    (x.Ship.ShipNameVariation.Length > 0 && shipName.StartsWith(x.Ship.ShipNameVariation))).Select(x => x);

                var matchedList = matchedEnumerable as ShipViewModel[] ?? matchedEnumerable.ToArray();
                
                if (!matchedList.Any()) continue;

                // Add to counter list
                var shipViewModel = matchedList.First();
                var shipModel = shipViewModel.Ship;
                shipModel.IsMember = true;

                var shipIdIndex = shipModel.MemberShipsId.IndexOf(ship.Id);
                if (shipIdIndex < 0) shipModel.MemberShipsId.Add(ship.Id);

                var shipIdLockedIndex = shipModel.LockedMemberShipsId.IndexOf(ship.Id);
                if (shipIdLockedIndex < 0 && ship.IsLocked) shipModel.LockedMemberShipsId.Add(ship.Id);
                if (shipIdLockedIndex >= 0 && !ship.IsLocked) shipModel.LockedMemberShipsId.Remove(ship.Id);

                if (ship.Level > shipModel.MaxLevel) shipModel.MaxLevel = ship.Level;
            }
        }

        public string Name { get; } = "Doppelchecker";
        public object View => new ShipListView {DataContext = _viewModel};
    }
}
