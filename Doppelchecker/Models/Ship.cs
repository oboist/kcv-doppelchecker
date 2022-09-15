﻿using CsvHelper.Configuration;
using Livet;

namespace Doppelchecker.Models
{
    public class Ship : NotificationObject
    {
        public Ship()
        {
            MemberShipsId = new ObservableSynchronizedCollection<int>();
            LockedMemberShipsId = new ObservableSynchronizedCollection<int>();
        }

        #region ShipClassName変更通知プロパティ
        private string _shipClassName;

        /// <summary>
        /// 艦種名
        /// </summary>
        public string ShipClassName
        {
            get
            { return _shipClassName; }
            set
            {
                if (_shipClassName == value)
                    return;
                _shipClassName = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ShipTypeName変更通知プロパティ
        private string _shipTypeName;

        /// <summary>
        /// 艦型名
        /// </summary>
        public string ShipTypeName
        {
            get
            { return _shipTypeName; }
            set
            {
                if (_shipTypeName == value)
                    return;
                _shipTypeName = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ShipTypeNumber変更通知プロパティ
        private int _shipTypeNumber;

        /// <summary>
        /// 艦型内番号
        /// </summary>
        public int ShipTypeNumber
        {
            get
            { return _shipTypeNumber; }
            set
            {
                if (_shipTypeNumber == value)
                    return;
                _shipTypeNumber = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ShipName変更通知プロパティ
        private string _shipName;

        /// <summary>
        /// 艦名
        /// </summary>
        public string ShipName
        {
            get
            { return _shipName; }
            set
            {
                if (_shipName == value)
                    return;
                _shipName = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region IsImplemented変更通知プロパティ
        private bool _isImplemented;

        /// <summary>
        /// 実装フラグ
        /// </summary>
        public bool IsImplemented
        {
            get
            { return _isImplemented; }
            set
            {
                if (_isImplemented == value)
                    return;
                _isImplemented = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region IsMember変更通知プロパティ
        private bool _isMember;

        /// <summary>
        /// 所属フラグ
        /// </summary>
        public bool IsMember
        {
            get
            { return _isMember; }
            set
            {
                if (_isMember == value)
                    return;
                _isMember = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ShipNameVariation変更通知プロパティ
        private string _shipNameVariation;

        /// <summary>
        /// 艦名変更艦艇用
        /// Currently, This field only support one time namechange (e.g. IJN 大鯨 -> IJN 龍鳳).
        /// For multiple time namechange (e.g. SMS Goeben), I think master data also has to be changed to JSON or other related.
        /// </summary>
        public string ShipNameVariation
        {
            get
            { return _shipNameVariation; }
            set
            {
                if (_shipNameVariation == value)
                    return;
                _shipNameVariation = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region MaxLevel変更通知プロパティ
        private int _maxLevel;

        /// <summary>
        /// 最高レベル
        /// </summary>
        public int MaxLevel
        {
            get
            { return _maxLevel; }
            set
            {
                if (_maxLevel == value)
                    return;
                _maxLevel = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region MemberShipsId変更通知プロパティ
        private ObservableSynchronizedCollection<int> _memberShipsId;

        public ObservableSynchronizedCollection<int> MemberShipsId
        {
            get
            { return _memberShipsId; }
            set
            {
                if (_memberShipsId == value)
                    return;
                _memberShipsId = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region LockedMemberShipsId変更通知プロパティ
        private ObservableSynchronizedCollection<int> _lockedMemberShipsId;

        public ObservableSynchronizedCollection<int> LockedMemberShipsId
        {
            get
            { return _lockedMemberShipsId; }
            set
            {
                if (_lockedMemberShipsId == value)
                    return;
                _lockedMemberShipsId = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region RemodelLevel変更通知プロパティ
        private int? _remodelLevel;

        /// <summary>
        /// 第一次改装レベル
        /// </summary>
        public int? RemodelLevel
        {
            get
            { return _remodelLevel; }
            set
            { 
                if (_remodelLevel == value)
                    return;
                _remodelLevel = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region SecondRemodelLevel変更通知プロパティ
        private int? _secondRemodelLevel;

        /// <summary>
        /// 第二次改装レベル
        /// </summary>
        public int? SecondRemodelLevel
        {
            get
            { return _secondRemodelLevel; }
            set
            {
                if (_secondRemodelLevel == value)
                    return;
                _secondRemodelLevel = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ThirdRemodelLevel変更通知プロパティ
        private int? _thirdRemodelLevel;

        /// <summary>
        /// 第三次改装レベル
        /// </summary>
        public int? ThirdRemodelLevel
        {
            get
            { return _thirdRemodelLevel; }
            set
            {
                if (_thirdRemodelLevel == value)
                    return;
                _thirdRemodelLevel = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region FourthRemodelLevel変更通知プロパティ
        private int? _fourthRemodelLevel;
        
        /// <summary>
        /// 第四次改装レベル
        /// </summary>
        public int? FourthRemodelLevel
        {
            get
            { return _fourthRemodelLevel; }
            set
            {
                if (_fourthRemodelLevel == value)
                    return;
                _fourthRemodelLevel = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region FifthRemodelLevel変更通知プロパティ
        private int? _fifthRemodelLevel;
        
        /// <summary>
        /// 最終改装レベル
        /// Only effective when remodel will be done more then twice
        /// e.g. Chitose, Chiyoda and Kasumi
        /// </summary>
        public int? FifthRemodelLevel
        {
            get
            { return _fifthRemodelLevel; }
            set
            {
                if (_fifthRemodelLevel == value)
                    return;
                _fifthRemodelLevel = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }

    public sealed class ShipMap : CsvClassMap<Ship>
    {
        public ShipMap()
        {
            Map(m => m.ShipClassName).Index(0);
            Map(m => m.ShipTypeName).Index(1);
            Map(m => m.ShipTypeNumber).Index(2);
            Map(m => m.ShipName).Index(3);
            Map(m => m.ShipNameVariation).Index(4);
            Map(m => m.IsImplemented).Index(5);
            Map(m => m.RemodelLevel).Index(6);
            Map(m => m.SecondRemodelLevel).Index(7);
            Map(m => m.ThirdRemodelLevel).Index(8);
            Map(m => m.FourthRemodelLevel).Index(9);
            Map(m => m.FifthRemodelLevel).Index(10);
        }
    }
}
