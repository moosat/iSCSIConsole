/* Copyright (C) 2012-2015 Tal Aloni <tal.aloni.il@gmail.com>. All rights reserved.
 * 
 * You can redistribute this program and/or modify it under the terms of
 * the GNU Lesser Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 */
using System;
using System.Collections.Generic;
using System.Text;
using Utilities;

namespace ISCSI
{
    public class StandardInquiryData
    {
        public byte PeripheralQualifier;
        public byte PeripheralDeviceType;
        public bool RMB;     // Removable media bit
        public byte Version; // We only support Version == 2
        public bool NormACA; // Normal ACA Supported
        public bool HiSup; // Hierarchical Support
        public byte ResponseDataFormat;
        public byte AdditionalLength; // Number of bytes following this byte

        public bool SCCS; // SCC Supported
        public bool ACC; // Access Controls Coordinator
        public byte TPGS; // Target Port Group Support
        public bool ThirdPartyCopy; // 3PC
        public bool Protect;

        public bool EncServ; // Enclosure Services
        public bool VS1;     // Vendor specific
        public bool MultiP;  // Multi Port
        public bool MChngr;  // Medium Changer
        public bool Addr16;  // Indicates that the SCSI target device supports 16-bit wide SCSI addresses

        public bool WBus16;  // Indicates that the SCSI target device supports 16-bit wide data transfers
        public bool Sync;    // Indicates that the SCSI target device supports synchronous data transfer
        public bool CmdQue;  // Command Queuing
        public bool VS2;     // Vendor specific

        public string VendorIdentification;
        public string ProductIdentification;
        public string ProductRevisionLevel;
        public ulong DriveSerialNumber;
        // Vendor Unique
        public byte Clocking;
        public bool QAS;
        public bool IUS;
        public byte[] VersionDescriptor = new byte[16];

        public StandardInquiryData()
        {
            ResponseDataFormat = 2;
            AdditionalLength = 91;
        }

        public StandardInquiryData(byte[] buffer, int offset)
        {
            PeripheralQualifier = (byte)(buffer[offset + 0] >> 5);
            PeripheralDeviceType = (byte)(buffer[offset + 0] & 0x1F);
            RMB = (buffer[offset + 1] & 0x80) != 0;
            Version = buffer[offset + 2];
            if (Version != 2)
            {
                return;
            }
            NormACA = (buffer[offset + 3] & 0x20) != 0;
            HiSup = (buffer[offset + 3] & 0x10) != 0;
            ResponseDataFormat = (byte)(buffer[offset + 3] & 0x0F);
            AdditionalLength = buffer[offset + 4];
            if (AdditionalLength < 91) // Standard Inquiry data should be at least 96 bytes (so the AdditionalLength must be at least 96 - 5)
            {
                return;
            }
            SCCS = (buffer[offset + 5] & 0x80) != 0;
            ACC = (buffer[offset + 5] & 0x40) != 0;
            TPGS = (byte)((buffer[offset + 5] >> 4) & 0x03);
            ThirdPartyCopy = (buffer[offset + 5] & 0x08) != 0;
            Protect = (buffer[offset + 5] & 0x01) != 0;

            EncServ = (buffer[offset + 6] & 0x40) != 0;
            VS1 = (buffer[offset + 6] & 0x20) != 0;
            MultiP = (buffer[offset + 6] & 0x10) != 0;
            MChngr = (buffer[offset + 6] & 0x08) != 0;
            Addr16 = (buffer[offset + 6] & 0x01) != 0;

            WBus16 = (buffer[offset + 7] & 0x20) != 0;
            Sync = (buffer[offset + 7] & 0x10) != 0;
            CmdQue = (buffer[offset + 7] & 0x02) != 0;
            VS2 = (buffer[offset + 7] & 0x01) != 0;

            VendorIdentification = Encoding.ASCII.GetString(buffer, 8, 8);
            ProductIdentification = Encoding.ASCII.GetString(buffer, 16, 16);
            ProductRevisionLevel = Encoding.ASCII.GetString(buffer, 32, 4);
            DriveSerialNumber = BigEndianConverter.ToUInt64(buffer, 36);

            Clocking = (byte)((buffer[offset + 56] >> 2) & 0x03);
            QAS = (buffer[offset + 56] & 0x02) != 0;
            IUS = (buffer[offset + 56] & 0x01) != 0;

            Array.Copy(buffer, offset + 58, VersionDescriptor, 0, 16);
        }

        public byte[] GetBytes()
        {
            byte[] buffer = new byte[96];
            buffer[0] |= (byte)(PeripheralQualifier << 5);
            buffer[0] |= (byte)(PeripheralQualifier & 0x1F);
            if (RMB)
            {
                buffer[1] |= 0x80;
            }
            buffer[2] = Version;

            if (NormACA)
            {
                buffer[3] |= 0x20;
            }
            if (HiSup)
            {
                buffer[3] |= 0x10;
            }
            buffer[3] |= (byte)(ResponseDataFormat & 0x0F);

            buffer[4] = AdditionalLength;

            if (SCCS)
            {
                buffer[5] |= 0x80;
            }
            if (ACC)
            {
                buffer[5] |= 0x40;
            }
            buffer[5] |= (byte)((TPGS & 0x03) << 4);
            if (ThirdPartyCopy)
            {
                buffer[5] |= 0x08;
            }
            if (Protect)
            {
                buffer[5] |= 0x01;
            }

            if (EncServ)
            {
                buffer[6] |= 0x40;
            }
            if (VS1)
            {
                buffer[6] |= 0x20;
            }
            if (MultiP)
            {
                buffer[6] |= 0x10;
            }
            if (MChngr)
            {
                buffer[6] |= 0x08;
            }
            if (Addr16)
            {
                buffer[6] |= 0x01;
            }

            if (WBus16)
            {
                buffer[7] |= 0x20;
            }
            if (Sync)
            {
                buffer[7] |= 0x10;
            }
            if (CmdQue)
            {
                buffer[7] |= 0x02;
            }
            if (VS2)
            {
                buffer[7] |= 0x01;
            }

            Array.Copy(ASCIIEncoding.ASCII.GetBytes(VendorIdentification), 0, buffer, 8, Math.Min(VendorIdentification.Length, 8));
            Array.Copy(ASCIIEncoding.ASCII.GetBytes(ProductIdentification), 0, buffer, 16, Math.Min(ProductIdentification.Length, 16));
            Array.Copy(ASCIIEncoding.ASCII.GetBytes(ProductRevisionLevel), 0, buffer, 32, 4);
            Array.Copy(BigEndianConverter.GetBytes(DriveSerialNumber), 0, buffer, 36, 8);


            buffer[56] |= (byte)((Clocking & 0x03) << 2);
            if (QAS)
            {
                buffer[56] |= 0x02;
            }
            if (IUS)
            {
                buffer[56] |= 0x01;
            }

            Array.Copy(VersionDescriptor, 0, buffer, 58, 16);

            return buffer;
        }
    }

}
