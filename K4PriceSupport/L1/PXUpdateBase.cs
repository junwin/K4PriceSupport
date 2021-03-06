﻿/***************************************************************************
 *
 *      Copyright (c) 2009,2010,2011 KaiTrade LLC (registered in Delaware)
 *                     All Rights Reserved Worldwide
 *
 * STRICTLY PROPRIETARY and CONFIDENTIAL
 *
 * WARNING:  This file is the confidential property of KaiTrade LLC For
 * use only by those with the express written permission and license from
 * KaiTrade LLC.  Unauthorized reproduction, distribution, use or disclosure
 * of this file or any program (or document) is prohibited.
 *
 ***************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using log4net;
using K4ServiceInterface;
using KaiTrade.Interfaces;

#pragma warning disable 0414

namespace L1PriceSupport
{
    [DataContract]
    public class PXUpdateBase : KaiTrade.Interfaces.IPXUpdate
    {
        private decimal?[] m_DataArray= new decimal?[KaiTrade.Interfaces.PXFields.MAXPXFIELDS];
        private string m_Mnemonic = "";
        private int m_DepthPosition = 0;
        private string m_Originator = "";
        public ILog m_Log = null;
        private KaiTrade.Interfaces.PXDepthOperation m_DepthOperation = KaiTrade.Interfaces.PXDepthOperation.none;
        private string m_DepthMarket = "";
        private long? m_ServerTicks;
        private string m_DriverTag = "";
        private KaiTrade.Interfaces.PXUpdateType m_UpdateType = KaiTrade.Interfaces.PXUpdateType.none;

        public PXUpdateBase(ILog log )
        {
            m_Log = log;
            m_DataArray = new decimal?[KaiTrade.Interfaces.PXFields.MAXPXFIELDS];

            this.Ticks = DateTime.Now.Ticks;
        }
        public PXUpdateBase(string originator)
        {
            m_DataArray = new decimal?[KaiTrade.Interfaces.PXFields.MAXPXFIELDS];
            this.Originator = originator;
            this.Ticks = DateTime.Now.Ticks;
        }
        public PXUpdateBase(KaiTrade.Interfaces.IProduct myProduct)
        {
            try
            {
                m_DataArray = new decimal?[KaiTrade.Interfaces.PXFields.MAXPXFIELDS];
                throw new Exception("Not Implimemnted");
                //this.From(myProduct.Mnemonic, myProduct.L1PX);
            }
            catch
            {
            }
        }
        #region PXUpdate Members
        [DataMember]
        public long Sequence
        {
            get
            {
                if (m_DataArray[KaiTrade.Interfaces.PXFields.SEQNUMBER] != null)
                {
                    return (long)m_DataArray[KaiTrade.Interfaces.PXFields.SEQNUMBER];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                m_DataArray[KaiTrade.Interfaces.PXFields.SEQNUMBER] = value;
            }
        }
        [DataMember]
        public long Ticks
        {
            get
            {
                if (m_DataArray[KaiTrade.Interfaces.PXFields.TIMETICKS] != null)
                {
                    return (long)m_DataArray[KaiTrade.Interfaces.PXFields.TIMETICKS];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                m_DataArray[KaiTrade.Interfaces.PXFields.TIMETICKS] = value;
            }
        }

        [DataMember]
        public KaiTrade.Interfaces.PXUpdateType UpdateType
        {
            get
            {
                return m_UpdateType;
            }
            set
            {
                m_UpdateType = value;
            }
        }

        [DataMember]
        public string DriverTag
        {
            get
            {
                return m_DriverTag;
            }
            set
            {
                m_DriverTag = value;
            }
        }

        [DataMember]
        public string Mnemonic
        {
            get
            {
                return m_Mnemonic;
            }
            set
            {
                m_Mnemonic = value;
            }
        }

        /// <summary>
        /// Get set the depth operation - i.e insert, replace delete
        /// </summary>
        [DataMember]
        public KaiTrade.Interfaces.PXDepthOperation DepthOperation
        {
            get { return m_DepthOperation; }
            set{m_DepthOperation=value;}
        }

        /// <summary>
        /// Market (MarketMaker) offering a depth entry
        /// </summary>
        [DataMember]
        public string DepthMarket
        {
            get { return m_DepthMarket; }
            set { m_DepthMarket = value; }
        }

        [DataMember]
        public int DepthPosition
        {
            get
            {
                return m_DepthPosition;
            }
            set
            {
                m_DepthPosition = value;
            }
        }
        [DataMember]
        public decimal? OfferPrice
        {
            get
            {
                return m_DataArray[KaiTrade.Interfaces.PXFields.OFFERPRICE];
            }
            set
            {
                m_DataArray[KaiTrade.Interfaces.PXFields.OFFERPRICE] = value;
            }
        }
        [DataMember]
        public decimal? OfferSize
        {
            get
            {
                return m_DataArray[KaiTrade.Interfaces.PXFields.OFFERSIZE];
            }
            set
            {
                m_DataArray[KaiTrade.Interfaces.PXFields.OFFERSIZE] = value;
            }
        }
        [DataMember]
        public decimal? BidSize
        {
            get
            {
                return m_DataArray[KaiTrade.Interfaces.PXFields.BIDSIZE];
            }
            set
            {
                if(m_DataArray==null)
                {
                    m_DataArray = new decimal?[KaiTrade.Interfaces.PXFields.MAXPXFIELDS];
                }
                m_DataArray[KaiTrade.Interfaces.PXFields.BIDSIZE] = value;
            }
        }
        [DataMember]
        public decimal? BidPrice
        {
            get
            {
                return m_DataArray[KaiTrade.Interfaces.PXFields.BIDPRICE];
            }
            set
            {
                if (m_DataArray == null)
                {
                    m_DataArray = new decimal?[KaiTrade.Interfaces.PXFields.MAXPXFIELDS];
                }
                m_DataArray[KaiTrade.Interfaces.PXFields.BIDPRICE] = value;
            }
        }

        [DataMember]
        public long? ServerTicks
        {
            get
            {
                return m_ServerTicks;
            }
            set
            {
                m_ServerTicks = value;
            }
        }

        [DataMember]
        public decimal? TradePrice
        {
            get
            {
                return m_DataArray[KaiTrade.Interfaces.PXFields.TRADEPRICE];
            }
            set
            {
                if (m_DataArray == null)
                {
                    m_DataArray = new decimal?[KaiTrade.Interfaces.PXFields.MAXPXFIELDS];
                }
                m_DataArray[KaiTrade.Interfaces.PXFields.TRADEPRICE] = value;
            }
        }
        [DataMember]
        public decimal? TradeVolume
        {
            get
            {
                return m_DataArray[KaiTrade.Interfaces.PXFields.TRADEVOLUME];
            }
            set
            {
                if (m_DataArray == null)
                {
                    m_DataArray = new decimal?[KaiTrade.Interfaces.PXFields.MAXPXFIELDS];
                }
                m_DataArray[KaiTrade.Interfaces.PXFields.TRADEVOLUME] = value;
            }
        }

        [DataMember]
        public decimal? DayHigh
        {
            get
            {
                return m_DataArray[KaiTrade.Interfaces.PXFields.DAYHIGH];
            }
            set
            {
                if (m_DataArray == null)
                {
                    m_DataArray = new decimal?[KaiTrade.Interfaces.PXFields.MAXPXFIELDS];
                }
                m_DataArray[KaiTrade.Interfaces.PXFields.DAYHIGH] = value;
            }
        }
        [DataMember]
        public decimal? DayLow
        {
            get
            {
                return m_DataArray[KaiTrade.Interfaces.PXFields.DAYLOW];
            }
            set
            {
                if (m_DataArray == null)
                {
                    m_DataArray = new decimal?[KaiTrade.Interfaces.PXFields.MAXPXFIELDS];
                }
                m_DataArray[KaiTrade.Interfaces.PXFields.DAYLOW] = value;
            }
        }

        public decimal? OfferPriceDelta
        {
            get
            {
                return m_DataArray[KaiTrade.Interfaces.PXFields.OFFERPRICEDELTA];
            }
            set
            {
                if (m_DataArray == null)
                {
                    m_DataArray = new decimal?[KaiTrade.Interfaces.PXFields.MAXPXFIELDS];
                }
                m_DataArray[KaiTrade.Interfaces.PXFields.OFFERPRICEDELTA] = value;
            }
        }

        public decimal? OfferSizeDelta
        {
            get
            {
                return m_DataArray[KaiTrade.Interfaces.PXFields.OFFERSIZEDELTA];
            }
            set
            {
                if (m_DataArray == null)
                {
                    m_DataArray = new decimal?[KaiTrade.Interfaces.PXFields.MAXPXFIELDS];
                }
                m_DataArray[KaiTrade.Interfaces.PXFields.OFFERSIZEDELTA] = value;
            }
        }

        public decimal? BidSizeDelta
        {
            get
            {
                return m_DataArray[KaiTrade.Interfaces.PXFields.BIDSIZEDELTA];
            }
            set
            {
                if (m_DataArray == null)
                {
                    m_DataArray = new decimal?[KaiTrade.Interfaces.PXFields.MAXPXFIELDS];
                }
                m_DataArray[KaiTrade.Interfaces.PXFields.BIDSIZEDELTA] = value;
            }
        }

        public decimal? BidPriceDelta
        {
            get
            {
                return m_DataArray[KaiTrade.Interfaces.PXFields.BIDPRICEDELTA];
            }
            set
            {
                if (m_DataArray == null)
                {
                    m_DataArray = new decimal?[KaiTrade.Interfaces.PXFields.MAXPXFIELDS];
                }
                m_DataArray[KaiTrade.Interfaces.PXFields.BIDPRICEDELTA] = value;
            }
        }

        public decimal? TradePriceDelta
        {
            get
            {
                return m_DataArray[KaiTrade.Interfaces.PXFields.TRADEPRICEDELTA];
            }
            set
            {
                m_DataArray[KaiTrade.Interfaces.PXFields.TRADEPRICEDELTA] = value;
            }
        }

        public decimal? TradeVolumeDelta
        {
            get
            {
                return m_DataArray[KaiTrade.Interfaces.PXFields.TRADEVOLUMEDELTA];
            }
            set
            {
                m_DataArray[KaiTrade.Interfaces.PXFields.TRADEVOLUMEDELTA] = value;
            }
        }
        [DataMember]
        public string Originator
        {
            get
            {
                return m_Originator;
            }
            set
            {
                m_Originator = value;
            }
        }

        public virtual void CalculateDeltas(KaiTrade.Interfaces.IPXUpdate prevUpdate)
        {
            try
            {
                // Just prices and size
                if ((BidPrice.HasValue) && (prevUpdate.BidPrice.HasValue))
                {
                    BidPriceDelta = BidPrice - prevUpdate.BidPrice;
                }
                else
                {
                    BidPriceDelta = null;
                }

                if ((BidSize.HasValue) && (prevUpdate.BidSize.HasValue))
                {
                    BidSizeDelta = BidSize - prevUpdate.BidSize;
                }
                else
                {
                    BidSizeDelta = null;
                }

                if ((OfferPrice.HasValue) && (prevUpdate.OfferPrice.HasValue))
                {
                    OfferPriceDelta = OfferPrice - prevUpdate.OfferPrice;
                }
                else
                {
                    OfferPriceDelta = null;
                }

                if ((OfferSize.HasValue) && (prevUpdate.OfferSize.HasValue))
                {
                    OfferSizeDelta = OfferSize - prevUpdate.OfferSize;
                }
                else
                {
                    OfferSizeDelta = null;
                }
                if ((TradePrice.HasValue) && (prevUpdate.TradePrice.HasValue))
                {
                    this.TradePriceDelta = TradePrice - prevUpdate.TradePrice;
                }
                else
                {
                    TradePriceDelta = null;
                }
                if ((TradeVolume.HasValue) && (prevUpdate.TradeVolume.HasValue))
                {
                    this.TradeVolumeDelta = TradeVolume - prevUpdate.TradeVolume;
                }
                else
                {
                    TradeVolumeDelta = null;
                }
            }
            catch
            {
                //m_Log.Error("compareUpdates", myE);
            }
        }

        public virtual void From(string myMnemonic, KaiTrade.Interfaces.L1PX myL1PX)
        {
            try
            {
                //this.Sequence
                this.Ticks = myL1PX.APIUpdateTime.Ticks;
                Mnemonic = myMnemonic;
                DepthPosition = 0;
                OfferPrice =  myL1PX.OfferPrice;
                OfferSize = myL1PX.OfferSize;
                BidSize = myL1PX.BidSize;
                BidPrice = myL1PX.BidPrice;
                TradePrice = myL1PX.TradePrice;
                TradeVolume = myL1PX.TradeVolume;
                ServerTicks  = myL1PX.APIUpdateTime.Ticks;
                //OfferPriceDelta
                //OfferSizeDelta
                //BidSizeDelta
                //BidPriceDelta
                //TradePriceDelta
                //TradeVolumeDelta
            }
            catch
            {
            }
        }

        public virtual void To(KaiTrade.Interfaces.L1PX myL1PX)
        {
            try
            {
                //this.Sequence
                myL1PX.APIUpdateTime = new DateTime(this.Ticks);
                //myL1PX.
                //Mnemonic
                //DepthPosition
                //myL1PX.OfferPrice = OfferPrice;
                //OfferSize
                //BidSize
                //BidPrice
                //TradePrice
                //TradeVolume
                //OfferPriceDelta
                //OfferSizeDelta
                //BidSizeDelta
                //BidPriceDelta
                //TradePriceDelta
                //TradeVolumeDelta
            }
            catch
            {
            }
        }

        decimal? GetDecimal(string value)
        {
            decimal? decimalValue = null;
            try
            {
                if (value.Length > 0)
                {
                    decimal dVal;
                    if (decimal.TryParse(value, out dVal))
                    {
                        decimalValue = dVal;
                    }
                }
            }
            catch
            {
            }
            return decimalValue;
        }
        public virtual void From(string myData, char myDelimiter)
        {
            try
            {
                string[] myDataArray = myData.Split(myDelimiter);
                if (myDataArray.Length >= 9)
                {
                    this.Originator = myDataArray[0];
                    this.Sequence = long.Parse(myDataArray[1]);
                    this.Ticks = long.Parse(myDataArray[2]);
                    this.Mnemonic = myDataArray[3];
                    this.DepthPosition = int.Parse(myDataArray[4]);
                    this.DepthOperation =  (KaiTrade.Interfaces.PXDepthOperation)int.Parse(myDataArray[5]);
                    this.DepthMarket =myDataArray[6];
                    this.BidSize = GetDecimal(myDataArray[7]);
                    this.BidPrice = GetDecimal(myDataArray[8]);
                    this.OfferPrice = GetDecimal(myDataArray[9]);
                    this.OfferSize = GetDecimal(myDataArray[10]);

                    this.TradePrice = GetDecimal(myDataArray[11]) ;
                    this.TradeVolume = GetDecimal(myDataArray[12]);
                    if (myDataArray.Length >= 14)
                    {
                        if (myDataArray[13].Length > 0)
                        {
                            this.ServerTicks = long.Parse(myDataArray[13]);
                        }
                        else
                        {
                            this.ServerTicks = this.Ticks;
                        }
                    }
                    else
                    {
                        this.ServerTicks = this.Ticks;
                    }
                    if (myDataArray.Length >= 15)
                    {
                        if (myDataArray[14].Length > 0)
                        {
                            this.DriverTag = myDataArray[14];
                        }
                    }
                    if (myDataArray.Length >= 16)
                    {
                        if (myDataArray[15].Length > 0)
                        {
                            this.UpdateType = (KaiTrade.Interfaces.PXUpdateType)int.Parse(myDataArray[15]);
                        }
                    }
                }
                else
                {
                    m_Log.Info(myData);
                }

                //OfferPriceDelta
                //OfferSizeDelta
                //BidSizeDelta
                //BidPriceDelta
                //TradePriceDelta
                //TradeVolumeDelta
            }
            catch
            {
            }
        }

        public virtual string To(char myDelimiter)
        {
            string myDelimitedString = "";
            try
            {
                myDelimitedString += this.Originator + myDelimiter;
                myDelimitedString += this.Sequence.ToString() + myDelimiter;
                myDelimitedString += this.Ticks.ToString() + myDelimiter;
                myDelimitedString += this.Mnemonic + myDelimiter;
                myDelimitedString += this.DepthPosition.ToString() + myDelimiter;
                myDelimitedString += ((int)this.DepthOperation).ToString() + myDelimiter;
                myDelimitedString += this.DepthMarket.ToString() + myDelimiter;
                myDelimitedString += this.BidSize.ToString() + myDelimiter;
                myDelimitedString += this.BidPrice.ToString() + myDelimiter;
                myDelimitedString += this.OfferPrice.ToString() + myDelimiter;
                myDelimitedString += this.OfferSize.ToString() + myDelimiter;
                myDelimitedString += this.TradePrice.ToString() + myDelimiter;
                myDelimitedString += this.TradeVolume.ToString() + myDelimiter;
                myDelimitedString += this.ServerTicks.ToString() + myDelimiter;
                myDelimitedString += this.DriverTag + myDelimiter;
                myDelimitedString += ((int)this.UpdateType).ToString();
            }
            catch
            {
            }
            return myDelimitedString;
        }

        #endregion

        public virtual void From(KaiTrade.Interfaces.IPXUpdate update)
        {
            throw new NotImplementedException();
        }
    }
}
