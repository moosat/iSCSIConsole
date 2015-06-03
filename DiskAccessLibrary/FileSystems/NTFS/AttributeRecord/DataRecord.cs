/* Copyright (C) 2014 Tal Aloni <tal.aloni.il@gmail.com>. All rights reserved.
 * 
 * You can redistribute this program and/or modify it under the terms of
 * the GNU Lesser Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace DiskAccessLibrary.FileSystems.NTFS
{
    // Data attribute can be either resident or non-resident attribute
    // This represents an AttributeRecord's data
    public class DataRecord
    {
        AttributeRecord m_record;

        public DataRecord(AttributeRecord record)
        {
            m_record = record;
        }

        public byte[] ReadDataCluster(NTFSVolume volume, long clusterVCN)
        {
            return ReadDataClusters(volume, clusterVCN, 1);
        }

        public byte[] ReadDataClusters(NTFSVolume volume, long clusterVCN, int count)
        {
            if (m_record is NonResidentAttributeRecord)
            {
                return ((NonResidentAttributeRecord)m_record).ReadDataClusters(volume, clusterVCN, count);
            }
            else
            {
                if (clusterVCN == 0)
                {
                    return ((ResidentAttributeRecord)m_record).Data;
                }
                else
                {
                    throw new ArgumentException("Invalid clusterVCN for resident attribute record");
                }
            }
        }

        public void WriteDataCluster(NTFSVolume volume, long clusterVCN, byte[] data)
        {
            WriteDataClusters(volume, clusterVCN, data);
        }

        public void WriteDataClusters(NTFSVolume volume, long clusterVCN, byte[] data)
        {
            if (m_record is NonResidentAttributeRecord)
            {
                ((NonResidentAttributeRecord)m_record).WriteDataClusters(volume, clusterVCN, data);
            }
            else
            {
                if (clusterVCN > data.Length / volume.BytesPerCluster)
                {
                    throw new ArgumentOutOfRangeException("Cluster VCN is not within the valid range");
                }

                long offset = clusterVCN * volume.BytesPerCluster;
                Array.Copy(data, 0, ((ResidentAttributeRecord)m_record).Data, offset, data.Length);
            }
        }

        public void ExtendRecord(NTFSVolume volume, ulong additionalLength)
        {
            ulong currentSize = this.DataRealSize;
            if (m_record is NonResidentAttributeRecord)
            {
                ((NonResidentAttributeRecord)m_record).ExtendRecord(volume, additionalLength);
            }
            else
            {
                // If the resident data record becomes too long, than it will be replaced with a non-resident data record when the file record will be saved
                byte[] data = ((ResidentAttributeRecord)m_record).Data;
                int currentLength = data.Length;
                byte[] temp = new byte[currentLength + (int)additionalLength];
                Array.Copy(data, temp, data.Length);
                ((ResidentAttributeRecord)m_record).Data = temp;
            }
        }

        public ulong GetDataAllocatedSize(int bytesPerCluster)
        {
            if (m_record is NonResidentAttributeRecord)
            {
                return (ulong)(((NonResidentAttributeRecord)m_record).DataClusterCount * bytesPerCluster);
            }
            else
            {
                return (ulong)((ResidentAttributeRecord)m_record).Data.Length;
            }
        }

        public ulong DataRealSize
        {
            get
            {
                if (m_record is NonResidentAttributeRecord)
                {
                    return ((NonResidentAttributeRecord)m_record).FileSize;
                }
                else
                {
                    return (ulong)((ResidentAttributeRecord)m_record).Data.Length;
                }
            }
        }

        public long DataClusterCount
        {
            get
            {
                if (m_record is NonResidentAttributeRecord)
                {
                    return ((NonResidentAttributeRecord)m_record).DataClusterCount;
                }
                else
                {
                    // can it be more than 1?
                    return 1;
                }
            }
        }

        public AttributeRecord Record
        {
            get
            {
                return m_record;
            }
        }
    }
}
